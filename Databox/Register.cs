using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Databox.Controls;
using MaterialSkin;
using MySql.Data.MySqlClient;

namespace Databox
{
    
    public partial class Register : Form
    {
        public int userId;
        public string email, accountType, password, password1, code;
        public Register()
        {
            InitializeComponent();
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

              

        private void panelUp_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public void Texts()
        {
            email = txtEmail.Text;
            password = txtPassword.Text;
            password1 = txtPassword1.Text;
        }
        public void Code()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            code = new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool Insert(string email, string password, string accountType)
        {
            Database_Connection dbConn = new Database_Connection();
            string query = $"SELECT COUNT(*) FROM user WHERE email = '{email}'";
            object result = dbConn.ExecuteScalar(query);
            int count = Convert.ToInt32(result);
            if (count > 0)
            {
                MessageBox.Show("You already have an account. Please log in.", "Error", MessageBoxButtons.OK);
                return false;
            }
            query = $"INSERT INTO user(email, password, accountType) VALUES('{email}', '{password}', '{accountType}')";
            dbConn.ExecuteQuery(query);
            query = "SELECT LAST_INSERT_ID()";
            MySqlDataReader dataReader = dbConn.GetData(query);
            userId = 0;
            if (dataReader.Read())
            {
                userId = dataReader.GetInt32(0);
            }
            dataReader.Close();
            dbConn.CloseConnection();
            return true;
        }
        public void accountReg(string email, string password, string password1, string accountType)
        {
            Texts();
            if (!string.IsNullOrEmpty(accountType) && !string.IsNullOrEmpty(email) && email.Contains('@') && password == password1)
            {
                if (Insert(email, password, accountType))
                {
                    if (accountType == "Teacher")
                    {
                        this.Hide();
                        Teacher_Info teacher = new Teacher_Info(userId);
                        teacher.Show();
                    }
                    else if (accountType == "Student")
                    {
                        this.Hide();
                        Student_Information student = new Student_Information(userId);
                        student.Show();
                    }
                }
                else
                {
                    this.Hide();
                    LogReg login = new LogReg();
                    login.Show();
                }
            } else if (string.IsNullOrEmpty(accountType) || string.IsNullOrEmpty(email) || !email.Contains('@') || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password1))
                {
                    if (string.IsNullOrEmpty(accountType))
                    {
                        MessageBox.Show("Select an account type", "Account type missing", MessageBoxButtons.OK);
                    }
                    else if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Please enter an email", "Invalid email", MessageBoxButtons.OK);
                    }
                    else if (!email.Contains('@'))
                    {
                        MessageBox.Show("Please input a valid email", "Invalid email", MessageBoxButtons.OK);
                    }
                    else if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password1))
                    {
                        MessageBox.Show("Please input a valid email", "Invalid email", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Please try again", "Error", MessageBoxButtons.OK);
                }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Texts();
            string query = $"SELECT COUNT(*) FROM user WHERE email = '{email}'";
            Database_Connection dbConn = new Database_Connection();
            object result = dbConn.ExecuteScalar(query);
            int count = Convert.ToInt32(result);
            dbConn.CloseConnection();

            if (count > 0)
            {
                MessageBox.Show("Email already exists. Please log in.", "Error", MessageBoxButtons.OK);
                return;
            }

            Code();
            Authenticate auth = new Authenticate();
            auth.Email(email, code);
            Authcode acode = new Authcode(code, email, password, password1, accountType);
            acode.Show();
            this.Hide();
        }



        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();            
            LogReg login = new LogReg();
            login.Show();
        }

        private void iconMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTeacher_Click(object sender, EventArgs e)
        {
            btnTeacher.BackColor = Color.FromArgb(65, 90, 90);
            btnStudent.BackColor = Color.FromArgb(65, 65, 65);
            accountType = "Teacher";
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {            
            btnStudent.BackColor = Color.FromArgb(65, 90, 90);
            btnTeacher.BackColor = Color.FromArgb(65, 65, 65);
            accountType = "Student";
        }
    }
}
