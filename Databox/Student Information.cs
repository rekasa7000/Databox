using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    public partial class Student_Information : Form
    {
        string firstname, middlename, lastname, contactNo, selectedCourse;
        int course_id, passedId = 0 ;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Student_Information(int passedID)
        {
            InitializeComponent();
            passedId = passedID;
        }

        private void Student_Information_Load(object sender, EventArgs e)
        {
            Database_Connection dbConnection = new Database_Connection();

            string query = "SELECT courseName FROM course";
            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                cmbCollege.Items.Add(dataReader["courseName"].ToString());
            }
            dataReader.Close();
            dbConnection.CloseConnection();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogReg login = new LogReg();
            login.Show();
        }

        private void btnHome_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void Fill()
        {
            firstname = txtFirstname.Text;
            middlename = txtMiddlename.Text;
            lastname = txtLastname.Text;
            contactNo = txtContact.Text;
            selectedCourse = cmbCollege.Text;

            // Retrieve the course_id from the database
            Database_Connection db = new Database_Connection();
            string query = $"SELECT course_id FROM course WHERE courseName = '{selectedCourse}'";
            MySqlDataReader dataReader = db.GetData(query);

            if (dataReader.Read())
            {
                course_id = Convert.ToInt32(dataReader["course_id"]);
            }
            else
            {
                MessageBox.Show("Invalid selected course!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataReader.Close();
            db.CloseConnection();
        }



        private void iconMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Fill();
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname) && !string.IsNullOrEmpty(contactNo) && !string.IsNullOrEmpty(selectedCourse)) {
                MessageBox.Show("Welcome! Student " + firstname + " " + lastname + "!", "Account Registration Successful", MessageBoxButtons.OK);
                this.Hide();
                Database_Connection db = new Database_Connection();

                // Construct query to insert user details into student table
                string query = $"INSERT INTO student (studFirstname, studMiddlename, studLastname, studContact, studCourse, user_id, course_id) " +
                                $"VALUES ('{firstname}', '{middlename}', '{lastname}', '{contactNo}', '{selectedCourse}', '{passedId}', '{course_id}');";

                // Execute query
                db.ExecuteQuery(query);
                string query1 = "SELECT LAST_INSERT_ID()";

                // Execute query and retrieve the teacher_id value
                object result = db.ExecuteScalar(query1);
                int studentID = Convert.ToInt32(result);
                StudentDashboard student = new StudentDashboard(studentID);
                student.Show();
            } else
                MessageBox.Show("Please enter information on the required fields", "Error", MessageBoxButtons.OK);
        }
    }
}
