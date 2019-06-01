using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Key_Manager
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            listKeys.Resize += new EventHandler(listKeys_onResize);
            listKeys.ColumnClick += new ColumnClickEventHandler(listKeys_ColumnClick);
            listKeys.DoubleClick += new EventHandler(listKeys_DoubleClick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeListViewColumns(listKeys);
            rePopulateListFromDB(listKeys);
        }

        private void rePopulateListFromDB(ListView lv)
        {
            lv.Items.Clear();

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");


            try
            {
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

                    switch(reader.GetInt32(reader.GetOrdinal("status")))
                    {
                        case 0:
                            item.BackColor = Properties.Settings.Default.colorDefault;
                            break;
                        case 1:
                            item.BackColor = Properties.Settings.Default.colorAptOut;
                            break;
                        case 2:
                            item.BackColor = Properties.Settings.Default.colorRmOut;
                            break;
                    }

                    lv.Items.Add(item);
                }


                m_dbConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
                progressBar1.Style = ProgressBarStyle.Marquee;

                // simply start and await the loading task
                await Task.Run(() => exportDB(saveDlg.FileName));

                // re-enable things
                progressBar1.Visible = false;
            }
        }

        private async void ImportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the current existing keylist and replace it with the one you are importing?", "Reset Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
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
                progressBar1.Style = ProgressBarStyle.Marquee;

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

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);

                try
                {

                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                    // Columns B-F Should be formatted as Text
                    ws.get_Range("B1").EntireColumn.NumberFormat = "@";
                    ws.get_Range("C1").EntireColumn.NumberFormat = "@";
                    ws.get_Range("D1").EntireColumn.NumberFormat = "@";

                    int r, c;
                    c = 1;
                    r = 2; // Start At Second Row for headers

                    string[] headers = { "Room Space", "Key Code (Room)", "Key Code (APT)", "Student ID", "Last Name", "First Name", "Date Last Updated", "Status" };

                    foreach (string header in headers)
                        ws.Cells[1, c++] = header;

                    c = 1; // Reset Column



                    m_dbConnection.Open();
                    string sql = "SELECT * FROM keylist order by RoomSpace asc";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ws.Cells[r, 1] = reader["RoomSpace"].ToString();
                        ws.Cells[r, 2] = reader["KeyCodeRm"].ToString();
                        ws.Cells[r, 3] = reader["KeyCodeApt"].ToString();
                        ws.Cells[r, 4] = reader["StudentId"].ToString();
                        ws.Cells[r, 5] = reader["NameLast"].ToString();
                        ws.Cells[r, 6] = reader["NameFirst"].ToString();
                        ws.Cells[r, 7] = reader["DateStamp"].ToString();
                        ws.Cells[r, 8] = reader["status"].ToString();
                        r++;
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    m_dbConnection.Close();

                    wb.SaveAs(path);
                    wb.Close(false, Type.Missing, Type.Missing);
                    app.Quit();
                }
        }

        private void importDB(String path) {

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(path);

                try
                {

                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)ws.UsedRange;

                    int rowCount = range.Rows.Count;
                    int colCount = range.Columns.Count;

                    m_dbConnection.Open();

                    // Empty Current Keylist
                    string sql = "DELETE FROM keylist";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();


                    command = new SQLiteCommand("INSERT INTO keylist VALUES (?,?,?,?,?,?,?)", m_dbConnection);

                    for (int r = 2; r <= rowCount; r++)
                    {

                    if (Convert.ToString(range.Cells[r, 1].Value2) == null)
                        break;

                        command = new SQLiteCommand("INSERT INTO keylist VALUES (@RoomSpace,@KeyCodeRM,@KeyCodeAPT,@StudentID,@LastName,@FirstName,@DateStamp,@status)", m_dbConnection);
                        command.Parameters.AddWithValue("@RoomSpace", Convert.ToString(range.Cells[r, 1].Value2));
                        command.Parameters.AddWithValue("@KeyCodeRM", Convert.ToString(range.Cells[r, 2].Value2));
                        command.Parameters.AddWithValue("@KeyCodeAPT", Convert.ToString(range.Cells[r, 3].Value2));
                        command.Parameters.AddWithValue("@StudentID", Convert.ToString(range.Cells[r, 4].Value2));
                        command.Parameters.AddWithValue("@LastName", Convert.ToString(range.Cells[r, 5].Value2));
                        command.Parameters.AddWithValue("@FirstName", Convert.ToString(range.Cells[r, 6].Value2));

                        command.Parameters.AddWithValue("@DateStamp", DateTime.FromOADate(Convert.ToDouble(range.Cells[r, 7].Value2)).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        command.Parameters.AddWithValue("@status", Convert.ToString(range.Cells[r, 8].Value2));

                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    m_dbConnection.Close();

                    wb.Close(false, Type.Missing, Type.Missing);
                    app.Quit();
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

                // Try to connect and check if keylist table exists. If not, error, otherwise update properties

                Properties.Settings.Default.dbPath = openDlg.FileName;
                Properties.Settings.Default.Save();
                rePopulateListFromDB(listKeys);
            }

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
                try
                {
                    SQLiteConnection.CreateFile(saveDlg.FileName);
                    Properties.Settings.Default.dbPath = saveDlg.FileName;
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

                    string sql = @"CREATE TABLE `keylist` 
                            ( `RoomSpace` VARCHAR(11) NOT NULL,
	                          `KeyCodeRm` VARCHAR(8) NOT NULL,
	                          `KeyCodeApt` VARCHAR(8) NOT NULL,
	                          `StudentId` VARCHAR(8) NOT NULL,
	                          `NameLast` VARCHAR(16) NOT NULL,
	                          `NameFirst` VARCHAR(16) NOT NULL,
	                          `DateStamp` TIMESTAMP NOT NULL,
                              `status` TINYINT NOT NULL,
                               PRIMARY KEY(`RoomSpace`)
                            );";

                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
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
            }
        }

        private void ColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
