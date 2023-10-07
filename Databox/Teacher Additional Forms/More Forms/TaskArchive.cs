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
    public partial class TaskArchive : Form
    {
        int teacher_id, class_id, currentIndex = 0, totalClasses;
        Panel desktopPanel;
        string archive = "Unarchive this task";
        public TaskArchive(int teacherID, int classID, Panel pan)
        {
            teacher_id = teacherID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
            for (int i = 1; i <= 5; i++)
            {
                Button button2 = Controls.Find("btnArchive" + i, true).FirstOrDefault() as Button;
                if (button2 != null)
                {
                    ttArchive.SetToolTip(button2, archive);
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentIndex -= 5; // Decrement the current index by 5
            if (currentIndex < 0)
            {
                MessageBox.Show("You are already at the beginning.");
                currentIndex = 0; // Ensure the index doesn't go below 0
            }
            taskLoad(currentIndex);
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
            Console.WriteLine(currentIndex);
            taskLoad(currentIndex);
        }

        void Clear()
        {
            for (int i = 0; i < 5; i++)
            {
                Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                Label label1 = Controls.Find("lblTask" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblType" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblDueDate" + (i + 1), true).FirstOrDefault() as Label;
                Label label4 = Controls.Find("lblDueTime" + (i + 1), true).FirstOrDefault() as Label;
                Label label5 = Controls.Find("lblScore" + (i + 1), true).FirstOrDefault() as Label;
                Label label6 = Controls.Find("lblStudent" + (i + 1), true).FirstOrDefault() as Label;

                if (panel != null)
                {
                    panel.Visible = true;
                }

                if (label1 != null)
                {
                    label1.Text = "";
                }

                if (label2 != null)
                {
                    label2.Text = "";
                }

                if (label3 != null)
                {
                    label3.Text = "";
                }

                if (label4 != null)
                {
                    label4.Text = "";
                }

                if (label5 != null)
                {
                    label5.Text = "";
                }

                if (label6 != null)
                {
                    label6.Text = "0";
                }
            }
        }

        void taskLoad(int startIndex)
        {
            Database_Connection db = new Database_Connection();
            List<string> taskNames = new List<string>();
            List<string> taskTypes = new List<string>();
            List<string> taskDueDates = new List<string>();
            List<string> taskDueTimes = new List<string>();
            List<string> taskScores = new List<string>();
            List<int> taskSubmissionCounts = new List<int>();
            List<string> taskIDs = new List<string>();

            db.OpenConnection();

            string query = "SELECT t.task_id, t.taskName, t.taskType, t.taskDueDate, t.taskDueTime, t.taskPoints, COUNT(s.submission_id) AS submissionCount " +
                "FROM task t " +
                "LEFT JOIN submission s ON t.task_id = s.task_id " +
                "WHERE t.class_id = @class_id AND t.taskArchive = 'YES' " +
                "GROUP BY t.task_id " +
                "LIMIT @startIndex, 5";

            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@startIndex", startIndex);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string taskID = reader.GetString("task_id");
                        taskIDs.Add(taskID);
                        string taskName = reader.GetString("taskName");
                        taskNames.Add(taskName);
                        string taskType = reader.GetString("taskType");
                        taskTypes.Add(taskType);
                        string taskDueDate = reader.GetDateTime("taskDueDate").ToString("yyyy-MM-dd");
                        taskDueDates.Add(taskDueDate);
                        string taskDueTime = reader.GetString("taskDueTime");
                        taskDueTimes.Add(taskDueTime);
                        string taskScore = reader.GetString("taskPoints");
                        taskScores.Add(taskScore);
                        int submissionCount = reader.GetInt32("submissionCount");
                        taskSubmissionCounts.Add(submissionCount);
                    }
                }
            }

            db.CloseConnection();

            for (int i = 0; i < taskNames.Count; i++)
            {
                Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                Label label1 = Controls.Find("lblTask" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblType" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblDueDate" + (i + 1), true).FirstOrDefault() as Label;
                Label label4 = Controls.Find("lblDueTime" + (i + 1), true).FirstOrDefault() as Label;
                Label label5 = Controls.Find("lblScore" + (i + 1), true).FirstOrDefault() as Label;
                Label label6 = Controls.Find("lblStudent" + (i + 1), true).FirstOrDefault() as Label;

                if (panel != null)
                {
                    panel.Visible = true;
                }

                if (label1 != null)
                {
                    label1.Text = taskNames[i];
                }

                if (label2 != null)
                {
                    label2.Text = taskTypes[i];
                }

                if (label3 != null)
                {
                    label3.Text = taskDueDates[i];
                }

                if (label4 != null)
                {
                    label4.Text = taskDueTimes[i];
                }

                if (label5 != null)
                {
                    label5.Text = taskScores[i];
                }

                if (label6 != null)
                {
                    label6.Text = taskSubmissionCounts[i].ToString();
                }
                Button btnArchive = Controls.Find("btnArchive" + (i + 1), true).FirstOrDefault() as Button;
                if (btnArchive != null)
                {
                    btnArchive.Tag = taskIDs[i];
                }
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to unarchive this task?", "Unarchive task?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Button btn = (Button)sender;
                string task_id = btn.Tag.ToString();

                if (!string.IsNullOrEmpty(task_id))
                {
                    
                    Database_Connection db = new Database_Connection();

                    // Construct query to update class details in the database
                    string query = $"UPDATE task " +
                                    $"SET taskArchive = 'NO' " +
                                    $"WHERE class_id = '{class_id}' and task_id = '{task_id}';";

                    // Execute query
                    db.ExecuteQuery(query);
                    MessageBox.Show("Task Unarchived!", "Successful", MessageBoxButtons.OK);
                    Clear();
                    taskLoad(currentIndex);
                }
                else
                {
                    MessageBox.Show("Invalid task ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            TaskView classesForm = new TaskView(teacher_id, class_id, desktopPanel);
            classesForm.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(classesForm);
            classesForm.Dock = DockStyle.Fill;
            classesForm.FormBorderStyle = FormBorderStyle.None;
            classesForm.Show();
            this.Close();
        }
        void CalculateTotalClasses()
        {
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class WHERE teacher_id = @teacher_id AND classArchive = 'YES'", db.connection))
            {
                countCmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
        }

        private void TaskArchive_Load(object sender, EventArgs e)
        {
            try {
                taskLoad(0);
                CalculateTotalClasses();
            } catch (Exception ex)
            { Console.WriteLine(ex); }
        }
    }
}
