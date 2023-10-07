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
using Databox.Controls;
using MySql.Data.MySqlClient;
using HtmlAgilityPack;
using System.Globalization;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class TaskAdd : Form
    {
        int teacher_id, class_id, taskPoints;
        string taskType, taskName, taskDescription, taskDuedate, taskDuetime,  taskLink, taskFilename, teacherName, className, section, taskDueTimeStr;
        byte[] taskDocument;
        Panel desktopPanel;
        public TaskAdd(int teacherID, int classID, Panel pan)
        {
            teacher_id = teacherID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
            tpDue.Format = DateTimePickerFormat.Custom;
            tpDue.CustomFormat = "hh:mm tt";
            tpDue.Value = DateTime.Today.AddHours(0);
            checklistFill();
            CheckAllItems();
            getTeacherName();
            getClassName();
        }



        void checklistFill()
        {
            string query = "SELECT CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) AS fullName " +
                "FROM student s " +
                "JOIN student_class sc ON s.student_id = sc.student_id " +
                "JOIN class c ON sc.class_id = c.class_id " +
                $"WHERE c.class_id = {class_id} " +
                "ORDER BY fullName";

            Database_Connection dbConnection = new Database_Connection();
            DataTable dataTable = dbConnection.ExecuteSelectQuery(query);

            // Clear existing items in the checkboxList
            checkboxList.Items.Clear();

            // Add items to the checkboxList
            foreach (DataRow row in dataTable.Rows)
            {
                string fullName = row["fullName"].ToString();
                checkboxList.Items.Add(fullName);
            }
        }


        void Fill()
        {
            taskType = cmbTasktype.SelectedItem.ToString();
            taskName = txtTitle.Text;
            taskDescription = richTextBox.Rtf;
            bool isValidPoints = int.TryParse(txtPoints.Text, out taskPoints);
            if (!isValidPoints || taskPoints <= 0)
            {
                MessageBox.Show("Please enter a valid number for the task score.", "Invalid Input", MessageBoxButtons.OK);
                return;
            }
            taskDuedate = dtpDue.Value.ToString("yyyy-MM-dd");
            taskDueTimeStr = tpDue.Value.ToString("hh:mm tt");
            DateTime dateTime = DateTime.ParseExact(taskDueTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);
            taskDuetime = dateTime.ToString("HH:mm", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(lblLink.Text))
            {
                taskLink = lblLink.Text;
            }
            if (!string.IsNullOrEmpty(lblFilename.Text))
            {
                string filePath = lblFilename.Text;
                string fileName = Path.GetFileName(filePath);
                taskDocument = File.ReadAllBytes(filePath);
                taskFilename = fileName;
            }
        }

        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAll.Checked)
            {
                CheckAllItems();
            }
            else
            {
                UncheckAllItems();
            }
        }

        void CheckAllItems()
        {
            for (int i = 0; i < checkboxList.Items.Count; i++)
            {
                checkboxList.SetItemChecked(i, true);
            }
        }

        void UncheckAllItems()
        {
            for (int i = 0; i < checkboxList.Items.Count; i++)
            {
                checkboxList.SetItemChecked(i, false);
            }
        }

        private void checkboxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkboxList.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {
                    bool allChecked = (checkboxList.Items.Count == checkboxList.CheckedItems.Count);

                    if (allChecked)
                    {
                        if (!btnAll.Checked)
                        {
                            btnAll.CheckedChanged -= btnAll_CheckedChanged;
                            btnAll.Checked = true;
                            btnAll.CheckedChanged += btnAll_CheckedChanged;
                        }
                    }
                    else
                    {
                        if (btnAll.Checked)
                        {
                            btnAll.CheckedChanged -= btnAll_CheckedChanged;
                            btnAll.Checked = false;
                            btnAll.CheckedChanged += btnAll_CheckedChanged;
                        }
                    }
                }));
            }
        }




        private void TaskAdd_Load(object sender, EventArgs e)
        {

        }
        

        private void btnBack_Click(object sender, EventArgs e)
        {
            TaskView taskView = new TaskView(teacher_id, class_id, desktopPanel);
            taskView.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(taskView);
            taskView.Dock = DockStyle.Fill;
            taskView.FormBorderStyle = FormBorderStyle.None;
            taskView.Show();
            this.Close();
        }



        private void btnCreate_Click(object sender, EventArgs e)
        {
            Database_Connection db = new Database_Connection();
            List<int> students = new List<int>();
            if (!btnAll.Checked)
            {
                for (int i = 0; i < checkboxList.Items.Count; i++)
                {
                    if (checkboxList.GetItemChecked(i))
                    {
                        string student = checkboxList.Items[i].ToString();
                        string query = $"SELECT student_id FROM student WHERE CONCAT(studLastname, ', ', studFirstname, ' ', studMiddlename) = '{student}'";
                        int student_id = Convert.ToInt32(db.ExecuteScalar(query));
                        students.Add(student_id);
                    }
                }
            }

            Fill();
            if (!string.IsNullOrEmpty(taskName) && !string.IsNullOrEmpty(taskDuedate) && !string.IsNullOrEmpty(taskDuetime) && !string.IsNullOrEmpty(taskType) && taskPoints > 0)
            {
                
                string query = "SELECT COUNT(*) FROM task " +
                               "WHERE taskType = @taskType " +
                               "AND taskName = @taskName " +
                               "AND taskDescription = @taskDescription " +
                               "AND taskDuedate = @taskDuedate " +
                               "AND taskDuetime = @taskDuetime " +
                               "AND taskPoints = @taskPoints " +
                               "AND taskArchive = @taskArchive";

                MySqlCommand command = new MySqlCommand(query, db.connection);
                command.Parameters.AddWithValue("@taskType", taskType);
                command.Parameters.AddWithValue("@taskName", taskName);
                command.Parameters.AddWithValue("@taskDescription", taskDescription);
                command.Parameters.AddWithValue("@taskDuedate", taskDuedate);
                command.Parameters.AddWithValue("@taskDuetime", taskDuetime);
                command.Parameters.AddWithValue("@taskPoints", taskPoints);
                command.Parameters.AddWithValue("@taskArchive", "NO");
                command.Parameters.AddWithValue("@teacher_id", teacher_id);

                db.OpenConnection();
                int count = Convert.ToInt32(command.ExecuteScalar());
                db.CloseConnection();

                if (count > 0)
                {
                    DialogResult result = MessageBox.Show("You already posted task, do you want to continue?", "Duplicate Task", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                string insertQuery, classTask = "YES";

                if (btnAll.Checked)
                {
                    if (!string.IsNullOrEmpty(taskLink) && !string.IsNullOrEmpty(taskFilename))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskLink, taskFilename, taskDocument, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskLink, @taskDocument, @taskFilename, @taskClass, @taskArchive, @class_id)";
                    }
                    else if (!string.IsNullOrEmpty(taskLink))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskLink, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskLink, @taskClass, @taskArchive, @class_id)";
                    }
                    else if (!string.IsNullOrEmpty(taskFilename))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskFilename, taskDocument, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskFilename, @taskDocument, @taskClass, @taskArchive, @class_id)";
                    }
                    else
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints,  @taskClass, @taskArchive, @class_id)";
                    }

                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, db.connection);
                    insertCommand.Parameters.AddWithValue("@taskType", taskType);
                    insertCommand.Parameters.AddWithValue("@taskName", taskName);
                    insertCommand.Parameters.AddWithValue("@taskDescription", taskDescription);
                    insertCommand.Parameters.AddWithValue("@taskDuedate", taskDuedate);
                    insertCommand.Parameters.AddWithValue("@taskDuetime", taskDuetime);
                    insertCommand.Parameters.AddWithValue("@taskPoints", taskPoints);
                    insertCommand.Parameters.AddWithValue("@taskLink", taskLink);
                    insertCommand.Parameters.AddWithValue("@taskFilename", taskFilename);
                    insertCommand.Parameters.AddWithValue("@taskDocument", taskDocument);
                    insertCommand.Parameters.AddWithValue("@taskClass", classTask);
                    insertCommand.Parameters.AddWithValue("@taskArchive", "NO");
                    insertCommand.Parameters.AddWithValue("@class_id", class_id);
                    db.OpenConnection();
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    classTask = "NO";
                    if (!string.IsNullOrEmpty(taskLink) && !string.IsNullOrEmpty(taskFilename))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskLink, taskFilename, taskDocument, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskLink, @taskDocument, @taskFilename, @taskClass, @taskArchive, @class_id)";
                    }
                    else if (!string.IsNullOrEmpty(taskLink))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskLink, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskLink, @taskClass, @taskArchive, @class_id)";
                    }
                    else if (!string.IsNullOrEmpty(taskFilename))
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskFilename, taskDocument, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskFilename, @taskDocument, @taskClass, @taskArchive, @class_id)";
                    }
                    else
                    {
                        insertQuery = "INSERT INTO task (taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskPoints, taskClass, taskArchive, class_id) " +
                            "VALUES (@taskType, @taskName, @taskDescription, @taskDuedate, @taskDuetime, @taskPoints, @taskClass, @taskArchive, @class_id)";
                    }

                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, db.connection);
                    insertCommand.Parameters.AddWithValue("@taskType", taskType);
                    insertCommand.Parameters.AddWithValue("@taskName", taskName);
                    insertCommand.Parameters.AddWithValue("@taskDescription", taskDescription);
                    insertCommand.Parameters.AddWithValue("@taskDuedate", taskDuedate);
                    insertCommand.Parameters.AddWithValue("@taskDuetime", taskDuetime);
                    insertCommand.Parameters.AddWithValue("@taskPoints", taskPoints);
                    insertCommand.Parameters.AddWithValue("@taskLink", taskLink);
                    insertCommand.Parameters.AddWithValue("@taskFilename", taskFilename);
                    insertCommand.Parameters.AddWithValue("@taskDocument", taskDocument);
                    insertCommand.Parameters.AddWithValue("@taskClass", classTask);
                    insertCommand.Parameters.AddWithValue("@taskArchive", "NO");
                    insertCommand.Parameters.AddWithValue("@class_id", class_id);
                    db.OpenConnection();
                    insertCommand.ExecuteNonQuery();

                    string getTaskID = "SELECT LAST_INSERT_ID()";
                    int task_id = Convert.ToInt32(db.ExecuteScalar(getTaskID));

                    for (int i = 0; i < students.Count; i++)
                    {
                        string querystudent = $"INSERT INTO task_student (task_id, student_id) " +
                                                     $"VALUES ('{task_id}', '{students[i]}');";
                        db.ExecuteQuery(querystudent);
                    }
                }                                      
                db.CloseConnection();
                string subject = $"{teacherName} added a new task.";
                string body = $"An {taskType} called {taskName} is created in class {className} section {section}. Due date: {taskDuedate} at {taskDueTimeStr}.";
                NotifyViaEmail(subject, body);
                MessageBox.Show("Task added to the successfully", "Task Added", MessageBoxButtons.OK);
                TaskView taskView = new TaskView(teacher_id, class_id, desktopPanel);
                taskView.TopLevel = false;
                desktopPanel.Controls.Clear();
                desktopPanel.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.FormBorderStyle = FormBorderStyle.None;
                taskView.Show();
                this.Close();
            } else
                MessageBox.Show("Error creating task", "Error", MessageBoxButtons.OK);
        }

        void NotifyViaEmail(string subject, string body)
        {
            Database_Connection dbConnection = new Database_Connection();
                string query = $"SELECT u.email FROM student_class sc JOIN student s ON sc.student_id = s.student_id JOIN user u ON s.user_id = u.user_id WHERE sc.class_id = {class_id}";
                DataTable resultTable = dbConnection.ExecuteSelectQuery(query);

                if (resultTable.Rows.Count > 0)
                {
                    foreach (DataRow row in resultTable.Rows)
                    {
                        string email = row["email"].ToString();

                        Email emailSender = new Email(email, subject, body);
                    }

                    Console.WriteLine("Emails sent successfully.");
                }
                else
                {
                    Console.WriteLine("No students found for the selected class.");
                }            
        }

        private void getTeacherName()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = $"SELECT CONCAT(teacherLastname, ', ', teacherFirstname, ' ', teacherMiddlename) as studName FROM teacher WHERE teacher_id = {teacher_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                teacherName = studentTable.Rows[0]["studName"].ToString();
            }
        }
        private void getClassName()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = $"SELECT className, classSection FROM class WHERE class_id = {class_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                className = studentTable.Rows[0]["className"].ToString();
                section = studentTable.Rows[0]["classSection"].ToString();
            }
        }

        private void lblAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblFilename.Text))
            {
                // File name exists, remove it
                lblFilename.Text = string.Empty;
                lblAttach.Text = "Attach File";
            }
            else
            {
                openFile.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx";
                openFile.ShowDialog();
                lblFilename.Text = openFile.FileName;
                lblAttach.Text = "Remove";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Remove")
            {
                lblLink.Text = string.Empty;
                linkLabel1.Text = "Add Link";
            }
            else
            {
                AddLink add = new AddLink();
                add.LinkAdded += Add_LinkAdded;
                add.Show();
            }
        }

        private void Add_LinkAdded(string link1)
        {
            lblLink.Text = link1;
            linkLabel1.Text = !string.IsNullOrEmpty(lblLink.Text) ? "Remove" : "Add Link";

        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://" + lblLink.Text);
        }

        private void lblFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lblFilename.Text);
        }
    }
}
