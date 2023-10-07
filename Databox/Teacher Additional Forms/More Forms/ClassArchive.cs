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
    public partial class ClassArchive : Form
    {
        int teacher_id,  totalClasses, currentIndex;
        Panel desktopPanel;
        string archive = "Unarchive class";
        public ClassArchive(int teacherID, Panel pan)
        {
            desktopPanel = pan;
            teacher_id = teacherID;
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

        private void LoadData()
        {
            Database_Connection db = new Database_Connection();

            // Construct query to select all rows from class table
            string query = "SELECT class_id, className, classCode, classCourse, classSection FROM class where teacher_id =" + teacher_id + " and classArchive = 'YES'";


            // Get the data
            MySqlDataReader dataReader = db.GetData(query);

            // Create a DataTable to hold the data
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            dataReader.Close();
            db.CloseConnection();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Unarchive this class?", "Unarchive Class", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Button btn = (Button)sender; // Get the clicked button
                string classID = btn.Tag.ToString(); // Retrieve the class_id from the button's Tag property

                if (!string.IsNullOrEmpty(classID))
                {
                    MessageBox.Show("Class Archived!", "Successful", MessageBoxButtons.OK);
                    Database_Connection db = new Database_Connection();

                    // Construct query to update class details in the database
                    string query = $"UPDATE class " +
                                    $"SET classArchive = 'NO' " +
                                    $"WHERE teacher_id = '{teacher_id}' and class_id = '{classID}';";

                    // Execute query
                    db.ExecuteQuery(query);
                    Clear();
                    classLoad(currentIndex);
                }
                else
                {
                    MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Do nothing or perform a different action
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentIndex -= 5; // Decrement the current index by 5
            if (currentIndex < 0)
            {
                MessageBox.Show("You are already at the beginning.");
                currentIndex = 0; // Ensure the index doesn't go below 0
            }
            classLoad(currentIndex);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Clear();
            currentIndex += 5; // Increment the current index by 5

            if (currentIndex >= totalClasses)
            {
                MessageBox.Show("No more classes to display.");
                currentIndex -= 5;
            }
            classLoad(currentIndex);
        }

        void Clear()
        {
            for (int i = 1; i <= 5; i++)
            {
                Label label = Controls.Find("lblClass" + i, true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + i, true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblSection" + i, true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblCourse" + i, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = "Empty.";
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
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
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class WHERE teacher_id = @teacher_id AND classArchive = 'YES'", db.connection))
            {
                countCmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
        }
        private void ClassArchive_Load(object sender, EventArgs e)
        {
            CalculateTotalClasses();
            classLoad(0);
            for (int i = 1; i <= 5; i++)
            {
                Button button = Controls.Find("btnArchive" + i, true).FirstOrDefault() as Button;
                if (button != null)
                {
                    ttArchive.SetToolTip(button, archive);
                }
            }
        }
        void classLoad(int startIndex)
        {
            Database_Connection db = new Database_Connection();
            List<string> classNames = new List<string>();
            List<string> classCodes = new List<string>();
            List<string> classSections = new List<string>();
            List<string> classCourses = new List<string>();
            List<string> classIDs = new List<string>();

            db.OpenConnection();

            // Create the SQL query
            string query = "SELECT class_id, className, classSection, classCourse, classCode, classRCode FROM class WHERE teacher_id = @teacher_id AND classArchive = 'YES' LIMIT @startIndex, 5";

            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                cmd.Parameters.AddWithValue("@startIndex", startIndex);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string className = reader.GetString("className");
                        classNames.Add(className);
                        string classCode = reader.GetString("classCode");
                        classCodes.Add(classCode);
                        string classSection = reader.GetString("classSection");
                        classSections.Add(classSection);
                        string classCourse = reader.GetString("classCourse");
                        classCourses.Add(classCourse);
                        string classID = reader.GetString("class_id");
                        classIDs.Add(classID);
                    }
                }

            }
            db.CloseConnection();

            for (int i = 0; i < classNames.Count; i++)
            {
                Label label = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + (i + 1), true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblSection" + (i + 1), true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblCourse" + (i + 1), true).FirstOrDefault() as Label;
                Button btnArchive = Controls.Find("btnArchive" + (i + 1), true).FirstOrDefault() as Button;
                if (btnArchive != null)
                {
                    btnArchive.Tag = classIDs[i];
                }
                Button btnEdit = Controls.Find("btnEdit" + (i + 1), true).FirstOrDefault() as Button;

                if (label != null)
                {
                    label.Text = classNames[i];
                    label1.Text = classCodes[i];
                    label2.Text = classSections[i];
                    label3.Text = classCourses[i];
                }
            }
        }
    }
}
