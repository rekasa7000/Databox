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
    public partial class ClassEdit : Form
    {
        int teacher_id, courseID, class_id;
        string name, code, section, course;
        string className, classCode, classCourse, classSection;
        Panel desktopPanel;
        public ClassEdit(int teacherId, Panel pan)
        {
            InitializeComponent();
            teacher_id = teacherId;
            desktopPanel = pan;
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

        private void panelClass_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelName_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelLine_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelCode_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelCourse_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelSubject_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void panelSection_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            getTexts();
            if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(classCode) && !string.IsNullOrEmpty(classCourse) && !string.IsNullOrEmpty(classSection))
            {
                // Prompt user to confirm saving changes
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirm Save Changes", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Edit Successful", "Class creation successful", MessageBoxButtons.OK);
                    Database_Connection db = new Database_Connection();

                    // Construct query to update user details in student table
                    string query = $"UPDATE class " +
                                    $"SET className = '{className}', classCourse = '{classCourse}', classSection = '{classSection}' " +
                                    $"WHERE teacher_id = '{teacher_id}' and class_id = '{class_id}';";

                    // Execute query
                    db.ExecuteQuery(query);

                    Classes classesForm = new Classes(teacher_id, desktopPanel);
                    classesForm.TopLevel = false;
                    desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                    desktopPanel.Controls.Add(classesForm);
                    classesForm.Dock = DockStyle.Fill;
                    classesForm.FormBorderStyle = FormBorderStyle.None;
                    classesForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter information on the required fields", "Error", MessageBoxButtons.OK);
            }
        }


        void retrieveCourseid()
        {
            // First, create an instance of the Database_Connection class
            Database_Connection dbConnection = new Database_Connection();

            
            string query = $"SELECT course_id FROM course WHERE courseName='{course}'";

            // Execute the query and retrieve the course_id
            try
            {
                dbConnection.OpenConnection();
                object result = dbConnection.ExecuteScalar(query);

                // Check if the query returned a value and cast it to an int
                courseID = result != null ? Convert.ToInt32(result) : -1;                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the course_id: {ex.Message}");
            }
            finally
            {
                dbConnection.CloseConnection();
            }

        }

        void populateCourse()
        {
            // create an instance of the Database_Connection class
            Database_Connection dbConnection = new Database_Connection();

            // create a query to retrieve data from the database, selecting only the courseName column
            string query = "SELECT courseName FROM course";

            // use the GetData method to retrieve the data
            MySqlDataReader dataReader = dbConnection.GetData(query);

            // iterate over the data and add it to the ComboBox
            while (dataReader.Read())
            {
                cmbCourse.Items.Add(dataReader["courseName"].ToString());
            }

            // close the data reader and database connection
            dataReader.Close();
            dbConnection.CloseConnection();
        }
        
        void populateTextfields()
        {
            txtClassname.Text = name;
            txtClasscode.Text = code;
            cmbCourse.SelectedItem = course;
            txtSection.Text = section;
        }


        void getTexts()
        {
            className = txtClassname.Text;
            classCode = txtClasscode.Text;
            classCourse = cmbCourse.SelectedItem.ToString();
            classSection = txtSection.Text;
        }

        private void ClassEdit_Load(object sender, EventArgs e)
        {
        }
        public void LoadData(int classId, string cName, string cSection, string cCode, string cCourse)
        {
            class_id = classId;
            name = cName;
            section = cSection;
            course = cCourse;
            code = cCode;

            populateCourse();
            populateTextfields();
        }
    }
}
