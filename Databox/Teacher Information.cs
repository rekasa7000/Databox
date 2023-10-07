using MaterialSkin;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    public partial class Teacher_Info : Form
    {
        string firstname, middlename, lastname, contactNo, expertise;
        int passedId;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Teacher_Info(int passedID)
        {
            InitializeComponent();
            passedId = passedID;
            var skin = MaterialSkinManager.Instance;
            skin.Theme = MaterialSkinManager.Themes.DARK;
            skin.ColorScheme = new ColorScheme(
                Color.FromArgb(30, 31, 34),
                Color.FromArgb(43, 45, 49),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(92, 132, 217),
                TextShade.WHITE
                );
        }
        public void Fill()
        {
            firstname = txtFirstname.Text;
            middlename = txtMiddlename.Text;
            lastname = txtLastname.Text;
            contactNo = txtContact.Text;
            expertise = cmbCollege.SelectedItem.ToString();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnHome_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogReg login = new LogReg();
            login.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelAdditional_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFirstname_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTeachInfo_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMiddlename_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Teacher_Info_Load(object sender, EventArgs e)
        {
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Fill();
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname) && !string.IsNullOrEmpty(contactNo) && !string.IsNullOrEmpty(expertise))
            {
                MessageBox.Show("Welcome! Teacher " +firstname + " " + lastname +"!", "Account Registration Successful", MessageBoxButtons.OK);
                this.Hide();
                Database_Connection db = new Database_Connection();

                // Construct query to insert user details into teacherent table
                string query = $"INSERT INTO teacher (teacherFirstname, teacherMiddlename, teacherLastname, teacherContact, teacherCollege, user_id) " +
                                $"VALUES ('{firstname}', '{middlename}', '{lastname}', '{contactNo}', '{expertise}', '{passedId}');";

                // Execute query
                db.ExecuteQuery(query);
                string query1 = "SELECT LAST_INSERT_ID()";

                // Execute query and retrieve the teacher_id value
                object result = db.ExecuteScalar(query1);
                int teacherId = Convert.ToInt32(result);
                TeacherDashboard teacher = new TeacherDashboard(teacherId);
                teacher.Show();
            }
            else
                MessageBox.Show("Please enter information on the required fields", "Error", MessageBoxButtons.OK);
        }
    }
}
