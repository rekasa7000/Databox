using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class TaskSubmitted : Form
    {
        int student_id, class_id, currentIndex, totalClasses;
        Panel desktopPanel;

        public TaskSubmitted(int studentId, int classID, Panel pan)
        {
            student_id = studentId;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
            taskLoad(0);
        }

        void taskLoad(int startIndex)
        {
            Database_Connection db = new Database_Connection();
            List<string> taskNames = new List<string>();
            List<string> taskStatus = new List<string>();
            List<string> taskScores = new List<string>();
            List<string> taskIDs = new List<string>();

            db.OpenConnection();

            string query = "SELECT t.task_id, t.taskName, t.taskPoints, s.submitStatus " +
                           "FROM task AS t " +
                           "JOIN class AS c ON t.class_id = c.class_id " +
                           "JOIN student_class AS sc ON c.class_id = sc.class_id " +
                           "LEFT JOIN submission AS s ON t.task_id = s.task_id AND s.student_id = sc.student_id " +
                           "WHERE sc.student_id = @student_id AND sc.class_id = @class_id " +
                           "AND s.submission_id IS NOT NULL " +
                           "LIMIT @startIndex, 5";
            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@student_id", student_id);
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
                        string taskScore = reader.GetString("taskPoints");
                        taskScores.Add(taskScore);
                        string status = reader.GetString("submitStatus");
                        taskStatus.Add(status);
                    }
                }
            }
            db.CloseConnection();

            for (int i = 0; i < taskNames.Count; i++)
            {
                Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                Label lblTask = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
                Label lblStatus = Controls.Find("lblStatus" + (i + 1), true).FirstOrDefault() as Label;
                Label lblScore = Controls.Find("lblScore" + (i + 1), true).FirstOrDefault() as Label;

                if (panel != null)
                {
                    panel.Visible = true;
                }

                if (lblTask != null)
                {
                    lblTask.Text = taskNames[i];
                }
                if (lblStatus != null)
                {
                    if (taskStatus[i] == "Late")
                    {
                        lblStatus.Text = "Done Late";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblStatus.Text = "Turned in";
                        lblStatus.ForeColor = Color.Teal;
                    }
                }
                if (lblScore != null)
                {
                    lblScore.Text = "/"+taskScores[i];
                }
                Button btnTask = Controls.Find("btnTask" + (i + 1), true).FirstOrDefault() as Button;
                if (btnTask != null)
                {
                    btnTask.Tag = taskIDs[i];
                }
            }
            if (taskNames.Count == 0)
            {
                MessageBox.Show("There are no Done tasks.");
                currentIndex -= 5;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            StudentClasses classStud = new StudentClasses(student_id, desktopPanel);
            classStud.TopLevel = false;
            desktopPanel.Controls.Clear();
            desktopPanel.Controls.Add(classStud);
            classStud.Dock = DockStyle.Fill;
            classStud.FormBorderStyle = FormBorderStyle.None;
            classStud.Show();
            this.Close();
        }

        private void btnAssigned_Click(object sender, EventArgs e)
        {
            TaskAssigned assign = new TaskAssigned(student_id, class_id, desktopPanel);
            assign.TopLevel = false;
            desktopPanel.Controls.Clear();
            desktopPanel.Controls.Add(assign);            
            assign.Dock = DockStyle.Fill;
            assign.FormBorderStyle = FormBorderStyle.None;
            assign.Show();
            this.Close();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentIndex -= 5;
            if (currentIndex < 0)
            {
                MessageBox.Show("You are already at the beginning.");
                currentIndex = 0;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Clear();
            currentIndex += 5;

            if (currentIndex >= totalClasses)
            {
                MessageBox.Show("No more classes to display.");
                currentIndex -= 5;
            }
        }
     
        private void TaskSubmitted_Load(object sender, EventArgs e)
        {
            CalculateTotalClasses();
            taskLoad(0);
        }
        private void btnTask_Clicked(object sender, EventArgs e)
        {
            Button lbl = (Button)sender;
            string taskIDString = lbl.Tag.ToString(); 

            if (int.TryParse(taskIDString, out int taskID))
            {
                // Pass the data to the ClassView form
                Task taskView = new Task(student_id, class_id, taskID, desktopPanel);
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
        void CalculateTotalClasses()
        {
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM task t " +
               "JOIN `class` AS c ON t.class_id = c.class_id " +
               "WHERE t.class_id = @class_id", db.connection))
            {
                countCmd.Parameters.AddWithValue("@class_id", class_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
        }

        void Clear()
        {
            for (int i = 0; i < 5; i++)
            {
                Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                Label label1 = Controls.Find("lblTask" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblType" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblTaskdue" + (i + 1), true).FirstOrDefault() as Label;
                Label label4 = Controls.Find("lblDueTime" + (i + 1), true).FirstOrDefault() as Label;
                Label label5 = Controls.Find("lblScore" + (i + 1), true).FirstOrDefault() as Label;

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
            }
        }
        private void btnMissing_Click(object sender, EventArgs e)
        {
            TaskMissing missing = new TaskMissing(student_id, class_id, desktopPanel);
            missing.TopLevel = false;
            desktopPanel.Controls.Add(missing);
            missing.Dock = DockStyle.Fill;
            missing.FormBorderStyle = FormBorderStyle.None;
            missing.Show();
            this.Close();
        }
    }
}
