using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class StudentProgressList : Form
    {
        int teacher_id, class_id;
        Panel desktopPanel;
        public StudentProgressList(int teacherID, Panel pan)
        {
            teacher_id = teacherID;
            desktopPanel = pan;
            InitializeComponent();
        }

        private void StudentProgressList_Load(object sender, EventArgs e)
        {
            loadClasses();
        }
        void loadClasses()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = "SELECT className FROM class where teacher_id = " + teacher_id + " and classArchive = 'NO'";
            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                cmbClass.Items.Add(dataReader["className"].ToString());
            }

            dataReader.Close();
            dbConnection.CloseConnection();
        }
        void loadStudent(int startIndex)
        {
            Clear();
            Database_Connection db = new Database_Connection();
            List<string> fullNames = new List<string>();
            List<string> emails = new List<string>();
            List<string> studentIDs = new List<string>();

            db.OpenConnection();

            string query = "SELECT s.student_id, CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) AS fullName, u.email " +
                               "FROM student s " +
                               "JOIN user u ON u.user_id = s.user_id " +
                               "JOIN student_class sc ON sc.student_id = s.student_id " +
                               "JOIN class c ON c.class_id = sc.class_id " +
                               $"WHERE c.class_id = {class_id} AND c.classArchive = 'NO' " +
                               "ORDER BY fullName";


            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@startIndex", startIndex);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentName = reader.GetString("fullName");
                        fullNames.Add(studentName);
                        string email = reader.GetString("email");
                        emails.Add(email);
                        string studentID = reader.GetString("student_id");
                        studentIDs.Add(studentID);
                    }
                }
            }
            db.CloseConnection();
            for (int i = 0; i < fullNames.Count; i++)
            {
                Label label = Controls.Find("lblStudent" + (i + 1), true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = fullNames[i];
                    label.Enabled = true;
                    label.Tag = studentIDs[i];
                }
                if (label != null)
                {
                    label1.Text = emails[i];
                }
            }
        }
        void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                Label label = Controls.Find("lblStudent" + (i + 1), true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = "Empty";
                    label.Enabled = false;
                    label1.Text = "";
                }
            }
        }
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedClassName = cmbClass.SelectedItem.ToString();
            class_id = GetClassID(selectedClassName);
            loadStudent(0);
        }

        private void lblStudent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;
            string studentIDString = link.Tag.ToString();
            if (int.TryParse(studentIDString, out int student_id))
            {
                ProgressGraph taskView = new ProgressGraph(teacher_id, class_id, student_id, desktopPanel);
                taskView.TopLevel = false;
                desktopPanel.Controls.Clear();
                desktopPanel.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.FormBorderStyle = FormBorderStyle.None;
                taskView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetClassID(string className)
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = $"SELECT class_id FROM class WHERE className = @className AND classArchive = 'NO'";
            MySqlCommand command = new MySqlCommand(query, dbConnection.connection);
            command.Parameters.AddWithValue("@className", className);
            dbConnection.OpenConnection();
            int classID = Convert.ToInt32(command.ExecuteScalar());
            dbConnection.CloseConnection();
            return classID;
        }


    }
}
