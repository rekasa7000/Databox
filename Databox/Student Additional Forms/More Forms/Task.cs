using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databox.Controls;
using FontAwesome.Sharp;

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class Task : Form
    {
        int student_id, class_id, task_id, teacher_id, panIndex;
        byte[] file;
        string namefile;
        Panel desktopPanel;
        TaskDocument taskDocument;
        byte[] subDoc1 = null, subDoc2 = null, subDoc3 = null, subDoc4 = null, subDoc5 = null;
        string fileName1 = null, fileName2 = null, fileName3 = null, fileName4 = null, fileName5 = null;
        object from;
        public Task(int studentID, int classID, int taskID, Panel pan)
        {
            student_id = studentID;
            class_id = classID;
            task_id = taskID;
            desktopPanel = pan;            
            InitializeComponent();
            getTeacherID();
            RetrieveStudentname();
            loadTitle(); loadClass(); loadData(); retrieveComment(); submission(); 
            for (int i = 0; i < 5; i++)
            {
                Button btn = Controls.Find("btnAdd" + (i + 1), true).FirstOrDefault() as Button;
                if (btn != null)
                {
                    btn.Size = new Size(383, 35);
                }
            }
            
        }

        void getTeacherID()
        {
            try
            {
                Database_Connection db = new Database_Connection();

                string query = "SELECT t.teacher_id " +
                    "FROM class c " +
                    "JOIN teacher t ON c.teacher_id = t.teacher_id " +
                    "WHERE c.class_id = @class_id;";

                MySqlCommand command = new MySqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@class_id", class_id);

                db.OpenConnection();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    teacher_id = reader.GetInt32("teacher_id");
                }
                reader.Close();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Task_Load(object sender, EventArgs e)
        {
            try
            {
                loadTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                TaskAssigned tasks = new TaskAssigned(student_id, class_id, desktopPanel);
                tasks.TopLevel = false;
                desktopPanel.Controls.Clear();
                desktopPanel.Controls.Add(tasks);
                tasks.Dock = DockStyle.Fill;
                tasks.FormBorderStyle = FormBorderStyle.None;
                tasks.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void loadClass()
        {
            try
            {
                Database_Connection db = new Database_Connection();

                string query = "SELECT className, classCode, classSection FROM class WHERE class_id = @class_id";
                MySqlCommand command = new MySqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@class_id", class_id);

                db.OpenConnection();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    string code = reader.GetString(1);
                    string section = reader.GetString(2);

                    string topLabel = $"{name} \n{section} [{code}] ";
                    lblClass.Text = topLabel;

                }
                reader.Close();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void lblFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(taskDocument.FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://" + lblLink.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void loadData()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                db.OpenConnection();
                string query = "SELECT CONCAT(t.teacherFirstname, ' ', t.teacherLastname) as teacher, task.taskDuedate, task.taskDuetime, task.taskCreate, taskPoints, taskDescription," +
                    "taskLink, taskFilename, taskDocument " +
                    "FROM teacher AS t " +
                    "JOIN class AS c ON t.teacher_id = c.teacher_id " +
                    "JOIN task ON c.class_id = task.class_id " +
                    "WHERE task.task_id = @task_id";
                MySqlCommand command = new MySqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@task_id", task_id);

                db.OpenConnection();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string teacher = reader.GetString(0);
                    string taskDuedate = reader.GetString(1);
                    string taskDueTimeStr = reader.GetString("taskDueTime");
                    DateTime dateTime = DateTime.ParseExact(taskDueTimeStr, "HH:mm:ss", CultureInfo.InvariantCulture);
                    string taskDueTime = dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                    DateTime taskCreated = reader.GetDateTime(3);
                    string taskPoints = reader.GetString(4);
                    string taskDescription = reader.GetString(5);
                    string taskLink = reader.IsDBNull(6) ? null : reader.GetString(6);
                    string taskFilename = reader.IsDBNull(7) ? null : reader.GetString(7);
                    byte[] taskDocumentBytes = null;
                    if (!reader.IsDBNull(8))
                    {
                        taskDocumentBytes = (byte[])reader["taskDocument"];
                    }
                    taskDocument = null;
                    if (taskDocumentBytes != null)
                    {
                        taskDocument = new TaskDocument();
                        taskDocument.FileName = $"{taskFilename}";
                        taskDocument.FilePath = SaveTaskDocument(taskDocumentBytes, taskDocument.FileName);
                    }
                    string dateCreated = taskCreated.ToString("MMMM d");

                    lblFilename.Text = taskFilename;
                    lblLink.Text = taskLink;
                    namefile = taskFilename;
                    file = taskDocumentBytes;
                    //Display taskDescription to richTextbox
                    RichTextBox tempRichTextBox = new RichTextBox();
                    tempRichTextBox.Rtf = taskDescription;
                    tempRichTextBox.SelectAll();
                    tempRichTextBox.SelectionColor = Color.White;
                    string modifiedRtf = tempRichTextBox.Rtf;
                    desc.Rtf = modifiedRtf;

                    DateTime dueDate = DateTime.Parse(taskDuedate);

                    DateTime currentWeekMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                    if (dueDate >= currentWeekMonday && dueDate <= currentWeekMonday.AddDays(6))
                    {
                        string formattedDuedate = $"{dueDate.ToString("dddd", CultureInfo.CurrentCulture)}";
                        string due = $"Due {formattedDuedate}, {taskDueTime}";
                        lblDue.Text = due;
                    }
                    else
                    {
                        string formattedDuedate = $"{dueDate.ToString("MMMM d, yyyy")}";
                        string due = $"{formattedDuedate} {taskDueTime}";
                        lblDue.Text = due;
                    }
                    string points = $"{taskPoints} Points";
                    lblPoints.Text = points;
                    string topLabel = $"{teacher} • {dateCreated} ";
                    lblTeacher.Text = topLabel;

                    DateTime taskDueDateTime;

                    if (!string.IsNullOrEmpty(taskDuedate) && !string.IsNullOrEmpty(taskDueTime))
                    {
                        if (DateTime.TryParse(taskDuedate, out DateTime dueDate1) && DateTime.TryParse(taskDueTime, out DateTime dueTime))
                        {
                            taskDueDateTime = dueDate1.Date + dueTime.TimeOfDay;

                            if (taskDueDateTime < DateTime.Now)
                            {
                                lblStatus.Text = "Missing";
                                lblStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                lblStatus.Text = "Assigned";
                                lblStatus.ForeColor = Color.Teal;
                            }
                        }
                        else
                        {
                            // Invalid date or time format
                            lblStatus.Text = "Invalid Due Date/Time";
                        }
                    }
                    else
                    {
                        // Either taskDuedate or taskDuetime is empty or null
                        lblStatus.Text = "Invalid Due Date/Time";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;

                if (textBox.Text == "Enter your comment here...")
                {
                    textBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;

                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Enter your comment here...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void addLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddLink add = new AddLink();
                Button btn = (Button)from;
                Panel parent = Controls.Find("panelAdd" + panIndex, true).FirstOrDefault() as Panel;
                add.LinkAdded += (linkValue) => Add_LinkAdded(linkValue, btn, parent);
                add.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel5_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd5.Size = new Size(383, 35);
                btnAdd5.Text = "Add link or Attach file";
                panelAdd5.Visible = false;
                fileName5 = null;
                subDoc5 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel4_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd4.Size = new Size(383, 35);
                btnAdd4.Text = "Add link or Attach file";
                panelAdd4.Visible = false;
                fileName4 = null;
                subDoc4 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel3_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd3.Size = new Size(383, 35);
                btnAdd3.Text = "Add link or Attach file";
                panelAdd3.Visible = false;
                fileName3 = null;
                subDoc3 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd2.Size = new Size(383, 35);
                btnAdd2.Text = "Add link or Attach file";
                panelAdd2.Visible = false;
                fileName2 = null;
                subDoc2 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd1.Size = new Size(383, 35);
                btnAdd1.Text = "Add link or Attach file";
                fileName1 = null;
                subDoc1 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        int submissionId;
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Submit")
                {
                    Database_Connection db = new Database_Connection();
                    string insertSubmissionQuery = "INSERT INTO `submission` (`submitStatus`, `task_id`, `student_id`) VALUES (@status, @task_id, @student_id)";
                    string insertSubmitFilesQuery = "INSERT INTO `submitfiles` (`submission_id`, `submitLink`, `submitFilename`, `submitDocument`) VALUES (@submission_id, @submitLink, @submitFilename, @submitDocument)";

                    using (MySqlCommand insertSubmissionCmd = new MySqlCommand(insertSubmissionQuery, db.connection))
                    using (MySqlCommand insertSubmitFilesCmd = new MySqlCommand(insertSubmitFilesQuery, db.connection))
                    {
                        string status = GetSubmitStatus(); // Get the submit status based on lblStatus.Text

                        insertSubmissionCmd.Parameters.AddWithValue("@status", status);
                        insertSubmissionCmd.Parameters.AddWithValue("@task_id", task_id); // Replace with the actual task ID
                        insertSubmissionCmd.Parameters.AddWithValue("@student_id", student_id); // Replace with the actual student ID

                        db.OpenConnection();
                        insertSubmissionCmd.ExecuteNonQuery();

                        // Retrieve the auto-generated submission ID
                        submissionId = (int)insertSubmissionCmd.LastInsertedId;

                        // Insert submit files into the submitfiles table for btnAdd1
                        if (btnAdd1.Text != "Add link or Attach file" || fileName1 != null || subDoc1 != null)
                        {
                            insertSubmitFilesCmd.Parameters.Clear();
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submission_id", submissionId);
                            if (btnAdd1.Text.Contains("https://"))
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", btnAdd1.Text);
                            }
                            else
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", null);
                            }
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitFilename", fileName1);
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitDocument", subDoc1);

                            insertSubmitFilesCmd.ExecuteNonQuery();
                            panelAdd2.Visible = false;
                        }

                        // Insert submit files into the submitfiles table for btnAdd2
                        if (btnAdd2.Text != "Add link or Attach file" || fileName2 != null || subDoc2 != null)
                        {
                            insertSubmitFilesCmd.Parameters.Clear();
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submission_id", submissionId);
                            if (btnAdd2.Text.Contains("https://"))
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", btnAdd2.Text);
                            }
                            else
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", null);
                            }
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitFilename", fileName2);
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitDocument", subDoc2);

                            insertSubmitFilesCmd.ExecuteNonQuery();
                            panelAdd3.Visible = false;
                        }
                        if (btnAdd3.Text != "Add link or Attach file" || fileName3 != null || subDoc3 != null)
                        {
                            insertSubmitFilesCmd.Parameters.Clear();
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submission_id", submissionId);
                            if (btnAdd3.Text.Contains("https://"))
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", btnAdd3.Text);
                            }
                            else
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", null);
                            }
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitFilename", fileName3);
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitDocument", subDoc3);

                            insertSubmitFilesCmd.ExecuteNonQuery();
                            panelAdd4.Visible = false;
                        }
                        if (btnAdd4.Text != "Add link or Attach file" || fileName4 != null || subDoc4 != null)
                        {
                            insertSubmitFilesCmd.Parameters.Clear();
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submission_id", submissionId);
                            if (btnAdd4.Text.Contains("https://"))
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", btnAdd4.Text);
                            }
                            else
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", null);
                            }
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitFilename", fileName4);
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitDocument", subDoc4);

                            insertSubmitFilesCmd.ExecuteNonQuery();
                            panelAdd5.Visible = false;
                        }
                        if (btnAdd5.Text != "Add link or Attach file" || fileName5 != null || subDoc5 != null)
                        {
                            insertSubmitFilesCmd.Parameters.Clear();
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submission_id", submissionId);
                            if (btnAdd5.Text.Contains("https://"))
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", btnAdd5.Text);
                            }
                            else
                            {
                                insertSubmitFilesCmd.Parameters.AddWithValue("@submitLink", null);
                            }
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitFilename", fileName5);
                            insertSubmitFilesCmd.Parameters.AddWithValue("@submitDocument", subDoc5);

                            insertSubmitFilesCmd.ExecuteNonQuery();
                        }
                        db.CloseConnection();

                        btnSubmit.Text = "Unsubmit";
                        btnSubmit.BackColor = Color.FromArgb(49, 51, 56);
                        for (int i = 0; i < 5; i++)
                        {
                            Button btn = Controls.Find("btnAdd" + (i + 1), true).FirstOrDefault() as Button;
                            if (btn != null)
                            {
                                btn.Size = new Size(383, 35);
                            }
                        }
                        MessageBox.Show("Submission recorded successfully.");
                    }
                }
                else
                {
                    Database_Connection db = new Database_Connection();
                    string deleteSubmitFilesQuery = "DELETE FROM `submission` WHERE `task_id` = @task_id AND student_id = @student_id";

                    using (MySqlCommand deleteSubmitFilesCmd = new MySqlCommand(deleteSubmitFilesQuery, db.connection))
                    {
                        deleteSubmitFilesCmd.Parameters.AddWithValue("@task_id", task_id);
                        deleteSubmitFilesCmd.Parameters.AddWithValue("@student_id", student_id);

                        db.OpenConnection();
                        deleteSubmitFilesCmd.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    btnSubmit.BackColor = Color.Teal;
                    btnSubmit.Text = "Submit";
                    loadData();
                    for (int i = 0; i < 5; i++)
                    {
                        Button btn = Controls.Find("btnAdd" + (i + 1), true).FirstOrDefault() as Button;
                        if (btn != null)
                        {
                            btn.Size = new Size(350, 35);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        string GetSubmitStatus()
        {
            string status = null;

            if (lblStatus.Text == "Assigned")
            {
                status = "On-time";
            }
            else if (lblStatus.Text == "Missing")
            {
                status = "Late";
            }

            return status;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComment.Text != "Enter your comment here...")
                {
                    InsertComment();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void InsertComment()
        {
            try
            {
                string text = txtComment.Text;
                Database_Connection db = new Database_Connection();
                string insertQuery = "INSERT INTO `task_comment` (`commentName`, `task_id`, `comment`, `student_id`, `teacher_id`) " +
                    "VALUES (@name, @task_id, @comment, @student_id, @teacher_id)";

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, db.connection))
                {
                    insertCmd.Parameters.AddWithValue("@name", name);
                    insertCmd.Parameters.AddWithValue("@task_id", task_id);
                    insertCmd.Parameters.AddWithValue("@comment", text);
                    insertCmd.Parameters.AddWithValue("@student_id", student_id);
                    insertCmd.Parameters.AddWithValue("@teacher_id", teacher_id);

                    db.OpenConnection();
                    insertCmd.ExecuteNonQuery();
                    db.CloseConnection();
                    retrieveComment();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        string name;
        void RetrieveStudentname()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                string selectQuery = "SELECT CONCAT(studFirstname, ' ', studLastname) as name FROM student WHERE student_id = @student_id";

                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, db.connection))
                {
                    selectCmd.Parameters.AddWithValue("@student_id", student_id);

                    db.OpenConnection();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            name = reader.GetString("name");
                        }
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        void retrieveComment()
        {
            try
            {
                panelComments.Controls.Clear();
                Database_Connection db = new Database_Connection();
                string selectQuery = @"
SELECT c.comment AS comment,
       c.commentName,
       c.commentTimestamp
FROM task_comment c
LEFT JOIN student s ON c.student_id = s.student_id
LEFT JOIN teacher t ON c.teacher_id = t.teacher_id
WHERE c.task_id = @task_id AND s.student_id = @student_id AND t.teacher_id = @teacher_id AND commentName = @name";

                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, db.connection))
                {
                    selectCmd.Parameters.AddWithValue("@task_id", task_id);
                    selectCmd.Parameters.AddWithValue("@student_id", student_id);
                    selectCmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                    selectCmd.Parameters.AddWithValue("@name", name);

                    db.OpenConnection();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string commentText = reader.GetString("comment");
                            string name = reader.GetString("commentName");
                            string time = reader.GetString("commentTimestamp");

                            Panel commentPanel = new Panel();
                            commentPanel.BorderStyle = BorderStyle.None;
                            commentPanel.Dock = DockStyle.Top;
                            commentPanel.AutoSize = true;
                            commentPanel.Padding = new Padding(0, 0, 0, 5);

                            Label nameLabel = new Label();
                            nameLabel.Text = name;
                            nameLabel.Font = new Font("Nirmala UI", 8, FontStyle.Italic);
                            nameLabel.ForeColor = Color.LightBlue;
                            nameLabel.Location = new Point(0, 0);

                            Label timeLabel = new Label();
                            timeLabel.Text = time;
                            timeLabel.Font = new Font("Nirmala UI", 8, FontStyle.Italic);
                            timeLabel.ForeColor = Color.LightBlue;
                            timeLabel.Location = new Point(nameLabel.Right, 0);

                            Label commentLabel = new Label();
                            commentLabel.Text = commentText;
                            commentLabel.Font = new Font("Nirmala UI", 12, FontStyle.Regular);
                            commentLabel.ForeColor = Color.Gainsboro;
                            commentLabel.Location = new Point(0, nameLabel.Bottom);
                            commentLabel.AutoSize = true;
                            commentLabel.MaximumSize = new Size(panelComments.Width, 0);

                            commentPanel.Controls.Add(commentLabel);
                            commentPanel.Controls.Add(timeLabel);
                            commentPanel.Controls.Add(nameLabel);

                            panelComments.Controls.Add(commentPanel);
                            txtComment.Text = "";
                        }


                        if (!reader.HasRows)
                        {
                            txtComment.Text = "Enter your comment here...";
                        }
                    }

                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void submission()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                string selectQuery = "SELECT submit.submitStatus " +
                    "FROM submission submit " +
                    "JOIN task ON task.task_id = submit.task_id " +
                    "WHERE task.task_id = @task_id AND submit.student_id = @student_id";

                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, db.connection))
                {
                    selectCmd.Parameters.AddWithValue("@task_id", task_id);
                    selectCmd.Parameters.AddWithValue("@student_id", student_id);

                    db.OpenConnection();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        bool hasSubmission = reader.HasRows;

                        if (hasSubmission)
                        {
                            reader.Read();
                            string submitStatus = reader.GetString("submitStatus");

                            if (submitStatus == "Late")
                            {
                                lblStatus.Text = "Done Late";
                                lblStatus.ForeColor = Color.Red;
                            }
                            else
                            {
                                lblStatus.Text = "Turned in";
                                lblStatus.ForeColor = Color.Teal;
                            }

                            RetrieveSubmittedFiles(true);
                        }
                        else
                        {
                            btnSubmit.Text = "Submit";
                            btnSubmit.BackColor = Color.Teal;
                        }
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void RetrieveSubmittedFiles(bool hasSubmission)
        {
            try
            {
                Database_Connection db = new Database_Connection();
                string selectQuery = "SELECT submit.submitStatus, subfile.submitLink, subfile.submitFilename, subfile.submitDocument " +
                    "FROM submission submit " +
                    "JOIN submitfiles AS subfile ON submit.submission_id = subfile.submission_id " +
                    "JOIN task ON task.task_id = submit.task_id " +
                    "WHERE task.task_id = @task_id AND submit.student_id = @student_id";

                using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, db.connection))
                {
                    selectCmd.Parameters.AddWithValue("@task_id", task_id);
                    selectCmd.Parameters.AddWithValue("@student_id", student_id);

                    db.OpenConnection();
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        int panelCount = 1;

                        while (reader.Read())
                        {
                            string submitFilename = reader.IsDBNull(reader.GetOrdinal("submitFilename")) ? null : reader.GetString("submitFilename");
                            string submitLink = reader.IsDBNull(reader.GetOrdinal("submitLink")) ? null : reader.GetString("submitLink");
                            string submitStatus = reader.GetString("submitStatus");

                            if (submitStatus == "Late")
                            {
                                lblStatus.Text = "Done Late";
                            }
                            else
                            {
                                lblStatus.Text = "Turned in";
                            }

                            if (!string.IsNullOrEmpty(submitLink) || !string.IsNullOrEmpty(submitFilename))
                            {
                                if (panelCount <= 5)
                                {
                                    Panel panel = Controls.Find("panelAdd" + (panelCount), true).FirstOrDefault() as Panel;
                                    panel.Visible = true;

                                    Button button = Controls.Find("btnAdd" + (panelCount), true).FirstOrDefault() as Button;
                                    button.Text = submitLink ?? submitFilename;
                                    panelCount++;
                                }
                            }
                            else
                            {
                                if (btnAdd1.Text == "Add link or Attach file")
                                {
                                    panelAdd1.Visible = false;
                                }
                            }

                        }

                        if (hasSubmission)
                        {
                            btnSubmit.Text = "Unsubmit";
                            btnSubmit.BackColor = Color.FromArgb(49, 51, 56);
                        }
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Add_LinkAdded(string link, Button senderButton, Panel pan)
        {
            try
            {
                senderButton.Text = "https://" + link;
                if (pan != null)
                {
                    pan.Visible = true;

                }
                btnPressed(from);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void attachFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFile.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx";
                openFile.ShowDialog();
                Button btn = (Button)from;

                string filePath = openFile.FileName;

                btn.Text = filePath;

                byte[] doc = File.ReadAllBytes(filePath);
                int i = panIndex - 1;
                switch (i)
                {
                    case 1:
                        subDoc1 = doc;
                        fileName1 = filePath;
                        break;
                    case 2:
                        subDoc2 = doc;
                        fileName2 = filePath;
                        break;
                    case 3:
                        subDoc3 = doc;
                        fileName3 = filePath;
                        break;
                    case 4:
                        subDoc4 = doc;
                        fileName4 = filePath;
                        break;
                    case 5:
                        subDoc5 = doc;
                        fileName5 = filePath;
                        break;
                    default:
                        break;
                }
                btnPressed(from);
                panelShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        void panelShow()
        {
            try
            {
                Panel parent = Controls.Find("panelAdd" + panIndex, true).FirstOrDefault() as Panel;
                if (parent != null)
                {
                    parent.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void btnPressed(object sender)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Text != "Add link or Attach file")
                {
                    btn.Size = new Size(350, 35);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                panIndex = 0;
                if (btnAdd1.Text.Contains("https://"))
                {
                    System.Diagnostics.Process.Start(btnAdd1.Text);
                }
                else if (btnAdd1.Text == "Add link or Attach file")
                {
                    addMenu.Show(btnAdd1, new Point(0, btnAdd1.Height));
                    panIndex = 2;
                    from = sender;
                }
                else
                {
                    System.Diagnostics.Process.Start(btnAdd1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                panIndex = 0;
                if (btnAdd2.Text.Contains("https://"))
                {
                    System.Diagnostics.Process.Start(btnAdd2.Text);
                }
                else if (btnAdd2.Text == "Add link or Attach file")
                {
                    addMenu.Show(btnAdd2, new Point(0, btnAdd2.Height));
                    panIndex = 3;
                    from = sender;
                }
                else
                {
                    System.Diagnostics.Process.Start(btnAdd2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                panIndex = 0;
                if (btnAdd3.Text.Contains("https://"))
                {
                    System.Diagnostics.Process.Start(btnAdd3.Text);
                }
                else if (btnAdd3.Text == "Add link or Attach file")
                {
                    addMenu.Show(btnAdd1, new Point(0, btnAdd3.Height));
                    panIndex = 4;
                    from = sender;
                }
                else
                {
                    System.Diagnostics.Process.Start(btnAdd3.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnAdd4_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                panIndex = 0;
                if (btnAdd4.Text.Contains("https://"))
                {
                    System.Diagnostics.Process.Start(btnAdd4.Text);
                }
                else if (btnAdd4.Text == "Add link or Attach file")
                {
                    addMenu.Show(btnAdd4, new Point(0, btnAdd4.Height));
                    panIndex = 5;
                    from = sender;
                }
                else
                {
                    System.Diagnostics.Process.Start(btnAdd4.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnAdd5_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                panIndex = 0;
                if (btnAdd5.Text.Contains("https://"))
                {
                    System.Diagnostics.Process.Start(btnAdd5.Text);
                }
                else if (btnAdd5.Text == "Add link or Attach file")
                {
                    addMenu.Show(btnAdd5, new Point(0, btnAdd5.Height));
                    panIndex = 6;
                    from = sender;
                }
                else
                {
                    System.Diagnostics.Process.Start(btnAdd5.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private string SaveTaskDocument(byte[] documentBytes, string fileName)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskDocuments");
            string filePath = Path.Combine(directoryPath, fileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(filePath, documentBytes);
            string relativePath = Path.Combine("TaskDocuments", fileName);
            return relativePath;
        }

        void loadTitle()
        {
            try
            {
                Database_Connection db = new Database_Connection();

                string query = "SELECT taskType, taskName FROM task WHERE task_id = @task_id";
                MySqlCommand command = new MySqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@task_id", task_id);

                db.OpenConnection();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string taskType = reader.GetString(0);
                    string taskName = reader.GetString(1);

                    string topLabel = $"[{taskType}] {taskName}";
                    lblTitle.Text = topLabel;

                }
                reader.Close();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
