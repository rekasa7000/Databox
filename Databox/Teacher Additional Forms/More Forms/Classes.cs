using MySql.Data.MySqlClient;
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
    public partial class Classes : Form
    {
        int teacher_id, currentIndex, totalClasses;
        string student = "Show Students";
        string edit = "Edit class";
        string archive = "Archive class";
        string copy = "Copy Class Code";
        Panel desktopPanel;
        public Classes(int teacherID, Panel pan)
        {
            desktopPanel = pan;
            teacher_id = teacherID;
            InitializeComponent();
        }

        private void Classes_Load(object sender, EventArgs e)
        {
            CalculateTotalClasses();
            classLoad(0);
            for (int i = 1; i <= 5; i++)
            {
                Button button = Controls.Find("btnStudent" + i, true).FirstOrDefault() as Button;
                Button button1 = Controls.Find("btnEdit" + i, true).FirstOrDefault() as Button;
                Button button2 = Controls.Find("btnArchive" + i, true).FirstOrDefault() as Button;
                Button button3 = Controls.Find("btnCopy" + i, true).FirstOrDefault() as Button;
                if (button != null)
                {
                    ttStudents.SetToolTip(button, student);
                    ttEdit.SetToolTip(button1, edit);
                    ttArchive.SetToolTip(button2, archive);
                    ttCopy.SetToolTip(button3, copy);
                }
            }            
        }

        void Clear()
        {
            for (int i = 1; i <= 5; i++)
            {
                Label label = Controls.Find("lblClass" + i, true).FirstOrDefault() as Label;
                Label label1 = Controls.Find("lblCode" + i, true).FirstOrDefault() as Label;
                Label label2 = Controls.Find("lblSection" + i, true).FirstOrDefault() as Label;
                Label label3 = Controls.Find("lblCourse" + i, true).FirstOrDefault() as Label;
                TextBox text = Controls.Find("txtCode" + (i), true).FirstOrDefault() as TextBox;
                if (label != null)
                {
                    label.Text = "Empty.";
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                    text.Text = "";
                }
            }
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
            Console.WriteLine(currentIndex);
            classLoad(currentIndex);
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

        private void btnCreateclass_Click(object sender, EventArgs e)
        {
            ClassAdd add = new ClassAdd(teacher_id, desktopPanel);
            add.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(add);
            add.Dock = DockStyle.Fill;
            add.FormBorderStyle = FormBorderStyle.None;
            add.Show();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to archive this class?", "Archive Class", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                                    $"SET classArchive = 'YES' " +
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string classIDString = btn.Tag.ToString(); 

            if (int.TryParse(classIDString, out int classID))
            {
                // Retrieve the class data from the database using the classID
                string query = $"SELECT className, classSection, classCode, classCourse FROM class WHERE class_id = {classID};";

                // Execute the query and retrieve the data
                Database_Connection db = new Database_Connection();
                MySqlDataReader dataReader = db.GetData(query);

                if (dataReader.Read())
                {
                    // Retrieve the data from the dataReader
                    string className = dataReader["className"].ToString();
                    string classSection = dataReader["classSection"].ToString();
                    string classCode = dataReader["classCode"].ToString();
                    string classCourse = dataReader["classCourse"].ToString();

                    // Pass the data to the ClassEdit form
                    ClassEdit edit = new ClassEdit(teacher_id, desktopPanel);
                    edit.TopLevel = false;
                    desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                    desktopPanel.Controls.Add(edit);
                    edit.Dock = DockStyle.Fill;
                    edit.FormBorderStyle = FormBorderStyle.None;                    
                    edit.LoadData(classID, className, classSection, classCode, classCourse); // Load the class data into the ClassEdit form
                    edit.Show();
                    classLoad(currentIndex);
                }
                else
                {
                    MessageBox.Show("Class not found in the database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                dataReader.Close(); // Close the data reader
            }
            else
            {
                MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void CalculateTotalClasses()
        {
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM class WHERE teacher_id = @teacher_id AND classArchive = 'NO'", db.connection))
            {
                countCmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
            }
            db.CloseConnection();
        }

        void classLoad(int startIndex)
        {
            Database_Connection db = new Database_Connection();
            List<string> classNames = new List<string>();
            List<string> classCodes = new List<string>();
            List<string> classSections = new List<string>();
            List<string> classCourses = new List<string>();
            List<string> classRcodes = new List<string>();
            List<string> classIDs = new List<string>();

            db.OpenConnection();

            // Create the SQL query
            string query = "SELECT class_id, className, classSection, classCourse, classCode, classRCode FROM class WHERE teacher_id = @teacher_id AND classArchive = 'NO' LIMIT @startIndex, 5";

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
                        string classRcode = reader.GetString("classRCode");
                        classRcodes.Add(classRcode);
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
                TextBox label4 = Controls.Find("txtCode" + (i + 1), true).FirstOrDefault() as TextBox;
                Button btnArchive = Controls.Find("btnArchive" + (i + 1), true).FirstOrDefault() as Button;
                if (btnArchive != null)
                {
                    btnArchive.Enabled = true;
                    btnArchive.Tag = classIDs[i];
                }
                Button btnEdit = Controls.Find("btnEdit" + (i + 1), true).FirstOrDefault() as Button;
                if (btnEdit!= null)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Tag = classIDs[i];
                }
                Button btnStudents= Controls.Find("btnStudent" + (i + 1), true).FirstOrDefault() as Button;
                if (btnStudents != null)
                {
                    btnStudents.Enabled = true;
                    btnStudents.Tag = classIDs[i];
                }
                Button btnCopy = Controls.Find("btnCopy" + (i + 1), true).FirstOrDefault() as Button;
                if (label4 != null && btnCopy != null)
                {
                    btnCopy.Enabled = true;
                    // Remove the previous event handler if any
                    btnCopy.Click -= CopyToClipboard_Click;

                    // Attach the event handler
                    btnCopy.Click += CopyToClipboard_Click;
                }
                LinkLabel lblTask = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as LinkLabel;
                if (lblTask != null)
                {
                    lblTask.Tag = classIDs[i];
                }

                if (label != null)
                {
                    label.Text = classNames[i];
                    label1.Text = classCodes[i];
                    label2.Text = classSections[i];
                    label3.Text = classCourses[i];
                    label4.Text = classRcodes[i];
                }
            }

            if (classNames.Count == 0)
            {
                MessageBox.Show("No more classes to display.");
                currentIndex -= 5; // Move the current index back to the previous set of data
            }
        }
        private void CopyToClipboard_Click(object sender, EventArgs e)
        {
            Button btnCopy = (Button)sender;
            TextBox label4 = btnCopy.Parent.Controls.Find("txtCode" + btnCopy.Name.Substring(7), true).FirstOrDefault() as TextBox;
            string code1 = label4.Text;

            if (label4 != null && !string.IsNullOrWhiteSpace(code1))
            {
                Database_Connection db = new Database_Connection();
                string query = "SELECT c.className, CONCAT(t.teacherFirstname, \" \", t.teacherLastname) AS Teacher " +
                    "FROM class c " +
                    "JOIN teacher t ON c.teacher_id = t.teacher_id " +
                    "WHERE c.teacher_id = "+ teacher_id +" AND classArchive = 'NO' AND classRCode = '" + code1 + "'";

                using (MySqlDataReader reader = db.GetData(query))
                {
                    if (reader.Read())
                    {
                        string className = reader["className"].ToString();
                        string teacherName = reader["Teacher"].ToString();
                        string message = $"To join the \"{className}\" class by instructor {teacherName}, Enter this code: ";

                        Clipboard.SetText(message + code1);
                        MessageBox.Show("Code copied to clipboard.");
                    }
                    else
                    {
                        MessageBox.Show("No class found with the given criteria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no code to copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // Get the clicked button
            string classIDString = btn.Tag.ToString(); // Retrieve the class_id from the button's Tag property

            if (int.TryParse(classIDString, out int classID))
            {
                // Pass the data to the ClassView form
                ClassView classView = new ClassView(teacher_id, classID, desktopPanel);
                classView.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(classView);
                classView.Dock = DockStyle.Fill;
                classView.FormBorderStyle = FormBorderStyle.None;
                classView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblClass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            LinkLabel lbl = (LinkLabel)sender; // Get the clicked button
            string classIDString = lbl.Tag.ToString(); // Retrieve the class_id from the button's Tag property

            if (int.TryParse(classIDString, out int classID))
            {
                // Pass the data to the ClassView form
                TaskView taskView = new TaskView(teacher_id, classID, desktopPanel);
                taskView.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(taskView);
                taskView.Dock = DockStyle.Fill;
                taskView.FormBorderStyle = FormBorderStyle.None;
                taskView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid class ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
