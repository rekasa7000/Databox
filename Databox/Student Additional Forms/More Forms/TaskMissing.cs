﻿using MySql.Data.MySqlClient;
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
    public partial class TaskMissing : Form
    {
        int student_id, class_id, currentIndex, totalClasses;
        Panel desktopPanel;

        public TaskMissing(int studentID, int classID, Panel pan)
        {
            student_id = studentID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssigned_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMissing_Click(object sender, EventArgs e)
        {
            try
            {
                taskLoad(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTask_Clicked(object sender, EventArgs e)
        {
            try
            {
                Button lbl = (Button)sender; // Get the clicked button
                string taskIDString = lbl.Tag.ToString(); // Retrieve the class_id from the button's Tag property

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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                TaskSubmitted submit = new TaskSubmitted(student_id, class_id, desktopPanel);
                submit.TopLevel = false;
                desktopPanel.Controls.Add(submit);
                submit.Dock = DockStyle.Fill;
                submit.FormBorderStyle = FormBorderStyle.None;
                submit.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                taskLoad(currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                currentIndex += 5;

                if (currentIndex >= totalClasses)
                {
                    MessageBox.Show("No more task missing.");
                    currentIndex -= 5;
                }
                taskLoad(currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CalculateTotalClasses()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                db.OpenConnection();
                using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM task t " +
                    "JOIN class AS c ON t.class_id = c.class_id " +
                    "WHERE t.class_id = @class_id " +
                    "AND CONCAT(t.taskDuedate, ' ', t.taskDuetime) > NOW()", db.connection))
                {
                    countCmd.Parameters.AddWithValue("@class_id", class_id);
                    totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
                }
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Clear()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                    Label label1 = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaskMissing_Load(object sender, EventArgs e)
        {
            try
            {
                taskLoad(0);
                CalculateTotalClasses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void taskLoad(int startIndex)
        {
            try {
            Database_Connection db = new Database_Connection();
            List<string> taskNames = new List<string>();
            List<string> taskTypes = new List<string>();
            List<string> taskDueDates = new List<string>();
            List<string> taskDueTimes = new List<string>();
            List<string> taskScores = new List<string>();
            List<string> taskIDs = new List<string>();

            db.OpenConnection();

            string query = "SELECT t.task_id, t.taskType, t.taskName, t.taskDescription, t.taskDuedate, t.taskDuetime, t.taskPoints, t.taskLink, t.taskArchive " +
                           "FROM task AS t " +
                           "JOIN class AS c ON t.class_id = c.class_id " +
                           "JOIN student_class AS sc ON c.class_id = sc.class_id " +
                           "LEFT JOIN submission AS s ON t.task_id = s.task_id AND s.student_id = sc.student_id " +
                           "WHERE sc.student_id = @student_id AND sc.class_id = @class_id " +
                           "AND CONCAT(t.taskDuedate, ' ', t.taskDuetime) < NOW() " +
                           "AND s.submission_id IS NULL " +
                           "AND (t.taskClass = 'YES' OR (t.taskClass = 'NO' OR t.task_id IN (SELECT task_id FROM task_student WHERE student_id = @student_id)))" +
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
                        string taskType = reader.GetString("taskType");
                        taskTypes.Add(taskType);
                        string taskDueDate = reader.GetDateTime("taskDueDate").ToString("yyyy-MM-dd");
                        taskDueDates.Add(taskDueDate);
                        string taskDueTimeStr = reader.GetString("taskDueTime");
                        DateTime dateTime = DateTime.ParseExact(taskDueTimeStr, "HH:mm:ss", CultureInfo.InvariantCulture);
                        string formattedTaskDueTime = dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        taskDueTimes.Add(formattedTaskDueTime);
                        string taskScore = reader.GetString("taskPoints");
                        taskScores.Add(taskScore);
                    }
                }
            }
            db.CloseConnection();

            for (int i = 0; i < taskIDs.Count; i++)
            {
                Panel panel = Controls.Find("classPanel" + (i + 1), true).FirstOrDefault() as Panel;
                Label label1 = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
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
                Button btnTask = Controls.Find("btnTask" + (i + 1), true).FirstOrDefault() as Button;
                if (btnTask != null)
                {
                    btnTask.Tag = taskIDs[i];
                }
                DateTime dueDateTime;
                bool isParsed = DateTime.TryParseExact(taskDueDates[i] + " " + taskDueTimes[i], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDateTime);

                if (isParsed)
                {
                    string formattedTaskDueTime = dueDateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                    taskDueTimes.Add(formattedTaskDueTime);

                    if (DateTime.Now > dueDateTime)
                    {
                        label3.ForeColor = Color.Red;
                        label4.ForeColor = Color.Red;
                    }
                }
                else
                {
                    Console.WriteLine("Failed to parse Task Due DateTime.");
                }
            }
            if (taskNames.Count == 0)
            {
                MessageBox.Show("There are no missing tasks.");
                currentIndex -= 5; // Move the current index back to the previous set of data
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
