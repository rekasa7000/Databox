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

namespace Databox.Student_Additional_Forms.More_Forms
{    
    public partial class Reports : Form
    {
        private Database_Connection dbConnection;
        int student_id;
        Panel desktopPanel;
        public Reports(int studentID, Panel pan)
        {
            student_id = studentID;
            desktopPanel = pan;
            InitializeComponent();
            dbConnection = new Database_Connection();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            PopulateClassAndTeacherNames(student_id);
        }

        private void PopulateClassAndTeacherNames(int studentId)
        {
            try
            {
                string query = $"SELECT class.className, CONCAT(teacher.teacherFirstname, ' ', teacher.teacherLastname) as teacherName " +
                               $"FROM report " +
                               $"JOIN class ON report.class_id = class.class_id " +
                               $"JOIN teacher ON class.teacher_id = teacher.teacher_id " +
                               $"WHERE student_id = {studentId}";
                DataTable resultTable = dbConnection.ExecuteSelectQuery(query);

                // Clear the labels
                ClearLabels();

                if (resultTable.Rows.Count > 0)
                {
                    for (int i = 0; i < resultTable.Rows.Count; i++)
                    {
                        if (i < 8)
                        {
                            string className = resultTable.Rows[i]["className"].ToString();
                            string teacherName = resultTable.Rows[i]["teacherName"].ToString();

                            // Set the class name label
                            Label lblClassName = (Label)Controls.Find($"lblReport{i + 1}", true).FirstOrDefault();
                            if (lblClassName != null)
                                lblClassName.Text = className;

                            // Set the teacher name label
                            Label lblTeacherName = (Label)Controls.Find($"lblClass{i + 1}", true).FirstOrDefault();
                            if (lblTeacherName != null)
                                lblTeacherName.Text = teacherName;
                        }
                        else
                        {
                            break; // Exit the loop if there are more than 8 results
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No classes found for the student.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ClearLabels()
        {
            for (int i = 1; i <= 8; i++)
            {
                Label lblClassName = (Label)Controls.Find($"lblReport{i}", true).FirstOrDefault();
                if (lblClassName != null)
                    lblClassName.Text = "Empty";

                Label lblTeacherName = (Label)Controls.Find($"lblClass{i}", true).FirstOrDefault();
                if (lblTeacherName != null)
                    lblTeacherName.Text = string.Empty;
            }
        }
        private void lblClassName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel report = (LinkLabel)sender;
            string className = report.Text;

            // Retrieve the report document based on the class name
            string query = $"SELECT reportDocument FROM report JOIN class ON report.class_id = class.class_id WHERE class.className = '{className}' AND student_id = {student_id}";
            DataTable resultTable = dbConnection.ExecuteSelectQuery(query);

            if (resultTable.Rows.Count > 0)
            {
                byte[] reportData = (byte[])resultTable.Rows[0]["reportDocument"];

                // Show the folder browser dialog to select the destination directory
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        string destinationPath = folderBrowserDialog.SelectedPath;
                        string fileName = $"{className}_Report.pdf"; // Change the file name as needed

                        string fullPath = Path.Combine(destinationPath, fileName);

                        // Save the report to the selected directory
                        File.WriteAllBytes(fullPath, reportData);

                        MessageBox.Show("Report saved successfully.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No report found for the selected class.");
            }
        }


    }
}
