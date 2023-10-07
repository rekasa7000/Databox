using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class AddClass : Form
    {
        int student_id;
        Panel desktopPanel;

        public AddClass(int studentID, Panel pan)
        {
            student_id = studentID;
            desktopPanel = pan;
            InitializeComponent();
        }

        private void AddClass_Load(object sender, EventArgs e)
        {

        }

        public void EnrollStudentToClass(int studentId, string classRCode)
        {
            try
            {
                Database_Connection db = new Database_Connection();

                // Retrieve the class_id based on the provided classRCode
                string query = $"SELECT class_id FROM class WHERE classRCode = '{classRCode}' AND classArchive = 'NO'";

                object classId = db.ExecuteScalar(query);
                if (classId != null)
                {
                    // Check if the student is already enrolled in the class
                    query = $"SELECT COUNT(*) FROM student_class WHERE student_id = {studentId} AND class_id = {classId}";

                    object enrollmentCount = db.ExecuteScalar(query);
                    if (enrollmentCount != null && Convert.ToInt32(enrollmentCount) == 0)
                    {
                        // Enroll the student in the class
                        query = $"INSERT INTO student_class (student_id, class_id) VALUES ({studentId}, {classId})";
                        db.ExecuteQuery(query);

                        MessageBox.Show("Class Added Successfully");
                        backToClasses();
                    }
                    else
                    {
                        MessageBox.Show("Class already added.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid code. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text;

                if (code.Length != 6)
                {
                    MessageBox.Show("Invalid code. Code should be exactly 6 characters long.");
                }
                else
                {
                    EnrollStudentToClass(student_id, code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void backToClasses()
        {
            try
            {
                StudentClasses add = new StudentClasses(student_id, desktopPanel);
                add.TopLevel = false;
                desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
                desktopPanel.Controls.Add(add);
                add.Dock = DockStyle.Fill;
                add.FormBorderStyle = FormBorderStyle.None;
                add.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            backToClasses();
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int selectionStart = textBox.SelectionStart;
            textBox.Text = textBox.Text.ToUpperInvariant();
            textBox.SelectionStart = selectionStart;
        }
    }
}
