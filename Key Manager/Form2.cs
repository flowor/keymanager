using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Key_Manager
{
    public partial class Form2 : Form
    {
        public Form2(ListViewItem listitem)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            groupBoxEditEntry.Text = listitem.SubItems[0].Text;
            textBoxKeyCodeRm.Text = listitem.SubItems[1].Text;
            textBoxKeyCodeApt.Text = listitem.SubItems[2].Text;
            textBoxSid.Text = listitem.SubItems[3].Text;
            textBoxLname.Text = listitem.SubItems[4].Text;
            textBoxFname.Text = listitem.SubItems[5].Text;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {

            mainForm.toggleProgress();


            List<TextBox> textBoxes = new List<TextBox> { textBoxKeyCodeRm, textBoxKeyCodeApt, textBoxSid, textBoxFname, textBoxLname};

            // Exit method if any blanks
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text.Trim() == "")
                    return;
            }


            await Task.Run(() => UpdateItem(groupBoxEditEntry.Text, textBoxKeyCodeRm.Text, textBoxKeyCodeApt.Text, textBoxSid.Text, textBoxLname.Text, textBoxFname.Text));

            mainForm.toggleProgress();
            mainForm.rePopulateListFromDB(mainForm._listKeys);
            Close();

        }

        private void UpdateItem(string groupBoxEditEntryText, string textBoxKeyCodeRmText, string textBoxKeyCodeAptText, string textBoxSidText, string textBoxLnameText, string textBoxFnameText)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            try
            {

                m_dbConnection.Open();

                string sql = "SELECT status FROM keylist WHERE RoomSpace = @RoomSpace";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", groupBoxEditEntryText);
                SQLiteDataReader reader = command.ExecuteReader();

                reader.Read();
                int status = reader.GetInt32(0);
                reader.Close();

                sql = @"UPDATE keylist
                               SET KeyCodeRm = @KeyCodeRM,
                               KeyCodeApt = @KeyCodeAPT,
                               StudentId = @StudentID,
                               NameLast = @LastName,
                               NameFirst = @FirstName,
                               DateStamp = @DateStamp,
                               status = @status

                               WHERE RoomSpace = @RoomSpace";

                command = new SQLiteCommand(sql, m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", groupBoxEditEntryText);

                command.Parameters.AddWithValue("@KeyCodeRM", textBoxKeyCodeRmText);
                command.Parameters.AddWithValue("@KeyCodeAPT", textBoxKeyCodeAptText);
                command.Parameters.AddWithValue("@StudentID", textBoxSidText);
                command.Parameters.AddWithValue("@LastName", textBoxLnameText);
                command.Parameters.AddWithValue("@FirstName", textBoxFnameText);

                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.Parameters.AddWithValue("@status", status.ToString());

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

        private void Button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
