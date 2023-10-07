using FontAwesome.Sharp;
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
    public partial class SubmittedTasks : Form
    {
        int class_id, task_id, teacher_id;
        Panel desktopPanel;
        public SubmittedTasks(int classID, int taskID, int teacherID, Panel pan)
        {
            class_id = classID;
            task_id = taskID;
            teacher_id = teacherID;
            desktopPanel = pan;
            InitializeComponent();
            assignedTasks("fullName", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
            notYetDoneTasks("fullName", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
            populateTasktitle(); populateLabel(); PopulateSubmissionScoresFromDatabase();
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupAssigned.Controls.Clear();
            groupLate.Controls.Clear();
            groupWell.Controls.Clear();
            flow.Controls.Clear();
            string selectedSort = cmbSort.SelectedItem.ToString();
            if (selectedSort == "Sort by date submitted")
            {
                assignedTasks("submit.submitDatetime ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
                notYetDoneTasks("submit.submitDatetime ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
            }
            else if (selectedSort == "Sort by First name")
            {
                assignedTasks("s.studFirstname ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
                notYetDoneTasks("s.studFirstname ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
            }
            else if (selectedSort == "Sort by Last name")
            {
                assignedTasks("s.studLastname ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
                notYetDoneTasks("s.studLastname ASC", "CONCAT(s.studFirstname, ' ', s.studLastname) AS fullName");
            }
        }
        void populateTasktitle()
        {
            string query = $"SELECT taskName FROM task WHERE task_id = {task_id}";
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            DataTable dataTable = db.ExecuteSelectQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                string taskName = row["taskName"].ToString();
                lblTasktitle.Text = taskName;
            }
        }

        void populateLabel()
        {
            string query = $@"
        SELECT
            (SELECT COUNT(*) FROM submission WHERE task_id = {task_id}) AS submission_count,
            (SELECT COUNT(*) FROM student_class WHERE student_class.class_id = {class_id} AND student_id NOT IN (SELECT student_id FROM submission WHERE task_id = {task_id})) AS not_submitted_count
        FROM
            dual";

            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            DataTable dataTable = db.ExecuteSelectQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                int submissionCount = Convert.ToInt32(row["submission_count"]);
                int notSubmittedCount = Convert.ToInt32(row["not_submitted_count"]);

                lblTurnedincount.Text = submissionCount.ToString();
                panelTurnedin.Visible = true;

                lblAssignedcount.Text = notSubmittedCount.ToString();
                panelAssigned.Visible = true;
            }
        }



        void assignedTasks(string orderBy, string name)
        {
            string query = $"SELECT {name}, " +
                           "t.taskPoints, submit.submitStatus, submit.submitScore " +
                           "FROM student s " +
                           "JOIN student_class sc ON s.student_id = sc.student_id " +
                           "JOIN class c ON sc.class_id = c.class_id " +
                           "JOIN task t ON t.class_id = sc.class_id " +
                           "LEFT JOIN submission submit ON t.task_id = submit.task_id AND s.student_id = submit.student_id " +
                           $"WHERE sc.class_id = {class_id} " +
                           $"AND t.task_id = {task_id} " +
                           "AND submit.submitStatus IS NOT NULL " +
                           $"ORDER BY {orderBy}";

            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            DataTable dataTable = db.ExecuteSelectQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                string fullName = row["fullName"].ToString();
                string taskPoints = row["taskPoints"].ToString();
                string status = row["submitStatus"].ToString();
                string score = row["submitScore"].ToString();
                lblScore.Text = $"{taskPoints} Points";
                if (status == "Late")
                {
                    generateControlsLate(fullName, taskPoints, status);
                    groupLate.Visible = true;
                }
                else
                {
                    generateControlsWell(fullName, taskPoints, status);
                    groupWell.Visible = true;
                }
                generateFlowControls(fullName, status);
            }
        }


        void notYetDoneTasks(string orderBy, string sort)
        {
            string query = $@"SELECT {sort},
                                       t.taskPoints,
                                       CASE
                                           WHEN CONCAT(t.taskDuedate, ' ', t.taskDuetime) <= NOW() THEN 'Missing'                                           
                                       END AS submitStatus
                                FROM student s
                                JOIN student_class sc ON s.student_id = sc.student_id
                                JOIN class c ON sc.class_id = c.class_id
                                JOIN task t ON t.class_id = sc.class_id
                                LEFT JOIN submission submit ON t.task_id = submit.task_id AND s.student_id = submit.student_id
                                WHERE sc.class_id = {class_id}
                                AND t.task_id = {task_id}
                                AND submit.submitStatus IS NULL
                                ORDER BY {orderBy};
                                ";


            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            DataTable dataTable = db.ExecuteSelectQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                string fullName = row["fullName"].ToString();
                string taskPoints = row["taskPoints"].ToString();
                string status = row["submitStatus"].ToString();
                lblScore.Text = $"{taskPoints} Points";
                if (status == "Missing")
                {
                    generateControlsAssigned(fullName, taskPoints, status);
                    groupAssigned.Visible = true;
                }
                else
                {
                    generateControlsAssigned(fullName, taskPoints, status);
                    groupAssigned.Visible = true;
                }
                generateFlowControls(fullName, status);
            }
        }
        private void generateControlsAssigned(string fullName, string taskPoints, string status)
        {
            groupAssigned.Text = status;
            groupAssigned.ForeColor = Color.White;
            groupAssigned.Size = new Size(350, 400);
            groupAssigned.AutoSize = true;
            groupAssigned.Dock = DockStyle.Top;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Bottom;
            panel.Size = new Size(100, 40);

            CheckBox checkBox = new CheckBox();
            checkBox.Text = fullName;
            checkBox.Location = new Point(10, 0);
            checkBox.Font = new Font("Nirmala UI", 10, FontStyle.Regular);
            checkBox.ForeColor = Color.White;
            checkBox.Size = new Size(200, 30);

            Panel pan = new Panel();
            TextBox textBox = new TextBox();
            pan.Location = new Point(checkBox.Right, checkBox.Top);
            textBox.Font = new Font("Nirmala UI", 11, FontStyle.Regular);
            textBox.BackColor = Color.FromArgb(43, 45, 49);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.None;
            pan.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.Size = new Size(25, 27);
            textBox.TextChanged += (sender, e) =>
            {
                checkBox.Checked = !string.IsNullOrEmpty(textBox.Text);
            };
            checkBox.Click += (sender, e) =>
            {
                ReturnTask taskView = new ReturnTask(checkBox.Text, class_id, task_id);
                taskView.TopLevel = false;
                panelSubmissions.Controls.Clear();
                panelSubmissions.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.Show();
            };
            pan.BackColor = Color.FromArgb(85, 85, 90);
            pan.Size = new Size(25, 25);

            Label label = new Label();
            label.Location = new Point(pan.Right, checkBox.Top);
            label.Font = new Font("Nirmala UI", 12, FontStyle.Regular);
            label.ForeColor = Color.White;
            label.Text = $"/{taskPoints} points";

            panel.Controls.Add(checkBox);
            panel.Controls.Add(pan);
            panel.Controls.Add(label);
            groupAssigned.Controls.Add(panel);
        }
        private void generateControlsLate(string fullName, string taskPoints, string status)
        {
            groupLate.Text = $"Done {status}";
            groupLate.ForeColor = Color.White;
            groupLate.Size = new Size(350, 400);
            groupLate.AutoSize = true;
            groupLate.Dock = DockStyle.Top;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Bottom;
            panel.Size = new Size(100, 40);

            CheckBox checkBox = new CheckBox();
            checkBox.Text = fullName;
            checkBox.Location = new Point(10, 0);
            checkBox.Font = new Font("Nirmala UI", 10, FontStyle.Regular);
            checkBox.ForeColor = Color.White;
            checkBox.Size = new Size(200, 30);

            Panel pan = new Panel();
            TextBox textBox = new TextBox();
            pan.Location = new Point(checkBox.Right, checkBox.Top);
            textBox.Font = new Font("Nirmala UI", 11, FontStyle.Regular);
            textBox.BackColor = Color.FromArgb(43, 45, 49);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.None;
            pan.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.Size = new Size(25, 27);
            textBox.TextChanged += (sender, e) =>
            {
                checkBox.Checked = !string.IsNullOrEmpty(textBox.Text);
            };
            checkBox.Click += (sender, e) =>
            {
                ReturnTask taskView = new ReturnTask(checkBox.Text, class_id, task_id);
                taskView.TopLevel = false;
                panelSubmissions.Controls.Clear();
                panelSubmissions.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.Show();
            };
            pan.BackColor = Color.FromArgb(85, 85, 90);
            pan.Size = new Size(25, 25);

            Label label = new Label();
            label.Location = new Point(pan.Right, checkBox.Top);
            label.Font = new Font("Nirmala UI", 12, FontStyle.Regular);
            label.ForeColor = Color.White;
            label.Text = $"/{taskPoints} points";

            panel.Controls.Add(checkBox);
            panel.Controls.Add(pan);
            panel.Controls.Add(label);
            groupLate.Controls.Add(panel);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            TaskView taskView = new TaskView(teacher_id, class_id, desktopPanel);
            taskView.TopLevel = false;
            desktopPanel.Controls.Clear();
            desktopPanel.Controls.Add(taskView);
            taskView.Dock = DockStyle.Fill;
            taskView.FormBorderStyle = FormBorderStyle.None;
            taskView.Show();
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            UpdateSubmissionScores();
            MessageBox.Show("Scores return Successfully!");
        }
        private void PopulateSubmissionScoresFromDatabase()
        {
            foreach (Panel panel in groupWell.Controls)
            {
                CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                Panel innerPanel = panel.Controls.OfType<Panel>().FirstOrDefault();
                TextBox textBox = innerPanel.Controls.OfType<TextBox>().FirstOrDefault();
                if (checkBox != null && textBox != null)
                {
                    string fullName = checkBox.Text;
                    int studentId = GetStudentId(fullName);
                    int score = GetSubmissionScore(task_id, studentId);

                    if (score != -1)
                    {
                        textBox.Text = score.ToString();
                    }
                }
            }

            foreach (Panel panel in groupLate.Controls)
            {
                CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                Panel innerPanel = panel.Controls.OfType<Panel>().FirstOrDefault();
                TextBox textBox = innerPanel.Controls.OfType<TextBox>().FirstOrDefault();
                if (checkBox != null && textBox != null)
                {
                    string fullName = checkBox.Text;
                    int studentId = GetStudentId(fullName);
                    int score = GetSubmissionScore(task_id, studentId);
                    if (score != -1) {
                        textBox.Text = score.ToString(); 
                    }
                }
            }
        }

        private int GetSubmissionScore(int taskId, int studentId)
        {
            string query = $"SELECT submitScore FROM submission WHERE task_id = {taskId} AND student_id = {studentId}";
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            object result = db.ExecuteScalar(query);
            db.CloseConnection();

            if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int score))
            {
                return score;
            }

            return -1;
        }

        private void UpdateSubmissionScores()
        {
            foreach (Panel panel in groupWell.Controls)
            {
                CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                Panel innerPanel = panel.Controls.OfType<Panel>().FirstOrDefault();
                TextBox textBox = innerPanel.Controls.OfType<TextBox>().FirstOrDefault();
                if (checkBox != null && textBox != null && checkBox.Checked)
                {
                    string fullName = checkBox.Text;
                    string scoreText = textBox.Text;

                    if (int.TryParse(scoreText, out int score))
                    {
                        // Update submission table with the score
                        int studentId = GetStudentId(fullName);
                        UpdateSubmissionScore(task_id, studentId, score);
                    }
                    else
                    {
                        MessageBox.Show("Invalid score format.");
                    }
                }
            }
            foreach (Panel panel in groupLate.Controls)
            {
                CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
                Panel innerPanel = panel.Controls.OfType<Panel>().FirstOrDefault();
                TextBox textBox = innerPanel.Controls.OfType<TextBox>().FirstOrDefault();
                if (checkBox != null && textBox != null && checkBox.Checked)
                {
                    string fullName = checkBox.Text;
                    string scoreText = textBox.Text;

                    if (int.TryParse(scoreText, out int score))
                    {
                        // Update submission table with the score
                        int studentId = GetStudentId(fullName);
                        UpdateSubmissionScore(task_id, studentId, score);
                    }
                    else
                    {
                        MessageBox.Show("Invalid score format.");
                    }
                }
            }
        }


        private CheckBox FindCheckBox(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is CheckBox checkBox && checkBox.Name == "checkBox")
                {
                    return checkBox;
                }

                CheckBox foundCheckBox = FindCheckBox(childControl);
                if (foundCheckBox != null)
                {
                    return foundCheckBox;
                }
            }

            return null;
        }

        private int GetStudentId(string fullName)
        {
            string query = $"SELECT student_id FROM student WHERE CONCAT(studFirstname, ' ', studLastname) = '{fullName}'";
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            int studentId = Convert.ToInt32(db.ExecuteScalar(query));
            db.CloseConnection();
            return studentId;
        }

        private void UpdateSubmissionScore(int taskId, int studentId, int score)
        {
            string query = $"UPDATE submission SET submitScore = {score} WHERE task_id = {taskId} AND student_id = {studentId}";
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            db.ExecuteNonQuery(query);
            db.CloseConnection();
        }

        private void generateControlsWell(string fullName, string taskPoints, string status)
        {
            groupWell.Text = $"{status} Done";
            groupWell.ForeColor = Color.White;
            groupWell.Size = new Size(350, 400);
            groupWell.AutoSize = true;
            groupWell.Dock = DockStyle.Top;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Bottom;
            panel.Size = new Size(100, 40);

            CheckBox checkBox = new CheckBox();
            checkBox.Text = fullName;
            checkBox.Location = new Point(10, 0);
            checkBox.Font = new Font("Nirmala UI", 10, FontStyle.Regular);
            checkBox.ForeColor = Color.White;
            checkBox.Size = new Size(200, 30);

            Panel pan = new Panel();
            TextBox textBox = new TextBox();
            pan.Location = new Point(checkBox.Right, checkBox.Top);
            textBox.Font = new Font("Nirmala UI", 11, FontStyle.Regular);
            textBox.BackColor = Color.FromArgb(43, 45, 49);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.None;
            pan.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.Size = new Size(25, 27);
            textBox.TextChanged += (sender, e) =>
            {
                checkBox.Checked = !string.IsNullOrEmpty(textBox.Text);
            };
            checkBox.Click += (sender, e) =>
            {
                ReturnTask taskView = new ReturnTask(checkBox.Text, class_id, task_id);
                taskView.TopLevel = false;
                panelSubmissions.Controls.Clear();
                panelSubmissions.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.Show();
            };
            pan.BackColor = Color.FromArgb(85, 85, 90);
            pan.Size = new Size(25, 25);

            Label label = new Label();
            label.Location = new Point(pan.Right, checkBox.Top);
            label.Font = new Font("Nirmala UI", 12, FontStyle.Regular);
            label.ForeColor = Color.White;
            label.Text = $"/{taskPoints} points";

            panel.Controls.Add(checkBox);
            panel.Controls.Add(pan);
            panel.Controls.Add(label);
            groupWell.Controls.Add(panel);
        }

        private void generateFlowControls(string fullName, string status)
        {
            string stat;

            Panel panel = new Panel();
            panel.Padding = new Padding(5, 5, 5, 5);            
            flow.Controls.Add(panel);
            flow.FlowDirection = FlowDirection.LeftToRight;
            panel.Size = new Size(200, 100);


            Panel pan = new Panel();
            pan.BackColor = Color.FromArgb(43, 45, 49);
            panel.Controls.Add(pan);
            pan.Dock = DockStyle.Fill;

            IconButton icon = new IconButton();
            icon.IconChar = IconChar.User;
            icon.Text = fullName;
            icon.Location = new Point(10, 10);
            icon.Font = new Font("Nirmala UI", 12, FontStyle.Regular);
            icon.ForeColor = Color.White;
            icon.Dock = DockStyle.Top;
            icon.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon.IconSize = 25;
            icon.IconColor = Color.LightBlue;
            icon.FlatStyle = FlatStyle.Flat;
            icon.Size = new Size(200, 50);
            icon.FlatAppearance.BorderSize = 0;
            icon.Click += (sender, e) =>
            {
                ReturnTask taskView = new ReturnTask(icon.Text, class_id, task_id);
                taskView.TopLevel = false;
                panelSubmissions.Controls.Clear();
                panelSubmissions.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.Show();
            };

            Label lblStatus = new Label();
            lblStatus.Location = new Point(10, icon.Bottom);
            lblStatus.Font = new Font("Nirmala UI", 10, FontStyle.Regular);
            lblStatus.ForeColor = Color.White;
            if (status == "Late")
            {
                stat = "Turned In Late";
                lblStatus.ForeColor = Color.Red;
            }
            else if (status == "On-time")
            {
                stat = "Turned In";
                lblStatus.ForeColor = Color.Teal;
            }
            else if (status == "Missing")
            {
                stat = "Missing";
                lblStatus.ForeColor = Color.Red;                
            }
            else
            {
                stat = "Assigned";
                lblStatus.ForeColor = Color.Blue;
            }
            lblStatus.Text = stat;

            pan.Controls.Add(icon);
            pan.Controls.Add(lblStatus);
        }
    }    
}
