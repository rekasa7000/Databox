using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class TaskView : Form
    {
        int teacher_id, class_id, currentIndex, totalClasses;
        Panel desktopPanel;
        string archive = "Archive this task";
        string edit = "Edit this task";
        public TaskView(int teacherID, int classID, Panel pan)
        {
            teacher_id = teacherID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
            for (int i = 1; i <= 5; i++)
            {
                Button button1 = Controls.Find("btnEdit" + i, true).FirstOrDefault() as Button;
                Button button2 = Controls.Find("btnArchive" + i, true).FirstOrDefault() as Button;
                if (button1 != null)
                {
                    ttEdit.SetToolTip(button1, edit);
                    ttArchive.SetToolTip(button2, archive);
                }
            }
        }

        private void btnCreateclass_Click(object sender, EventArgs e)
        {
            TaskAdd taskView = new TaskAdd(teacher_id, class_id, desktopPanel);
            taskView.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(taskView);
            taskView.Dock = DockStyle.Fill;
            taskView.FormBorderStyle = FormBorderStyle.None;
            taskView.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Classes classesForm = new Classes(teacher_id, desktopPanel);
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
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class WHERE teacher_id = @teacher_id AND classArchive = 'NO'", db.connection))
            {
                countCmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
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
                MessageBox.Show("No more tasks to display.");
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
            private void TaskView_Load(object sender, EventArgs e)
        {
            taskLoad(0);
            CalculateTotalClasses();
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

            // Create the SQL query
            string query = "SELECT t.task_id, t.taskName, t.taskType, t.taskDueDate, t.taskDueTime, t.taskPoints, COUNT(s.submission_id) AS submissionCount " +
                "FROM task t " +
                "LEFT JOIN submission s ON t.task_id = s.task_id " +
                "WHERE t.class_id = @class_id AND t.taskArchive = 'NO'" +
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
                        string taskDueTimeStr = reader.GetString("taskDueTime");
                        DateTime dateTime = DateTime.ParseExact(taskDueTimeStr, "HH:mm:ss", CultureInfo.InvariantCulture);
                        string taskDueTime = dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
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
                    label6.Tag = taskIDs[i];
                }
                Button btnEdit = Controls.Find("btnEdit" + (i + 1), true).FirstOrDefault() as Button;
                if (btnEdit != null)
                {
                    btnEdit.Tag = taskIDs[i];
                }
                Button btnArchive = Controls.Find("btnArchive" + (i + 1), true).FirstOrDefault() as Button;
                if (btnArchive != null)
                {
                    btnArchive.Tag = taskIDs[i];
                }
            }
            if (taskNames.Count == 0)
            {
                MessageBox.Show("No more task to display.");
                currentIndex -= 5; // Move the current index back to the previous set of data
            }
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            TaskArchive archive = new TaskArchive(teacher_id, class_id, desktopPanel);
            archive.TopLevel = false;
            desktopPanel.Controls.Clear();
            desktopPanel.Controls.Add(archive);
            archive.Dock = DockStyle.Fill;
            archive.FormBorderStyle = FormBorderStyle.None;
            archive.Show();
        }
        private void lblStudent_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string taskIDString = lbl.Tag.ToString();
            if (!string.IsNullOrEmpty(taskIDString))
            {
                if (int.TryParse(taskIDString, out int task_id))
                {
                    SubmittedTasks submit = new SubmittedTasks(class_id, task_id, teacher_id, desktopPanel);
                    submit.TopLevel = false;
                    desktopPanel.Controls.Clear(); 
                    desktopPanel.Controls.Add(submit);
                    submit.Dock = DockStyle.Fill;
                    submit.FormBorderStyle = FormBorderStyle.None;
                    submit.Show();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string taskIDString = btn.Tag.ToString();
            if (!string.IsNullOrEmpty(taskIDString))
            {

                if (int.TryParse(taskIDString, out int task_id))
                {
                    // Retrieve the task data from the database using the task_id
                    string query = $"SELECT taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskLink, taskFilename, taskDocument, taskClass FROM task WHERE task_id = {task_id};";

                    // Execute the query and retrieve the data
                    Database_Connection db = new Database_Connection();
                    MySqlDataReader dataReader = db.GetData(query);

                    if (dataReader.Read())
                    {
                        // Retrieve the data from the dataReader
                        string taskType = dataReader["taskType"].ToString();
                        string taskName = dataReader["taskName"].ToString();
                        string taskDescription = dataReader["taskDescription"].ToString();
                        DateTime taskDueDate = dataReader.GetDateTime("taskDuedate");
                        string taskDueTime = dataReader["taskDuetime"].ToString();
                        string taskPoints = dataReader["taskPoints"].ToString();
                        string taskLink = dataReader["taskLink"].ToString();
                        string fileName = dataReader["taskFilename"].ToString();
                        string taskClass = dataReader["taskClass"].ToString();



                        // Retrieve the task document information
                        byte[] taskDocumentBytes = dataReader["taskDocument"] as byte[];
                        TaskDocument taskDocument = null;
                        if (taskDocumentBytes != null)
                        {
                            taskDocument = new TaskDocument();
                            taskDocument.FileName = $"{fileName}";
                            taskDocument.FilePath = SaveTaskDocument(taskDocumentBytes, taskDocument.FileName);
                        }

                        // Pass the data to the TaskEdit form
                        TaskEdit edit = new TaskEdit(teacher_id, class_id, task_id, desktopPanel);
                        edit.TopLevel = false;
                        desktopPanel.Controls.Clear(); // Clear existing controls in desktopPanel
                        desktopPanel.Controls.Add(edit);
                        edit.Dock = DockStyle.Fill;
                        edit.FormBorderStyle = FormBorderStyle.None;
                        edit.LoadData(taskType, taskName, taskDescription, taskDueDate, taskDueTime, taskPoints, taskLink, taskDocument, taskClass); // Load the task data into the TaskEdit form
                        edit.Show();
                        taskLoad(currentIndex);
                    }
                    else
                    {
                        MessageBox.Show("Task not found in the database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    dataReader.Close(); // Close the data reader
                }
                else
                {
                    MessageBox.Show("Invalid Task ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
                MessageBox.Show("Task not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string SaveTaskDocument(byte[] documentBytes, string fileName)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskDocuments");
            string filePath = Path.Combine(directoryPath, fileName);

            // Create the directory if it doesn't exist
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(filePath, documentBytes);

            // Get the relative path of the saved file
            string relativePath = Path.Combine("TaskDocuments", fileName);
            return relativePath;
        }
        private void btnArchive_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to archive this task?", "Archive task?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Button btn = (Button)sender; 
                string task_id = btn.Tag.ToString(); 

                if (!string.IsNullOrEmpty(task_id))
                {
                    MessageBox.Show("Task Archived!", "Successful", MessageBoxButtons.OK);
                    Database_Connection db = new Database_Connection();

                    // Construct query to update class details in the database
                    string query = $"UPDATE task " +
                                    $"SET taskArchive = 'YES' " +
                                    $"WHERE task_id = '{task_id}';";

                    // Execute query
                    db.ExecuteQuery(query);
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
                // Do nothing or perform a different action
            }
        }
    }
}
