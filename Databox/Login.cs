using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    public partial class LogReg : Form
    {
        Database_Connection dbConn = new Database_Connection();
        Form activeForm;

        public LogReg()
        {
            InitializeComponent();
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void Login_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int arcSize = 25;
            int x = 0;
            int y = 0;
            int width = this.Width;
            int height = this.Height;
            path.AddArc(x, y, arcSize, arcSize, 180, 90);
            path.AddArc(width - arcSize, y, arcSize, arcSize, 270, 90);
            path.AddArc(width - arcSize, height - arcSize, arcSize, arcSize, 0, 90);
            path.AddArc(x, height - arcSize, arcSize, arcSize, 90, 90);
            path.CloseAllFigures();

            this.Region = new Region(path);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Text;
            string accountType = "";
            int userId = 0;

            string query = $"SELECT user_id, password, accountType FROM user WHERE email='{email}' AND password = '{password}'";
            Database_Connection db = new Database_Connection();
            MySqlDataReader dataReader = db.GetData(query);

            if (dataReader.Read())
            {
                string dbPassword = dataReader.GetString("password");
                accountType = dataReader.GetString("accountType");
                userId = dataReader.GetInt32("user_id");
                dataReader.Close();

                if (password == dbPassword)
                {
                    if (accountType == "Teacher")
                    {
                        // check if the user has a record in the teacher table
                        string teacherQuery = $"SELECT teacher_id FROM teacher WHERE user_id={userId}";
                        MySqlDataReader teacherReader = db.GetData(teacherQuery);

                        int teacherId = 0;
                        if (teacherReader.Read())
                        {
                            teacherId = teacherReader.GetInt32(0);
                            MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK);

                            // Set session data
                            SessionData sessionData = new SessionData();
                            sessionData.UserId = userId;
                            sessionData.AccountType = accountType;
                            sessionData.TeacherId = teacherId;
                            sessionData.ExpirationTime = DateTime.Now.AddDays(30);

                            // Save session data
                            SessionManager.SaveSession(sessionData);

                            TeacherDashboard menu = new TeacherDashboard(sessionData.TeacherId);
                            activeForm = menu;

                            AppointmentChecker appointment = new AppointmentChecker(activeForm);
                            ScheduleChecker schedule = new ScheduleChecker(activeForm);
                            Task.Run(() =>
                            {
                                appointment.StartCheckingSchedules();
                            });
                            Task.Run(() =>
                            {
                                schedule.StartCheckingSchedules();
                            });
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Please complete your teacher registration", "Informations Required", MessageBoxButtons.OK);
                            Teacher_Info form = new Teacher_Info(userId);
                            form.Show();
                            this.Hide();
                        }

                        teacherReader.Close();

                    }
                    else if (accountType == "Student")
                    {
                        // check if the user has a record in the student table
                        string studentQuery = $"SELECT student_id FROM student WHERE user_id={userId}";
                        MySqlDataReader studentReader = db.GetData(studentQuery);
                        if (studentReader.Read())
                        {
                            int studentId = studentReader.GetInt32(0);
                            MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK);

                            // Set session data
                            SessionData sessionData = new SessionData();
                            sessionData.UserId = userId;
                            sessionData.AccountType = accountType;
                            sessionData.StudentId = studentId;
                            sessionData.ExpirationTime = DateTime.Now.AddDays(30);

                            // Save session data
                            SessionManager.SaveSession(sessionData);

                            StudentDashboard menu = new StudentDashboard(studentId);
                            activeForm = menu;
                            StudentAppointmentScheduler appointment = new StudentAppointmentScheduler(activeForm);
                            StudentClassScheduler schedule = new StudentClassScheduler(activeForm);
                            Task.Run(() =>
                            {
                                appointment.StartCheckingSchedules();
                            });
                            Task.Run(() =>
                            {
                                schedule.StartCheckingSchedules();
                            });
                            menu.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Please complete your student registration", "Informations Required", MessageBoxButtons.OK);
                            Student_Information form = new Student_Information(userId);
                            form.Show();
                            this.Hide();
                        }
                        studentReader.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Incorrect Password", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid email and password combination", "Error", MessageBoxButtons.OK);
            }

            dataReader.Close();
        }

        private void LogReg_Load(object sender, EventArgs e)
        {            
        }
        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register info = new Register();
            info.Show();
        }

        private void lblForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmailPassword forgot = new EmailPassword();
            forgot.Show();
            this.Hide();
        }
    }
}
