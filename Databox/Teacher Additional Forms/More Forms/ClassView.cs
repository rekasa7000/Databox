using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ClassView : Form
    {
        int teacher_id, class_id, currentIndex;
        Panel desktopPanel;
        public ClassView(int teacherID, int classID, Panel pan)
        {
            teacher_id = teacherID;
            class_id = classID;
            desktopPanel = pan;
            InitializeComponent();
        }
        private void RoundCorner(object sender)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Create a rounded rectangle using the size of the panel and a radius of 10 pixels
                GraphicsPath path = new GraphicsPath();
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, 20, 20), 180, 90);
                path.AddLine(20, 0, panel.Width - 20, 0);
                path.AddArc(new Rectangle(panel.Width - 20, 0, 20, 20), -90, 90);
                path.AddLine(panel.Width, 20, panel.Width, panel.Height - 20);
                path.AddArc(new Rectangle(panel.Width - 20, panel.Height - 20, 20, 20), 0, 90);
                path.AddLine(panel.Width - 20, panel.Height, 20, panel.Height);
                path.AddArc(new Rectangle(0, panel.Height - 20, 20, 20), 90, 90);
                path.CloseFigure();

                // Set the region of the panel to the rounded rectangle
                panel.Region = new Region(path);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
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

        void loadStudent(int startIndex)
        {
            Database_Connection db = new Database_Connection();            
            List<string> fullNames = new List<string>();
            List<string> emails = new List<string>();
            List<string> courses = new List<string>();
            List<string> appointments = new List<string>();

            db.OpenConnection();

            string query = "SELECT CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) AS fullName, u.email, c.classCourse AS course, " +
               "CASE WHEN sched.student_id IS NOT NULL THEN sched.schedName ELSE 'None' END AS appointmentName " +
               "FROM student s " +
               "JOIN user u ON u.user_id = s.user_id " +
               "JOIN student_class sc ON sc.student_id = s.student_id " +
               "JOIN class c ON c.class_id = sc.class_id " +
               "LEFT JOIN schedule sched ON sched.student_id = s.student_id " +
               "WHERE c.class_id = @class_id AND c.classArchive = 'NO'" +
               "LIMIT @startIndex, 5";


            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@startIndex", startIndex);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentName = reader.GetString("fullName");
                        fullNames.Add(studentName);
                        string email = reader.GetString("email");
                        emails.Add(email);
                        string course = reader.GetString("course");
                        courses.Add(course);
                        string appointment = reader.GetString("appointmentName");
                        appointments.Add(appointment);
                    }
                }                
            }
            db.CloseConnection();
            for (int i = 0; i < fullNames.Count; i++)
            {
                Label label = Controls.Find("lblStudentname" + (i + 1), true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblCourse" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblAppointment" + (i + 1), true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = fullNames[i];
                    label1.Text = emails[i];
                    label2.Text = courses[i];
                    label3.Text = appointments[i];
                }
            }

            if (fullNames.Count == 0)
            {
                MessageBox.Show("No more students to display.");
                currentIndex -= 5; // Move the current index back to the previous set of data
            }
        }
        private void ClassView_Load(object sender, EventArgs e)
        {
            loadStudent(0);
        }
    }
}

