using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Key_Manager
{
    public partial class mainForm : Form
    {
        public static ListView _listKeys;
        public static ProgressBar _progressBar;
        public mainForm()
        {
            InitializeComponent();
            _progressBar = progressBar1;
            progressBar1.Style = ProgressBarStyle.Marquee;

            _listKeys = listKeys;
            listKeys.Resize += new EventHandler(listKeys_onResize);
            listKeys.ColumnClick += new ColumnClickEventHandler(listKeys_ColumnClick);
            listKeys.DoubleClick += new EventHandler(MenuStripItemEdit_Click);
            listKeys.MouseClick += new MouseEventHandler(listKeys_MouseClick);
            if (Properties.Settings.Default.dbPath == "init")
                createNewDb("entries.sqlite");
            else
                this.Text = String.Format("Spare Key Manager ({0})", Properties.Settings.Default.dbPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeListViewColumns(listKeys);
            rePopulateListFromDB(listKeys);
        }

        public static void rePopulateListFromDB(ListView lv)
        {
            lv.Items.Clear();

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");


            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();
                string sql = "SELECT * FROM keylist order by RoomSpace asc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["RoomSpace"].ToString());
                    item.SubItems.Add(reader["KeyCodeRm"].ToString());
                    item.SubItems.Add(reader["KeyCodeApt"].ToString());
                    item.SubItems.Add(reader["StudentId"].ToString());
                    item.SubItems.Add(reader["NameLast"].ToString());
                    item.SubItems.Add(reader["NameFirst"].ToString());
                    item.SubItems.Add(reader["DateStamp"].ToString());

                    switch (reader.GetInt32(reader.GetOrdinal("status")))
                    {
                        case 0:
                            item.BackColor = Properties.Settings.Default.colorDefault;
                            break;
                        case 1:
                            item.BackColor = Properties.Settings.Default.colorAptOut;
                            item.ToolTipText = "Missing Apartment Key";
                            break;
                        case 2:
                            item.BackColor = Properties.Settings.Default.colorRmOut;
                            item.ToolTipText = "Missing Room Key";
                            break;
                        case 3:
                            item.BackColor = Properties.Settings.Default.colorBothOut;
                            item.ToolTipText = "Missing Both Keys";
                            break;
                    }

                    lv.Items.Add(item);
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        private void listKeys_onResize(object sender, EventArgs e)
        {
            ResizeListViewColumns(listKeys);
        }

        private void ResizeListViewColumns(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }

        // The column we are currently using for sorting.
        private ColumnHeader SortingColumn = null;

        // csharphelper.com/blog/2014/09/sort-a-listview-using-the-column-you-click-in-c
        // Sort on this column.
        private void listKeys_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = listKeys.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn.Text.StartsWith("∧ "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn.Text = SortingColumn.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn.Text = "∧ " + SortingColumn.Text;
            }
            else
            {
                SortingColumn.Text = "∨ " + SortingColumn.Text;
            }

            // Create a comparer.
            listKeys.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            listKeys.Sort();
        }

        private void listKeys_DoubleClick(object sender, EventArgs e)
        {
            if (listKeys.SelectedItems != null)
            {
                MessageBox.Show(listKeys.SelectedItems[0].Text);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


        private async void ExportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Save File in Excel (.xlsx format)
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Excel Workbook|*.xlsx";
            saveDlg.RestoreDirectory = true;
            saveDlg.Title = "Export keylist as an Excel File";
            saveDlg.DefaultExt = "xlsx";
            saveDlg.AddExtension = true;

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {

                progressBar1.Visible = true;

                // simply start and await the loading task
                await Task.Run(() => exportDB(saveDlg.FileName));

                // re-enable things
                progressBar1.Visible = false;
            }
        }

        private async void ImportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listKeys.Items.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the current existing keylist and replace it with the one you are importing?", "Reset Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }


            // Open Excel File (.xlsx format)
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Excel Workbook|*.xlsx";
            openDlg.RestoreDirectory = true;
            openDlg.Title = "Import excel file into keylist";

            // Show the open dialog
            if (openDlg.ShowDialog() == DialogResult.OK)
            {


                progressBar1.Visible = true;

                // simply start and await the loading task
                await Task.Run(() => importDB(openDlg.FileName));

                rePopulateListFromDB(listKeys);

                // re-enable things
                progressBar1.Visible = false;
            }
        }

        private void exportDB(String path)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();
                string sql = "SELECT * FROM keylist order by RoomSpace asc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);

                WriteExcelFile(path, dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        private void importDB(String path)
        {

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();
                DataTable dt = ReadAsDataTable(path);

                string sql = "DELETE FROM keylist";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(m_dbConnection))
                {
                    using (var transaction = m_dbConnection.BeginTransaction())
                    {

                        var cols = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToString());

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            cmd.CommandText = "INSERT INTO keylist VALUES (@RoomSpace,@KeyCodeRM,@KeyCodeAPT,@StudentID,@NameLast,@NameFirst,@DateStamp,@status)";
                            cmd.Parameters.AddWithValue("@RoomSpace", Convert.ToString(dataRow["RoomSpace"]));
                            cmd.Parameters.AddWithValue("@KeyCodeRM", Convert.ToString(dataRow["KeyCodeRm"]));
                            cmd.Parameters.AddWithValue("@KeyCodeAPT", Convert.ToString(dataRow["KeyCodeAPT"]));
                            cmd.Parameters.AddWithValue("@StudentID", Convert.ToString(dataRow["StudentID"]));
                            cmd.Parameters.AddWithValue("@NameLast", Convert.ToString(dataRow["NameLast"]));
                            cmd.Parameters.AddWithValue("@NameFirst", Convert.ToString(dataRow["NameFirst"]));

                            DateTime date = DateTime.Parse(dataRow["DateStamp"].ToString());
                            string dateString = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            cmd.Parameters.AddWithValue("@DateStamp", dateString);
                            cmd.Parameters.AddWithValue("@status", Convert.ToString(dataRow["Status"]));

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }

        }

        public static DataTable ReadAsDataTable(string fileName)
        {
            DataTable dataTable = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                    }

                    dataTable.Rows.Add(dataRow);
                }

            }

            // Fix Header
            foreach (DataColumn column in dataTable.Columns)
                column.ColumnName = dataTable.Rows[0][column.ColumnName].ToString();
            

            dataTable.Rows.RemoveAt(0);


            return dataTable;
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private static void WriteExcelFile(string outputPath, DataTable table)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(outputPath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Key Manager" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }

        private void SetServerLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "sqlite database|*.sqlite";
            openDlg.RestoreDirectory = true;
            openDlg.Title = "Select Database";

            if (openDlg.ShowDialog() == DialogResult.OK && openDlg.FileName != "")
            {

                // TODO Try to connect and check if keylist table exists and log table. If not, error, otherwise update properties

                Properties.Settings.Default.dbPath = openDlg.FileName;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }

            this.Text = String.Format("Spare Key Manager ({0})", Properties.Settings.Default.dbPath);

        }


        private void CreateDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "sqlite database|*.sqlite";
            saveDlg.RestoreDirectory = true;
            saveDlg.Title = "Location to save database";
            saveDlg.DefaultExt = "sqlite";
            saveDlg.AddExtension = true;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                createNewDb(saveDlg.FileName);
            }
        }

        private void createNewDb(string path)
        {
            try
            {
                SQLiteConnection.CreateFile(path);
                Properties.Settings.Default.dbPath = path;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            try
            {
                m_dbConnection.Open();

                // Main Keylist table creation
                string sql = @"CREATE TABLE `keylist` 
                            ( `RoomSpace` VARCHAR(32) NOT NULL,
	                          `KeyCodeRm` VARCHAR(8) NOT NULL,
	                          `KeyCodeApt` VARCHAR(8) NOT NULL,
	                          `StudentId` VARCHAR(8) NOT NULL,
	                          `NameLast` VARCHAR(32) NOT NULL,
	                          `NameFirst` VARCHAR(32) NOT NULL,
	                          `DateStamp` TIMESTAMP NOT NULL,
                              `status` TINYINT NOT NULL,
                               PRIMARY KEY(`RoomSpace`)
                            );";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                // Log Table creation
                sql = @"CREATE TABLE `log` 
                            ( `id` integer primary key,
                              `RoomSpace` VARCHAR(32) NOT NULL,
                              `status_from` TINYINT NOT NULL,
                              `status_to` TINYINT NOT NULL,
	                          `DateStamp` TIMESTAMP NOT NULL
                            );";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
                rePopulateListFromDB(listKeys);
            }
            this.Text = String.Format("Spare Key Manager ({0})", Properties.Settings.Default.dbPath);
        }

        private void ColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void NewEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdd fa = new FormAdd();
            fa.Show();
        }

        private void ChangeDefaultColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();

            // Sets the initial color select to the current text color.
            MyDialog.Color = Properties.Settings.Default.colorDefault;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.colorDefault = MyDialog.Color;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }
        }

        private void ChangeAPTOutColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();

            // Sets the initial color select to the current text color.
            MyDialog.Color = Properties.Settings.Default.colorAptOut;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.colorAptOut = MyDialog.Color;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }
        }

        private void ChangeRMOutColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();

            // Sets the initial color select to the current text color.
            MyDialog.Color = Properties.Settings.Default.colorRmOut;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.colorRmOut = MyDialog.Color;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }
        }


        private void listKeys_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listKeys.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    //  menuStripItemEdit.Text = listKeys.FocusedItem.Text;
                }
            }
        }

        private void MenuStripItemEdit_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(listKeys.FocusedItem);
            form2.Show();
        }

        private void ChangeBothOutColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();

            // Sets the initial color select to the current text color.
            MyDialog.Color = Properties.Settings.Default.colorBothOut;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.colorBothOut = MyDialog.Color;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }
        }

        private async void MenuStripItemMissingRm_Click(object sender, EventArgs e)
        {

                progressBar1.Visible = true;

                // simply start and await the loading task
                await Task.Run(() => ToggleMissingRm());

                // re-enable things
                progressBar1.Visible = false;
           
                rePopulateListFromDB(listKeys);
        }

        private void ToggleMissingRm()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");
            string item = "";

            BeginInvoke((MethodInvoker)delegate
            {
                item = listKeys.FocusedItem.Text;
            });

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();

                string sql = "SELECT status FROM keylist WHERE RoomSpace = @RoomSpace";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                SQLiteDataReader reader = command.ExecuteReader();

                reader.Read();
                int status = reader.GetInt32(0);
                reader.Close();

                // Update Log
                sql = @"INSERT INTO log (RoomSpace, status_from, status_to, DateStamp)
                        VALUES (@RoomSpace, @status_from, @status_to, @DateStamp)";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                command.Parameters.AddWithValue("@status_from", status);

                switch (status)
                {
                    case 0:
                        status = 2;
                        break;

                    case 1:
                        status = 3;
                        break;

                    case 2:
                        status = 0;
                        break;

                    case 3:
                        status = 1;
                        break;
                }

                command.Parameters.AddWithValue("@status_to", status);
                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.ExecuteNonQuery();

                sql = @"UPDATE keylist
                               SET status = @status,
                               DateStamp = @DateStamp

                               WHERE RoomSpace = @RoomSpace";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        private async void MenuStripItemMissingApt_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            // simply start and await the loading task
            await Task.Run(() => ToggleMissingApt());

            // re-enable things
            progressBar1.Visible = false;

            rePopulateListFromDB(listKeys);
            
        }

        public static void toggleProgress()
        {
            _progressBar.Visible = !_progressBar.Visible;
        }

        private void ToggleMissingApt()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");
            string item = "";

            BeginInvoke((MethodInvoker)delegate
            {
                item = listKeys.FocusedItem.Text;
            });

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();

                string sql = "SELECT status FROM keylist WHERE RoomSpace = @RoomSpace";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                SQLiteDataReader reader = command.ExecuteReader();

                reader.Read();
                int status = reader.GetInt32(0);
                reader.Close();

                // Update Log
                sql = @"INSERT INTO log (RoomSpace, status_from, status_to, DateStamp)
                        VALUES (@RoomSpace, @status_from, @status_to, @DateStamp)";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                command.Parameters.AddWithValue("@status_from", status);

                switch (status)
                {
                    case 0:
                        status = 1;
                        break;

                    case 1:
                        status = 0;
                        break;

                    case 2:
                        status = 3;
                        break;

                    case 3:
                        status = 2;
                        break;
                }

                command.Parameters.AddWithValue("@status_to", status);
                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.ExecuteNonQuery();

                sql = @"UPDATE keylist
                               SET status = @status,
                               DateStamp = @DateStamp

                               WHERE RoomSpace = @RoomSpace";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rePopulateListFromDB(listKeys);
        }

        private async void MenuStripItemDelete_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;

            // simply start and await the loading task
            await Task.Run(() => deleteItem());

            // re-enable things
            progressBar1.Visible = false;

            rePopulateListFromDB(listKeys);
        }

        private void deleteItem()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");
            string item = "";
            BeginInvoke((MethodInvoker)delegate
            {
                item = listKeys.FocusedItem.Text;
            });

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));

                m_dbConnection.Open();

                string sql = "DELETE FROM keylist WHERE RoomSpace = @RoomSpace";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        private void GenerateEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = listKeys.FocusedItem;
            Dictionary<string, string> items = new Dictionary<string, string>();

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            string subject = "";
            string body = "";

            try
            {
                if (!File.Exists(Properties.Settings.Default.dbPath))
                    throw new Exception(String.Format("Database file not found at {0}", Properties.Settings.Default.dbPath));


                m_dbConnection.Open();
                string sql = "SELECT * FROM keylist WHERE RoomSpace = @RoomSpace";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", item.Text);
                SQLiteDataReader reader = command.ExecuteReader();

                reader.Read();

                items.Add("RoomSpace", reader["RoomSpace"].ToString());
                items.Add("KeyCodeRm", reader["KeyCodeRm"].ToString());
                items.Add("KeyCodeApt", reader["KeyCodeApt"].ToString());
                items.Add("StudentId", reader["StudentId"].ToString());
                items.Add("NameLast", reader["NameLast"].ToString());
                items.Add("NameFirst", reader["NameFirst"].ToString());
                items.Add("DateStamp", reader["DateStamp"].ToString());

                switch (reader.GetInt32(reader.GetOrdinal("status")))
                {
                    case 0:
                        // Both Keys in Stock
                        subject = String.Format($"{items["RoomSpace"]} | Spare Key Status");
                        body = String.Format($"The spare keys for {items["RoomSpace"]} are in stock. %0D%0A Resident: {items["NameLast"]}, {items["NameFirst"]} ({items["StudentId"]})%0D%0A%0D%0ARoom Key Code: {items["KeyCodeRm"]}%0D%0AApt Key Code: {items["KeyCodeApt"]}%0D%0A{items["DateStamp"]}%0D%0A%0D%0A");
                        break;
                    case 1:
                        // Missing Apt Key
                        subject = String.Format($"{items["RoomSpace"]} | Apartment Spare Issued");
                        body = String.Format($"The spare apartment key for {items["RoomSpace"]} has been issued to resident: {items["NameLast"]}, {items["NameFirst"]} ({items["StudentId"]})%0D%0A%0D%0AApt Key Code: {items["KeyCodeApt"]}%0D%0A{items["DateStamp"]}%0D%0A%0D%0AComment: %0D%0A%0D%0A");

                        break;
                    case 2:
                        // Missing Room Key
                        subject = String.Format($"{items["RoomSpace"]} | Room Spare Issued");
                        body = String.Format($"The spare room key for {items["RoomSpace"]} has been issued to resident: {items["NameLast"]}, {items["NameFirst"]} ({items["StudentId"]})%0D%0A%0D%0ARoom Key Code: {items["KeyCodeRm"]}%0D%0A{items["DateStamp"]}%0D%0A%0D%0AComment: %0D%0A%0D%0A");

                        break;
                    case 3:
                        // Missing Both Keys
                        subject = String.Format($"{items["RoomSpace"]} | Room & Apartment Spares Issued");
                        body = String.Format($"The spare room and apartment keys for {items["RoomSpace"]} have been issued to resident: {items["NameLast"]}, {items["NameFirst"]} ({items["StudentId"]})%0D%0A%0D%0ARoom Key Code: {items["KeyCodeRm"]}%0D%0AApt Key Code: {items["KeyCodeApt"]}%0D%0A{items["DateStamp"]}%0D%0A%0D%0AComment: %0D%0A%0D%0A");

                        break;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
            }

            System.Diagnostics.Process.Start(string.Format("mailto:?subject={0}&body={1}", subject, body));
        }

    }
}
