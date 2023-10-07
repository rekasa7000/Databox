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
using FontAwesome.Sharp;
using Databox.Student_Additional_Forms.More_Forms;

namespace Databox
{
    public partial class StudentDashboard : Form
    {
        public static Form ActiveForm { get; private set; }
        private IconButton currentBtn;
        private Panel leftBorderbtn;
        private Form currentChildForm;
        int studentID;
        private static NotifyIcon notifyIcon;

        public StudentDashboard(int student_id)
        {
            studentID = student_id;
            InitializeComponent();
            leftBorderbtn = new Panel();
            leftBorderbtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderbtn);

            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            hideSubMenu();
        }
        private void hideSubMenu()
        {
            subMenuAccount.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(51, 51, 51);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                crntchldFormIcon.IconChar = currentBtn.IconChar;
                crntchldFormIcon.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(30, 31, 34);
                currentBtn.ForeColor = Color.FromArgb(154, 192, 205);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(154, 192, 205);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private struct Colors
        {
            public static Color PastelPink = Color.FromArgb(255, 209, 220);
            public static Color PastelYellow = Color.FromArgb(255, 251, 178);
            public static Color PastelGreen = Color.FromArgb(204, 255, 204);
            public static Color PastelBlue = Color.FromArgb(179, 204, 255);
            public static Color PastelPurple = Color.FromArgb(221, 204, 255);
            public static Color PastelOrange = Color.FromArgb(255, 204, 153);
            public static Color PastelGray = Color.FromArgb(230, 230, 250);
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            desktopPanel.Controls.Add(childForm);
            desktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnClass_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.PastelGray);
            lblHome.Text = btnClass.Text;
            OpenChildForm(new StudentClasses(studentID, desktopPanel));
            hideSubMenu();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.PastelGray);
            lblHome.Text = btnSchedule.Text;
            OpenChildForm(new ClassSchedules(studentID, desktopPanel));
            hideSubMenu();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.PastelOrange);
            lblHome.Text = btnReport.Text;
            OpenChildForm(new Reports(studentID, desktopPanel));
            hideSubMenu();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.PastelPink);
            lblHome.Text = btnAccount.Text;
            showSubMenu(subMenuAccount);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderbtn.Visible = false;
            crntchldFormIcon.IconChar = IconChar.Home;
            crntchldFormIcon.IconColor = Color.FromArgb(179, 204, 255);
            lblHome.Text = "Home";
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ActiveForm = this;
            this.Hide();

            // Display the form in the system tray
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.Untitled.GetHicon());
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Show", null, ShowMenuItem_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitMenuItem_Click);
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            OpenChildForm(new StudentClasses(studentID, desktopPanel));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.ClearSession();
            this.Hide();
            LogReg log = new LogReg();
            log.Show();
        }

        private void btnViewInformation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PersonalInformation());
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChangePassword());
        }

        private static void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            // Show the main form when the system tray icon is double-clicked
            ActiveForm.Show();
            ActiveForm.WindowState = FormWindowState.Normal;
        }

        private static void ShowMenuItem_Click(object sender, EventArgs e)
        {
            // Show the main form when "Show" menu item is clicked
            ActiveForm.Show();
            ActiveForm.WindowState = FormWindowState.Normal;

            // Remove the system tray icon
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        private static void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Clean up resources and exit the application when "Exit" menu item is clicked
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            ActiveForm.Close();
            Application.Exit();
        }
    }
}

