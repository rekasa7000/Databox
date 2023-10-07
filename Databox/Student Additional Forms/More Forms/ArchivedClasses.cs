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

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class ArchivedClasses : Form
    {
        int student_id, currentIndex, totalClasses;
        Panel desktopPanel;

        public ArchivedClasses(int studentID, Panel pan)
        {
            student_id = studentID;
            desktopPanel = pan;
            InitializeComponent();
        }

        private void ArchivedClasses_Load(object sender, EventArgs e)
        {
            try
            {
                classLoad(0);
                CalculateTotalClasses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                currentIndex -= 5;
                if (currentIndex < 0)
                {
                    MessageBox.Show("You are already at the beginning.");
                    currentIndex = 0;
                }
                classLoad(currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void CalculateTotalClasses()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                db.OpenConnection();
                using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class " +
                   "JOIN student_class ON class.class_id = student_class.class_id " +
                   "WHERE student_class.student_id = @student_id AND class.classArchive = 'YES'", db.connection))
                {
                    countCmd.Parameters.AddWithValue("@student_id", student_id);
                    totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
                }
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                StudentClasses add = new StudentClasses(student_id, desktopPanel);
                add.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(add);
                add.Dock = DockStyle.Fill;
                add.FormBorderStyle = FormBorderStyle.None;
                add.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void Clear()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void classLoad(int startIndex)
        {
            try
            {
                Database_Connection db = new Database_Connection();
                List<string> classNames = new List<string>();
                List<string> classCodes = new List<string>();
                List<string> classSections = new List<string>();
                List<string> classTeachers = new List<string>();

                db.OpenConnection();

                // Create the SQL query
                string query = @"SELECT
                                  class.className,
                                  class.classSection,
                                  class.classCode,
                                  CONCAT(teacher.teacherLastname, ', ', teacher.teacherFirstname, ' ', teacher.teacherMiddlename) AS teacherFullName
                                FROM student_class
                                JOIN class ON student_class.class_id = class.class_id
                                JOIN teacher ON class.teacher_id = teacher.teacher_id
                                WHERE student_class.student_id = @student_id AND class.classArchive = 'YES' LIMIT @startIndex, 5";

                using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
                {
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);

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
                }

                if (classNames.Count == 0)
                {
                    MessageBox.Show("No more classes to display.");
                    currentIndex -= 5; // Move the current index back to the previous set of data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
