using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class StudentClasses : Form
    {
        int student_id, currentIndex, totalClasses;
        Panel desktopPanel;
        public StudentClasses(int studentID, Panel pan)
        {
            student_id = studentID;
            desktopPanel = pan;
            InitializeComponent();

        }

        private void StudentClasses_Load(object sender, EventArgs e)
        {
            classLoad(0);
            CalculateTotalClasses();
        }

        private void btnCreateclass_Click(object sender, EventArgs e)
        {
            AddClass add = new AddClass(student_id, desktopPanel);
            add.TopLevel = false;
            this.Enabled = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(add);
            add.FormBorderStyle = FormBorderStyle.None;
            add.StartPosition = FormStartPosition.CenterParent;

            // Calculate the center position of the desktopPanel
            int centerX = (desktopPanel.Width - add.Width) / 2;
            int centerY = (desktopPanel.Height - add.Height) / 2;
            add.Location = new Point(centerX, centerY);
            add.Show();
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentIndex -= 5;
            if (currentIndex < 0)
            {
                MessageBox.Show("You are already at the beginning.");
                currentIndex = 0;
            }
            classLoad(currentIndex);
        }

        void CalculateTotalClasses()
        {
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class " +
               "JOIN student_class ON class.class_id = student_class.class_id " +
               "WHERE student_class.student_id = @student_id AND class.classArchive = 'NO'", db.connection))
            {
                countCmd.Parameters.AddWithValue("@student_id", student_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
        }

        void Clear()
        {
            for (int i = 1; i <= 5; i++)
            {
                Label label = Controls.Find("lblClass" + i, true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + i, true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblSection" + i, true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblTeacher" + i, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = "Empty.";
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                }
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Clear();
            currentIndex += 5; // Increment the current index by 5

            if (currentIndex >= totalClasses)
            {
                MessageBox.Show("No more classes to display.");
                currentIndex -= 5;
            }
            classLoad(currentIndex);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ArchivedClasses add = new ArchivedClasses(student_id, desktopPanel);
            add.TopLevel = false;
            desktopPanel.Controls.Clear(); 
            desktopPanel.Controls.Add(add);
            add.Dock = DockStyle.Fill;
            add.FormBorderStyle = FormBorderStyle.None;
            add.Show();
        }

        void classLoad(int startIndex)
        {
            Database_Connection db = new Database_Connection();
            List<string> classNames = new List<string>();
            List<string> classCodes = new List<string>();
            List<string> classSections = new List<string>();
            List<string> classTeachers = new List<string>();
            List<string> classIDs = new List<string>();

            db.OpenConnection();

            // Create the SQL query
            string query = @"SELECT
                              class.class_id,
                              class.className,
                              class.classSection,
                              class.classCode,
                              CONCAT(teacher.teacherLastname, ', ', teacher.teacherFirstname, ' ', teacher.teacherMiddlename) AS teacherFullName
                            FROM student_class
                            JOIN class ON student_class.class_id = class.class_id
                            JOIN teacher ON class.teacher_id = teacher.teacher_id
                            WHERE student_class.student_id = @student_id AND class.classArchive = 'NO' LIMIT @startIndex, 5";

            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@startIndex", Math.Max(startIndex, 0));

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string className = reader.GetString("className");
                        classNames.Add(className);
                        string classCode = reader.GetString("classCode");
                        classCodes.Add(classCode);
                        string classSection = reader.GetString("classSection");
                        classSections.Add(classSection);
                        string teacherFullName = reader.GetString("teacherFullName");
                        classTeachers.Add(teacherFullName);
                        string classID = reader.GetString("class_id");
                        classIDs.Add(classID);
                    }
                }
            }

            db.CloseConnection();

            for (int i = 0; i < classNames.Count; i++)
            {
                Label label = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblSection" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblTeacher" + (i + 1), true).FirstOrDefault() as Label;
                Label label4 = Controls.Find("lblProf" + (i + 1), true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = classNames[i];
                    label1.Text = classCodes[i];
                    label2.Text = classSections[i];
                    label3.Text = classTeachers[i];                    
                }
                Button btnTask = Controls.Find("btnTask" + (i + 1), true).FirstOrDefault() as Button;
                if (btnTask != null)
                {
                    btnTask.Tag = classIDs[i];
                }
            }

            if (classNames.Count == 0)
            {
                MessageBox.Show("No more classes to display.");
                currentIndex -= 5; // Move the current index back to the previous set of data
            }
        }
        private void btnTask_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string classIDString = btn.Tag.ToString();
            if (int.TryParse(classIDString, out int classID))
            {
                TaskAssigned assign = new TaskAssigned(student_id, classID, desktopPanel);
                assign.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(assign);
                assign.Dock = DockStyle.Fill;
                assign.FormBorderStyle = FormBorderStyle.None;
                assign.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
