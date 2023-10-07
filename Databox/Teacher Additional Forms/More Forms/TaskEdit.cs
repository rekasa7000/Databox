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

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class TaskEdit : Form
    {
        Panel desktopPanel;
        int teacher_id, task_id, class_id;
        string taskType, taskName, taskDescription, taskDuedate, taskDuetime, taskLink, taskFilename, taskClass;
        byte[] taskDocument;
        TaskDocument doc;
        int taskPoints;
        public TaskEdit(int teacherID, int classID, int taskID, Panel desktopPan)
        {
            desktopPanel = desktopPan;
            class_id = classID;
            task_id = taskID;
            teacher_id = teacherID;
            InitializeComponent();
            tpDue.Format = DateTimePickerFormat.Custom;
            tpDue.CustomFormat = "hh:mm tt";
            tpDue.Value = DateTime.Today.AddHours(0);
            checkList();
        }
        void checkList()
        {
            Database_Connection db = new Database_Connection();
            string query = $"SELECT student_id FROM task_student WHERE task_id = {task_id}";
            DataTable studentIdsTable = db.ExecuteSelectQuery(query);

            List<int> selectedStudents = new List<int>();
            foreach (DataRow row in studentIdsTable.Rows)
            {
                int studentId = Convert.ToInt32(row["student_id"]);
                selectedStudents.Add(studentId);
            }

            checkboxList.Items.Clear();

            string fillQuery = "SELECT s.student_id, CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) AS fullName " +
                               "FROM student s " +
                               "JOIN student_class sc ON s.student_id = sc.student_id " +
                               "JOIN class c ON sc.class_id = c.class_id " +
                               $"WHERE c.class_id = {class_id} " +
                               "ORDER BY fullName";
            DataTable dataTable = db.ExecuteSelectQuery(fillQuery);

            if (taskClass == "NO")
            {
                btnAll.Checked = false;
                foreach (DataRow row in dataTable.Rows)
                {
                    int studentId = Convert.ToInt32(row["student_id"]);
                    string fullName = row["fullName"].ToString();
                    checkboxList.Items.Add(fullName);

                    if (selectedStudents.Contains(studentId))
                    {
                        int index = checkboxList.Items.Count - 1;
                        checkboxList.SetItemChecked(index, true);
                    }
                }
            }
            else
            {
                btnAll.Checked = true;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    checkboxList.Items.Add(dataTable.Rows[i]["fullName"].ToString());
                    checkboxList.SetItemChecked(i, true);
                }
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
        public void LoadData(string taskType, string taskName, string taskDescription, DateTime taskDueDate,
            string taskDueTime, string taskPoints, string taskLink, TaskDocument taskDocument, string tclass)
        {
            cmbTasktype.SelectedItem = taskType;
            txtTitle.Text = taskName;
            richTextBox.Rtf = taskDescription;
            dtpDue.Value = taskDueDate;
            DateTime dateTime = DateTime.ParseExact(taskDueTime, "HH:mm:ss", CultureInfo.InvariantCulture);
            string taskDuetime = dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            tpDue.Value = DateTime.ParseExact(taskDuetime, "hh:mm tt", CultureInfo.InvariantCulture);
            txtPoints.Text = taskPoints;
            lblLink.Text = taskLink;
            doc = taskDocument;
            if (taskDocument != null)
            {
                lblFilename.Text = taskDocument.FileName;
            }
            taskClass = tclass; 
            checkList();
        }


        private void TaskEdit_Load(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(lblFilename.Text))
            {
                lblAttach.Text = "Remove";
            }
            else
            {
                lblAttach.Text = "Add link";
            }

            if (!string.IsNullOrEmpty(lblLink.Text))
            {
                linkLabel1.Text = "Remove";
            }
            else
            {
                linkLabel1.Text = "Add link";
            }
        }




        private void lblAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblFilename.Text))
            {
                // File name exists, remove it
                string filePath = lblFilename.Text;
                lblFilename.Text = string.Empty;
                lblAttach.Text = "Attach File";

                // Delete the temporary file
                File.Delete(filePath);
            }
            else
            {
                openFile.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx";
                openFile.ShowDialog();
                lblFilename.Text = openFile.FileName;
                lblAttach.Text = "Remove";
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
            string taskDueTimeStr = tpDue.Value.ToString("hh:mm tt");
            DateTime dateTime = DateTime.ParseExact(taskDueTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);
            taskDuetime = dateTime.ToString("HH:mm", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(lblLink.Text))
            {
                taskLink = lblLink.Text;
            } else
            {
                taskLink = null;
            }
            if (!string.IsNullOrEmpty(lblFilename.Text))
            {
                string filePath = lblFilename.Text;
                string fileName = Path.GetFileName(filePath);
                taskDocument = File.ReadAllBytes(filePath);
                taskFilename = fileName;
            } else
            {
                taskDocument = null;
                taskFilename = null;
            }
        }

        private void lblFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(doc.FilePath))
            {
                try
                {
                    Process.Start(doc.FilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            Fill();
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
            if (!string.IsNullOrEmpty(taskName) && !string.IsNullOrEmpty(taskDuedate) && !string.IsNullOrEmpty(taskDuetime) && !string.IsNullOrEmpty(taskType) && taskPoints > 0)
            {
                int rowsAffected;
                if (btnAll.Checked)
                {
                    string updateQuery = "UPDATE task " +
                                     "SET taskType = @taskType, " +
                                     "taskName = @taskName, " +
                                     "taskDescription = @taskDescription, " +
                                     "taskDuedate = @taskDuedate, " +
                                     "taskDuetime = @taskDuetime, " +
                                     "taskPoints = @taskPoints, " +
                                     "taskLink = @taskLink, " +
                                     "taskFilename = @taskFilename, " +                                     
                                     "taskDocument = @taskDocument, " +
                                     "taskClass = @taskClass " +
                                     "WHERE taskArchive = @taskArchive " +                                     
                                     "AND task_id = @task_id";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, db.connection);
                    updateCommand.Parameters.AddWithValue("@taskType", taskType);
                    updateCommand.Parameters.AddWithValue("@taskName", taskName);
                    updateCommand.Parameters.AddWithValue("@taskDescription", taskDescription);
                    updateCommand.Parameters.AddWithValue("@taskDuedate", taskDuedate);
                    updateCommand.Parameters.AddWithValue("@taskDuetime", taskDuetime);
                    updateCommand.Parameters.AddWithValue("@taskPoints", taskPoints);
                    updateCommand.Parameters.AddWithValue("@taskLink", taskLink);
                    updateCommand.Parameters.AddWithValue("@taskFilename", taskFilename);
                    updateCommand.Parameters.AddWithValue("@taskDocument", taskDocument);                    
                    updateCommand.Parameters.AddWithValue("@taskArchive", "NO");
                    updateCommand.Parameters.AddWithValue("@taskClass", "Yes");
                    updateCommand.Parameters.AddWithValue("@task_id", task_id);

                    db.OpenConnection();
                    rowsAffected = updateCommand.ExecuteNonQuery();
                    string deleteQuery = $"DELETE FROM task_student WHERE task_id = '{task_id}'";
                    db.ExecuteQuery(deleteQuery);
                } else
                {
                    string updateQuery = "UPDATE task " +
                                     "SET taskType = @taskType, " +
                                     "taskName = @taskName, " +
                                     "taskDescription = @taskDescription, " +
                                     "taskDuedate = @taskDuedate, " +
                                     "taskDuetime = @taskDuetime, " +
                                     "taskPoints = @taskPoints, " +
                                     "taskLink = @taskLink, " +
                                     "taskFilename = @taskFilename, " +
                                     "taskDocument = @taskDocument, " +
                                     "taskClass = @taskClass " +
                                     "WHERE taskArchive = @taskArchive " +                                     
                                     "AND task_id = @task_id";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, db.connection);
                    updateCommand.Parameters.AddWithValue("@taskType", taskType);
                    updateCommand.Parameters.AddWithValue("@taskName", taskName);
                    updateCommand.Parameters.AddWithValue("@taskDescription", taskDescription);
                    updateCommand.Parameters.AddWithValue("@taskDuedate", taskDuedate);
                    updateCommand.Parameters.AddWithValue("@taskDuetime", taskDuetime);
                    updateCommand.Parameters.AddWithValue("@taskPoints", taskPoints);
                    updateCommand.Parameters.AddWithValue("@taskLink", taskLink);
                    updateCommand.Parameters.AddWithValue("@taskFilename", taskFilename);
                    updateCommand.Parameters.AddWithValue("@taskDocument", taskDocument);
                    updateCommand.Parameters.AddWithValue("@taskArchive", "NO");
                    updateCommand.Parameters.AddWithValue("@taskClass", "NO");
                    updateCommand.Parameters.AddWithValue("@task_id", task_id);

                    db.OpenConnection();
                    rowsAffected = updateCommand.ExecuteNonQuery();

                    string deleteQuery = $"DELETE FROM task_student WHERE task_id = '{task_id}'";
                    db.ExecuteQuery(deleteQuery);

                    for (int i = 0; i < students.Count; i++)
                    {
                        string querystudent = $"INSERT INTO task_student (task_id, student_id) " +
                                                     $"VALUES ('{task_id}', '{students[i]}');";
                        db.ExecuteQuery(querystudent);
                    }
                }
                db.CloseConnection();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Task updated successfully", "Task Updated", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("No task found to update", "Task Not Found", MessageBoxButtons.OK);
                }

                TaskView taskView = new TaskView(teacher_id, class_id, desktopPanel);
                taskView.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.FormBorderStyle = FormBorderStyle.None;
                taskView.Show();
                this.Close();
            }
        }

    }
}
