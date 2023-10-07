
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Databox
{
    public partial class ReminderForm : Form
    {
        private string name;
        private string message;

        public ReminderForm(string name, string message)
        {
            this.name = name;
            this.message = message;

            InitializeComponent();
            InitializeFormAppearance();
            InitializeContent();
        }

        private void InitializeFormAppearance()
        {
            TopMost = true;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void InitializeContent()
        {
            lblTitle.Text = name;
            txtMessage.Text = message;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancel the form closing
                Hide(); // Hide the form instead of closing it
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
