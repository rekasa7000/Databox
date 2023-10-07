using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ReturnTask : Form
    {
        string studentName, taskName; int class_id, task_id;
        public ReturnTask(string student, int classid, int taskid)
        {
            studentName = student;
            class_id = classid;
            task_id = taskid;
            InitializeComponent();
            DisplayQueryResults();
            RetrieveSubmittedFiles();
            lblName.Text = student;
        }

        private void ReturnTask_Load(object sender, EventArgs e)
        {

        }

        private int GetStudentId(string studentName)
        {
            string query = $"SELECT student_id FROM student WHERE CONCAT(studFirstname, ' ', studLastname) = '{studentName}'";

            Database_Connection db = new Database_Connection();
            db.OpenConnection();

            object result = db.ExecuteScalar(query);
            int studentId = result != null ? Convert.ToInt32(result) : -1;

            db.CloseConnection();

            return studentId;
        }

        private void DisplayQueryResults()
        {
            string query = "SELECT t.taskName, t.taskPoints, submit.submitStatus, submit.submitScore, CONCAT(t.taskDuedate, ' ', t.taskDuetime) AS taskDuedate, submit.submitDatetime " +
                           "FROM student s " +
                           "JOIN student_class sc ON s.student_id = sc.student_id " +
                           "JOIN class c ON sc.class_id = c.class_id " +
                           "JOIN task t ON t.class_id = sc.class_id " +
                           "LEFT JOIN submission submit ON t.task_id = submit.task_id AND s.student_id = submit.student_id " +
                           $"WHERE t.task_id = {task_id} " +
                           $"AND s.student_id = {GetStudentId(studentName)}";

            Database_Connection db = new Database_Connection();
            DataTable resultTable = db.ExecuteSelectQuery(query);

            int labelIndex = 0;
            int labelTop = 10;
            foreach (DataRow row in resultTable.Rows)
            {
                taskName = row["taskPoints"].ToString();
                string taskPoints = row["taskPoints"].ToString();
                string submitStatus = row["submitStatus"].ToString();
                string submitScore = row["submitScore"].ToString();
                DateTime taskDueDate = DateTime.Parse(row["taskDuedate"].ToString());
                DateTime? submitDate = null;
                if (row["submitDatetime"] != DBNull.Value)
                {
                    if (DateTime.TryParse(row["submitDatetime"].ToString(), out DateTime parsedDateTime))
                    {
                        submitDate = parsedDateTime;
                    }
                }
                if (string.IsNullOrEmpty(submitStatus) && submitDate == null)
                {
                    if (taskDueDate < DateTime.Now)
                    {
                        lblStatus.Text = "Missing";
                        lblStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblStatus.Text = "Assigned";
                        lblStatus.ForeColor = Color.LightBlue;
                    }
                }
                else if (string.IsNullOrEmpty(submitScore))
                {
                    lblStatus.Text = "Not yet graded";
                    if (taskDueDate < submitDate)
                    {
                        lblSubmitstatus.Text = "Turned In late";
                        lblSubmitstatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblSubmitstatus.Text = "Turned In";
                        lblSubmitstatus.ForeColor = Color.LightBlue;
                    }
                }
                else
                {
                    lblStatus.Text = $"{submitScore}/{taskPoints} \n Graded";
                    if (taskDueDate < submitDate)
                    {
                        lblSubmitstatus.Text = "Turned In late";
                        lblSubmitstatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblSubmitstatus.Text = "Turned In";
                        lblSubmitstatus.ForeColor = Color.LightBlue;
                    }
                }
                labelIndex++;
                labelTop += 30;
            }
        }


        void RetrieveSubmittedFiles()
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
                selectCmd.Parameters.AddWithValue("@student_id", GetStudentId(studentName));

                db.OpenConnection();
                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string submitFilepath = reader.IsDBNull(reader.GetOrdinal("submitFilename")) ? null : reader.GetString("submitFilename");
                            string submitFilename = Path.GetFileName(submitFilepath);
                            string submitLink = reader.IsDBNull(reader.GetOrdinal("submitLink")) ? null : reader.GetString("submitLink");
                            string submitStatus = reader.GetString("submitStatus");

                            if (!string.IsNullOrEmpty(submitLink) || !string.IsNullOrEmpty(submitFilename))
                            {
                                Panel filePanel = new Panel();
                                filePanel.BorderStyle = BorderStyle.FixedSingle;
                                filePanel.Dock = DockStyle.Top;
                                filePanel.Padding = new Padding(20, 10, 10, 20);

                                if (!string.IsNullOrEmpty(submitLink))
                                {
                                    LinkLabel filenameLabel = new LinkLabel();
                                    filenameLabel.Text = submitLink;
                                    filenameLabel.LinkColor = Color.White;
                                    filenameLabel.VisitedLinkColor = Color.White;
                                    filenameLabel.Font = new Font("Nirmala UI", 11, FontStyle.Regular);
                                    filenameLabel.ForeColor = Color.White;
                                    filenameLabel.Location = new Point(10, 10);
                                    filenameLabel.Dock = DockStyle.Top;

                                    filenameLabel.Click += (sender, e) =>
                                    {
                                        System.Diagnostics.Process.Start("https://" + filenameLabel.Text);
                                    };

                                    filePanel.Controls.Add(filenameLabel);
                                }
                                else if (!string.IsNullOrEmpty(submitFilename))
                                {
                                    byte[] submitDocument = reader.IsDBNull(reader.GetOrdinal("submitDocument")) ? null : (byte[])reader["submitDocument"];
                                    string combinedFilename = string.IsNullOrEmpty(submitFilename) ? null : $"{submitFilename}.pdf";
                                    string localFilePath = submitDocument != null ? SaveTaskDocument(submitDocument, combinedFilename) : null;

                                    LinkLabel filenameLabel = new LinkLabel();
                                    filenameLabel.Text = submitFilename;
                                    filenameLabel.Font = new Font("Nirmala UI", 11, FontStyle.Regular);
                                    filenameLabel.ForeColor = Color.White;
                                    filenameLabel.LinkColor = Color.White;
                                    filenameLabel.VisitedLinkColor = Color.White;
                                    filenameLabel.Location = new Point(10, 10);
                                    filenameLabel.Dock = DockStyle.Top;

                                    filenameLabel.Click += (sender, e) =>
                                    {
                                        if (File.Exists(localFilePath))
                                        {
                                            Process.Start(localFilePath);
                                        }
                                        else
                                        {
                                            MessageBox.Show("File not found.");
                                        }
                                    };

                                    filePanel.Controls.Add(filenameLabel);
                                }

                                panelFill.Controls.Add(filePanel);
                            }
                        }
                    }
                    else
                    {
                        // Add panel with "No attachment or link attached" label
                        Panel filePanel = new Panel();
                        filePanel.BorderStyle = BorderStyle.FixedSingle;
                        filePanel.Dock = DockStyle.Fill;
                        filePanel.Padding = new Padding(50, 50, 50, 40);

                        Label label = new Label();
                        label.Text = "No attachment or link attached";
                        label.ForeColor = Color.White;
                        label.Dock = DockStyle.Top;
                        label.Font = new Font("Nirmala UI", 14, FontStyle.Regular);
                        filePanel.Controls.Add(label);
                        panelFill.Controls.Add(filePanel);
                    }

                    db.CloseConnection();
                }
            }
        }



        private string SaveTaskDocument(byte[] documentBytes, string fileName)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string taskDirectory = Path.Combine(baseDirectory, "TaskDocuments", taskName, studentName);

            if (!Directory.Exists(taskDirectory))
            {
                Directory.CreateDirectory(taskDirectory);
            }

            string filePath = Path.Combine(taskDirectory, fileName);
            File.WriteAllBytes(filePath, documentBytes);

            return filePath;
        }
    }
}

