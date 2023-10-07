using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using SqlServerTypes; // Make sure to add the appropriate reference

namespace Databox
{
    static class Program
    {
        private static Form activeForm;
        [STAThread]
        static void Main()
        {
            try
            {
                if (!HasInternetConnection())
                {
                    MessageBox.Show("No internet connection available. Please check your network connection and try again.", "No Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (IsFirstInstance())
                {
                    SetStartup();
                }

                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SessionData sessionData = SessionManager.LoadSession();
                if (sessionData != null && IsSessionValid(sessionData))
                {
                    if (sessionData != null && IsSessionValid(sessionData))
                    {
                        if (sessionData.AccountType == "Teacher")
                        {
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
                            Application.Run(menu);
                        }
                        else if (sessionData.AccountType == "Student"){
                            StudentDashboard menu = new StudentDashboard(sessionData.StudentId);
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
                            Application.Run(menu);
                        }
                    }
                }
                else
                {
                    LogReg loginForm = new LogReg();
                    loginForm.FormClosed += LoginForm_FormClosed;
                    Application.Run(loginForm);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, display an error message, or log the error
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsSessionValid(SessionData sessionData)
        {
            DateTime now = DateTime.Now;
            return sessionData.ExpirationTime > now;
        }

        private static void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SessionManager.ClearSession();
            Application.Exit();
        }
        private static bool IsFirstInstance()
        {
            string appName = "Databox";
            bool isFirstInstance;

            using (Mutex mutex = new Mutex(true, appName, out isFirstInstance))
            {
                return isFirstInstance;
            }
        }

        private static void SetStartup()
        {
            string appName = "Databox";
            string executablePath = Application.ExecutablePath;
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = System.IO.Path.Combine(startupFolderPath, appName + ".lnk");

            if (!System.IO.File.Exists(shortcutPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = executablePath;
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.Save();
            }
        }
        private static bool HasInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
