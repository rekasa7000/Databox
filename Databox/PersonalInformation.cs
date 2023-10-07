using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databox;
using MaterialSkin;
using MySql.Data.MySqlClient;

namespace Databox
{
    public partial class PersonalInformation : Form
    {
        private Database_Connection dbConnection;
        private bool isEditing;

        public PersonalInformation()
        {
            InitializeComponent();
            dbConnection = new Database_Connection();
            var skin = MaterialSkinManager.Instance;
            skin.Theme = MaterialSkinManager.Themes.DARK;
            skin.ColorScheme = new ColorScheme(
                Color.FromArgb(30, 31, 34),
                Color.FromArgb(43, 45, 49),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(92, 132, 217),
                TextShade.WHITE
                );
        }

        private void PersonalInformation_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            SessionData sessionData = SessionManager.LoadSession();
            string query = "";

            if (sessionData.AccountType == "Teacher")
            {
                query = $"SELECT * FROM teacher WHERE teacher_id = {sessionData.TeacherId}";
            }
            else if (sessionData.AccountType == "Student")
            {
                query = $"SELECT * FROM student WHERE student_id = {sessionData.StudentId}";
            }

            DataTable resultTable = dbConnection.ExecuteSelectQuery(query);
            if (resultTable.Rows.Count > 0)
            {
                if (sessionData.AccountType == "Student")
                {
                    DataRow row = resultTable.Rows[0];
                    txtFirstName.Text = row["studFirstname"].ToString();
                    txtMiddlename.Text = row["studMiddlename"].ToString();
                    txtLastname.Text = row["studLastname"].ToString();
                    txtContact.Text = row["studContact"].ToString();
                    txtCollege.Text = row["studCourse"].ToString();
                }
                else
                {
                    DataRow row = resultTable.Rows[0];
                    txtFirstName.Text = row["teacherFirstname"].ToString();
                    txtMiddlename.Text = row["teacherMiddlename"].ToString();
                    txtLastname.Text = row["teacherLastname"].ToString();
                    txtContact.Text = row["teacherContact"].ToString();
                    txtCollege.Text = row["teacherCollege"].ToString();
                }
                btnSave.Visible = false;
                btnCancel.Visible = false;
                isEditing = false;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true;
                btnSave.Visible = true;
                btnCancel.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SessionData sessionData = SessionManager.LoadSession();
            string updateQuery = "";

            if (sessionData.AccountType == "Teacher")
            {
                updateQuery = $"UPDATE teacher SET teacherFirstname = '{txtFirstName.Text}', teacherMiddlename = '{txtMiddlename.Text}', teacherLastname = '{txtLastname.Text}', teacherContact = '{txtContact.Text}', teacherCollege = '{txtCollege.Text}' WHERE teacher_id = {sessionData.TeacherId}";
            }
            else if (sessionData.AccountType == "Student")
            {
                updateQuery = $"UPDATE student SET studFirstname = '{txtFirstName.Text}', studMiddlename = '{txtMiddlename.Text}', studLastname = '{txtLastname.Text}', studContact = '{txtContact.Text}', studCourse = '{txtCollege.Text}' WHERE student_id = {sessionData.StudentId}";
            }

            int rowsAffected = dbConnection.ExecuteNonQuery(updateQuery);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Personal Information updated successfully.");
                btnSave.Visible = false;
                btnCancel.Visible = false;
                isEditing = false;
            }
            else
            {
                MessageBox.Show("Failed to update data. Please try again.");
            }
        }
    }
}
