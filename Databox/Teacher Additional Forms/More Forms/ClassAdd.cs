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
using Databox.Controls;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ClassAdd : Form
    {
        string className, classCode, classCourse, classSection;
        int teacher_id;
        Panel desktopPanel;
        public ClassAdd(int teacherID, Panel pan)
        {
            InitializeComponent();
            teacher_id = teacherID ;
            desktopPanel = pan;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
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

        private void panelCode_Paint(object sender, PaintEventArgs e)
        {
            RoundCorner(sender);
        }

        private void ClassAdd_Load(object sender, EventArgs e)
        {
            Database_Connection dbConnection = new Database_Connection();

            string query = "SELECT * FROM course";

            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                cmbCourse.Items.Add(dataReader["courseName"].ToString());
            }

            dataReader.Close();
            dbConnection.CloseConnection();
        }

        void Fill()
        {
            className = txtClassname.Text;
            classCode = txtClasscode.Text;
            classCourse = cmbCourse.SelectedItem.ToString();
            classSection = txtSection.Text;
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

        void Clear()
        {
            txtClassname.Text = "";
            txtClasscode.Text = "";
            cmbCourse.SelectedIndex = 0;
            txtSection.Text = "";
        }

        string randomStr()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomString = new string(Enumerable.Repeat(chars, 6)
                                      .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        private void btnCreateclass_Click(object sender, EventArgs e)
        {
            Fill();
            if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(classCode) && !string.IsNullOrEmpty(classCourse) && !string.IsNullOrEmpty(classSection))
            {
                string random = randomStr();
                MessageBox.Show("Class Section Created", "Class creation successful", MessageBoxButtons.OK);
                Database_Connection db = new Database_Connection();
                string arch = "NO";

                // Construct query to insert user details into student table
                string query = $"INSERT INTO class (className, classCode, classCourse, classSection, classArchive, classRCode, teacher_id) " +
                                $"VALUES ('{className}', '{classCode}', '{classCourse}', '{classSection}', '{arch}', '{random}', '{teacher_id}');";

                // Execute query
                db.ExecuteQuery(query);
                Clear();
                ClassCode cc = new ClassCode(random);
                
                Classes classesForm = new Classes(teacher_id, desktopPanel);
                classesForm.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(classesForm);
                classesForm.Dock = DockStyle.Fill;
                classesForm.FormBorderStyle = FormBorderStyle.None;
                cc.Show();
                classesForm.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please enter information on the required fields", "Error", MessageBoxButtons.OK);

        }
    }    
}
