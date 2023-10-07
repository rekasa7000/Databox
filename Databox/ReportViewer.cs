using Apitron.PDF.Rasterizer;
using Databox.Teacher_Additional_Forms.More_Forms;
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

namespace Databox
{
    public partial class ReportViewer : Form
    {
        Panel desktopPanel;
        int teacher_id, student_id, class_id;
        string studentName, className;
        private Database_Connection dbConnection;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string reportsDirectory = Path.Combine(baseDirectory, "reports");
                string classDirectory = Path.Combine(reportsDirectory, className);
                string filePath = Path.Combine(classDirectory, $"{studentName}.pdf");
                Console.WriteLine(filePath);

                FileStream fs = new FileStream(filePath, FileMode.Open);
                pdfViewer.Document = new Document(fs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        public ReportViewer(int teacherID, int studentID, int classID, Panel pan)
        {
            teacher_id = teacherID;
            student_id = studentID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
            dbConnection = new Database_Connection();
            getStudentName(); getClassName();
        }

        private void getStudentName()
        {
            string query = $"SELECT CONCAT(studLastname, ', ', studFirstname, ' ', studMiddlename) as studName FROM student WHERE student_id = {student_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                studentName = studentTable.Rows[0]["studName"].ToString();
            }
        }

        private void getClassName()
        {
            string query = $"SELECT className FROM class WHERE class_id = {class_id};";
            DataTable studentTable = dbConnection.ExecuteSelectQuery(query);

            if (studentTable.Rows.Count > 0)
            {
                className = studentTable.Rows[0]["className"].ToString();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ProgressGraph classesForm = new ProgressGraph(teacher_id,  class_id, student_id, desktopPanel);
            classesForm.TopLevel = false;
            desktopPanel.Controls.Clear();
            desktopPanel.Controls.Add(classesForm);
            classesForm.Dock = DockStyle.Fill;
            classesForm.FormBorderStyle = FormBorderStyle.None;
            classesForm.Show();
            this.Close();
        }
    }
}
