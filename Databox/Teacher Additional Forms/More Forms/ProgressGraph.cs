using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using System.Drawing;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ProgressGraph : Form
    {
        private int teacher_id, class_id, student_id;
        private Panel desktopPanel;
        private Database_Connection dbConnection;
        private string className, section, teacherName, filePath;

        public ProgressGraph(int teacherID, int classID, int studentID, Panel pan)
        {
            teacher_id = teacherID;
            class_id = classID;
            student_id = studentID;
            desktopPanel = pan;
            InitializeComponent();
            dbConnection = new Database_Connection();
            getStudentName();
            getClassName();
            getTeacherName();
            btnViewVisible();
        }
        private void getStudentName()
        {
            string query = $"SELECT CONCAT(studLastname, ', ', studFirstname, ' ', studMiddlename) as studName FROM student WHERE student_id = {student_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                string studName = studentTable.Rows[0]["studName"].ToString();
                lblStudent.Text = studName;
            }
        }

        private void getTeacherName()
        {
            string query = $"SELECT CONCAT(teacherLastname, ', ', teacherFirstname, ' ', teacherMiddlename) as studName FROM teacher WHERE teacher_id = {teacher_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                teacherName = studentTable.Rows[0]["studName"].ToString();
            }
        }

        private void getClassName()
        {
            string query = $"SELECT className, classSection FROM class WHERE class_id = {class_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                className = studentTable.Rows[0]["className"].ToString();
                    section = studentTable.Rows[0]["classSection"].ToString();
                }
        }
    private void ProgressGraph_Load(object sender, EventArgs e)
        {
            string query = $@"
    SELECT task.taskName, task.taskType,
        CASE
            WHEN CONCAT(task.taskDuedate, ' ', task.taskDuetime) > NOW() AND submission.submission_id IS NULL THEN 'Assigned'
            WHEN submission.submission_id IS NULL THEN 'Missing'
            WHEN submission.submitScore IS NULL THEN 'Not Graded'
            ELSE 'Graded'
        END AS submissionStatus, 
        CASE
            WHEN submission.submission_id IS NULL THEN 0
            WHEN submission.submitScore IS NULL THEN 0
            ELSE submission.submitScore/task.taskPoints * 100
        END AS score
    FROM task
    LEFT JOIN submission ON submission.task_id = task.task_id AND submission.student_id = {student_id}
    JOIN class ON class.class_id = task.class_id
    WHERE class.class_id = {class_id};";


            DataTable scoresTable = dbConnection.ExecuteSelectQuery(query);

            Dictionary<string, List<(string taskName, int? score)>> scoresByTaskType = new Dictionary<string, List<(string, int?)>>();
            List<string> allTaskNames = new List<string>();

            foreach (DataRow row in scoresTable.Rows)
            {
                string taskName = row["taskName"].ToString();
                int? score = Convert.ToInt32(row["score"]) as int?;
                string taskType = row["taskType"].ToString();

                if (!scoresByTaskType.ContainsKey(taskType))
                {
                    scoresByTaskType[taskType] = new List<(string, int?)>();
                }

                if (score != null)
                {
                    score = (int)Math.Round((double)score / 100.0 * 100);
                }

                scoresByTaskType[taskType].Add((taskName, score));
                allTaskNames.Add(taskName);
            }

            // Remove duplicate task names
            List<string> taskNames = allTaskNames.Distinct().ToList();

            DisplayGraph(scoresByTaskType, taskNames);
            DisplayTaskStatusPieChart(scoresTable);
            CalculateMetrics(scoresTable);
        }
        private void DisplayGraph(Dictionary<string, List<(string taskName, int? score)>> scoresByTaskType, List<string> taskNames)
        {
            panelBargraphs.Controls.Clear();

            int chartWidth = 400;
            int chartHeight = 300;
            int spacing = 20;
            int currentY = spacing;

            foreach (var kvp in scoresByTaskType)
            {
                string taskType = kvp.Key;
                List<(string taskName, int? score)> taskScores = kvp.Value;

                if (taskScores.Count == 0)
                {
                    Label label = new Label();
                    label.Text = $"No {taskType}s created.";
                    label.AutoSize = true;
                    panelBargraphs.Controls.Add(label);
                    label.Dock = DockStyle.Top;
                    continue;
                }

                Chart chart = new Chart();
                chart.Size = new Size(chartWidth, chartHeight);
                chart.Location = new Point(spacing, currentY);
                chart.Titles.Add($"Student Scores for {taskType}");
                chart.ChartAreas.Add(new ChartArea());
                Series series = new Series(taskType);
                series.ChartType = SeriesChartType.Column;

                for (int i = 0; i < taskScores.Count; i++)
                {
                    string taskName = taskScores[i].taskName;
                    int? score = taskScores[i].score;
                    int index = taskNames.IndexOf(taskName);
                    series.Points.AddXY($"{taskName}", score);
                    series.Points[i].AxisLabel = $"{taskName}";
                }

                chart.Series.Add(series);
                chart.Dock = DockStyle.Top;
                panelBargraphs.Controls.Add(chart);
                currentY += chartHeight + spacing;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentProgressList classesForm = new StudentProgressList(teacher_id, desktopPanel);
            classesForm.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(classesForm);
            classesForm.Dock = DockStyle.Fill;
            classesForm.FormBorderStyle = FormBorderStyle.None;
            classesForm.Show();
            this.Hide();
        }

        private void DisplayTaskStatusPieChart(DataTable scoresTable)
        {
            Dictionary<string, int> taskStatusCounts = new Dictionary<string, int>
    {
        { "Assigned", 0 },
        { "Missing", 0 },
        { "Not Graded", 0 },
        { "Graded", 0 }
    };

            foreach (DataRow row in scoresTable.Rows)
            {
                string submissionStatus = row["submissionStatus"].ToString();
                if (taskStatusCounts.ContainsKey(submissionStatus))
                    taskStatusCounts[submissionStatus]++;
            }

            Chart chart = new Chart();
            chart.Size = new Size(400, 300);
            chart.Titles.Add("Task Status");
            chart.ChartAreas.Add(new ChartArea());

            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Outside";

            foreach (var kvp in taskStatusCounts)
            {
                string taskStatus = kvp.Key;
                int count = kvp.Value;

                if (count > 0)
                {
                    double percentage = (double)count / scoresTable.Rows.Count;
                    string labelText = $"{percentage:P0} {taskStatus}"; // Construct the label text with percentage and task status
                    series.Points.AddXY(labelText, count);
                    series.Points[series.Points.Count - 1].LegendText = taskStatus; // Set the legend text as task status
                }
            }

            chart.Series.Add(series);
            chart.Dock = DockStyle.Fill;

            panelPiechart.Controls.Add(chart);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer classesForm = new ReportViewer(teacher_id, student_id, class_id, desktopPanel);
            classesForm.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(classesForm);
            classesForm.Dock = DockStyle.Fill;
            classesForm.FormBorderStyle = FormBorderStyle.None;
            classesForm.Show();
            this.Hide();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {            
            DialogResult result = MessageBox.Show("Are you sure you want to release this report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dbConnection.OpenConnection();
                string query = "UPDATE report SET isReleased = @classId WHERE student_id = @studentId";
                MySqlCommand command = new MySqlCommand(query, dbConnection.connection);
                command.Parameters.AddWithValue("@classId", 1);
                command.Parameters.AddWithValue("@studentId", student_id);
                command.ExecuteNonQuery();
                dbConnection.CloseConnection();
                MessageBox.Show("Report Released Successfully");
            }
        }


        private void btnGenerate_Click(object sender, EventArgs e)
    {     
        string studentName = lblStudent.Text;

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string reportsDirectory = Path.Combine(baseDirectory, "reports");
        string classDirectory = Path.Combine(reportsDirectory, className);
        filePath = Path.Combine(classDirectory, $"{studentName}.pdf");

            Directory.CreateDirectory(classDirectory);
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    document.SetMargins(50, 50, 50, 50);

                    var header = new Paragraph("Academic Progress Data Report")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16);
                    document.Add(header);

                    document.Add(new Paragraph($"Name: {studentName}"));
                document.Add(new Paragraph($"Class: {className}"));
                    document.Add(new Paragraph($"Section: {section}"));
                    document.Add(new Paragraph($"Teacher: {teacherName}"));
                    var currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
                    document.Add(new Paragraph($"Date Created: {currentDate}"));
                    document.Add(new Paragraph(""));
                    foreach (Control control in panelPiechart.Controls)
                    {
                        if (control is Chart chart)
                        {
                            string tempImagePath = "temp.png";
                            chart.SaveImage(tempImagePath, ChartImageFormat.Png);
                            using (var image = new Bitmap(tempImagePath))
                            {
                                var pdfImage = new iText.Layout.Element.Image(ImageDataFactory.Create(image, null));
                                document.Add(pdfImage);
                            }
                            File.Delete(tempImagePath);
                        }
                    }
                    foreach (Control control in panelBargraphs.Controls)
                    {
                        if (control is Chart chart)
                        {
                            string tempImagePath = "temp.png";
                            chart.SaveImage(tempImagePath, ChartImageFormat.Png);
                            using (var image = new Bitmap(tempImagePath))
                            {
                                var pdfImage = new iText.Layout.Element.Image(ImageDataFactory.Create(image, null));
                                document.Add(pdfImage);
                            }
                            File.Delete(tempImagePath);
                        }
                    }
                    document.Add(new Paragraph(lblAverageScore.Text));
                    document.Add(new Paragraph(lblCumulativeScore.Text));
                    document.Add(new Paragraph(lblWeightedAverage.Text));
                    document.Add(new Paragraph(lblHighestScore.Text));
                    document.Add(new Paragraph(lblLowestScore.Text));
                    document.Add(new Paragraph(lblPercentageCorrectAnswers.Text));
                    document.Add(new Paragraph(lblLetterGrade.Text));
                    document.Add(new Paragraph("Comment:"));
                    document.Add(new Paragraph(txtComment.Text));
                    document.Close();
                }
            }

            byte[] pdfBytes = File.ReadAllBytes(filePath);
            InsertPdfIntoDatabase($"{studentName}.pdf", pdfBytes, class_id, student_id);            
            btnViewVisible();
        }

        private void InsertPdfIntoDatabase(string reportTitle, byte[] pdfBytes, int classId, int studentId)
        {
            Database_Connection dbConnection = new Database_Connection();
            if (dbConnection.OpenConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM report WHERE student_id = @studentId";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, dbConnection.connection);
                checkCommand.Parameters.AddWithValue("@studentId", studentId);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                string query;
                if (count > 0)
                {
                    DialogResult result = MessageBox.Show("A report already exists for this student. Do you want to update the existing report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        query = "UPDATE report SET reportTitle = @reportTitle, reportDocument = @reportDocument, class_id = @classId WHERE student_id = @studentId";
                        MessageBox.Show("PDF created and saved to the database successfully!");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    query = "INSERT INTO report (reportTitle, reportDocument, class_id, student_id) VALUES (@reportTitle, @reportDocument, @classId, @studentId)";
                    MessageBox.Show("PDF created and saved to the database successfully!");
                }

                MySqlCommand command = new MySqlCommand(query, dbConnection.connection);
                command.Parameters.AddWithValue("@reportTitle", reportTitle);
                command.Parameters.AddWithValue("@reportDocument", pdfBytes);
                command.Parameters.AddWithValue("@classId", classId);
                command.Parameters.AddWithValue("@studentId", studentId);
                command.ExecuteNonQuery();
                dbConnection.CloseConnection();
            }
        }

        void btnViewVisible()
        {
            Database_Connection dbConnection = new Database_Connection();
            if (dbConnection.OpenConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM report WHERE student_id = @studentId";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, dbConnection.connection);
                checkCommand.Parameters.AddWithValue("@studentId", student_id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count > 0)
                {
                    btnView.Visible = true;
                    btnRelease.Visible = true;
                }
                else
                {
                    btnView.Visible = false;
                    btnRelease.Visible = false;
                }
            }
        }

        private void CalculateMetrics(DataTable scoresTable)
        {
            int totalAssessments = scoresTable.Rows.Count;
            double sumOfScores = 0;
            double cumulativeScore = 0;
            Dictionary<string, int> assessmentCounts = new Dictionary<string, int>();
            Dictionary<string, double> assessmentScores = new Dictionary<string, double>();
            List<double> allScores = new List<double>();

            foreach (DataRow row in scoresTable.Rows)
            {
                double score = Convert.ToDouble(row["score"]);
                double maximumScore = 100; 
                double convertedScore = score / maximumScore * 100;

                sumOfScores += convertedScore;
                cumulativeScore += convertedScore;
                allScores.Add(convertedScore);

                string assessmentType = row["taskType"].ToString();
                if (assessmentCounts.ContainsKey(assessmentType))
                {
                    assessmentCounts[assessmentType]++;
                    assessmentScores[assessmentType] += convertedScore;
                }
                else
                {
                    assessmentCounts[assessmentType] = 1;
                    assessmentScores[assessmentType] = convertedScore;
                }
            }

            double averageScore = sumOfScores / totalAssessments;

            // Calculate weighted average
            Dictionary<string, double> assessmentWeights = new Dictionary<string, double>
    {
        { "Activity", 0.3 },
        { "Quiz", 0.4 },
        { "Exam", 0.3 }
    };

            double weightedAverage = 0;
            double totalWeight = 0;

            foreach (var kvp in assessmentScores)
            {
                string assessmentType = kvp.Key;
                double assessmentScore = kvp.Value;
                double assessmentWeight = assessmentWeights[assessmentType];

                weightedAverage += assessmentScore * assessmentWeight;
                totalWeight += assessmentWeight;
            }

            weightedAverage /= totalWeight;

            double lowestScore = 0, highestScore = 0;
            if (scoresTable.Rows.Count > 0)
            {
                highestScore = scoresTable.AsEnumerable().Max(row => Convert.ToDouble(row["score"]));
                lowestScore = scoresTable.AsEnumerable().Min(row => Convert.ToDouble(row["score"]));
            }
            double percentageCorrectAnswers = 0;
            int totalCorrectAnswers = scoresTable.AsEnumerable().Count(row => row["submissionStatus"].ToString() == "Graded" && Convert.ToDouble(row["score"]) > 0);
            if (totalAssessments > 0)
                percentageCorrectAnswers = (double)totalCorrectAnswers / totalAssessments * 100;

            string letterGrade = ConvertScoreToLetterGrade(weightedAverage);


            lblAverageScore.Text = "Average Score: " + averageScore.ToString("0.00");
            lblCumulativeScore.Text = "Cumulative Score: " + cumulativeScore.ToString("0.00");
            lblWeightedAverage.Text = "Weighted Average: " + weightedAverage.ToString("0.00");
            lblHighestScore.Text = "Highest Score: " + highestScore.ToString("0.00");
            lblLowestScore.Text = "Lowest Score: " + lowestScore.ToString("0.00");
            lblPercentageCorrectAnswers.Text = "Percentage of Correct Answers: " + percentageCorrectAnswers.ToString("0.00") + "%";
            lblLetterGrade.Text = "Letter Grade: (Estimated) " + letterGrade;

        }

        private string ConvertScoreToLetterGrade(double score)
        {
            if (score >= 90)
                return "A";
            else if (score >= 80)
                return "B";
            else if (score >= 70)
                return "C";
            else if (score >= 60)
                return "D";
            else
                return "F";
        }
    }
}
