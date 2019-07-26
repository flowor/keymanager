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
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
            this.ActiveControl = textBoxRmSpace;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { textBoxRmSpace, textBoxKeyCodeRm, textBoxKeyCodeApt,
            textBoxSid, textBoxFname, textBoxLname};

            // Exit method if any blanks
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text.Trim() == "")
                    return;
            }
            
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.dbPath + ";Version=3;");

            try
            {

                m_dbConnection.Open();

                        
                SQLiteCommand command = new SQLiteCommand("INSERT INTO keylist VALUES (@RoomSpace,@KeyCodeRM,@KeyCodeAPT,@StudentID,@LastName,@FirstName,@DateStamp,@status)", m_dbConnection);
                command.Parameters.AddWithValue("@RoomSpace", textBoxRmSpace.Text);
                command.Parameters.AddWithValue("@KeyCodeRM", textBoxKeyCodeRm.Text);
                command.Parameters.AddWithValue("@KeyCodeAPT", textBoxKeyCodeApt.Text);
                command.Parameters.AddWithValue("@StudentID", textBoxSid.Text);
                command.Parameters.AddWithValue("@LastName", textBoxLname.Text);
                command.Parameters.AddWithValue("@FirstName", textBoxFname.Text);

                command.Parameters.AddWithValue("@DateStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                command.Parameters.AddWithValue("@status", 0);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                m_dbConnection.Close();
                mainForm.rePopulateListFromDB(mainForm._listKeys);
                Close();
            }
        }
    }
}
