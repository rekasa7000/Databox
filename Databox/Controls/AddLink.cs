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

namespace Databox.Controls
{
    public partial class AddLink : Form
    {
        public AddLink()
        {
            InitializeComponent();
        }
        public string LinkValue { get; private set; }

        private void iconExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddLink_Load(object sender, EventArgs e)
        {

        }
        public delegate void LinkAddedEventHandler(string link);
        public event LinkAddedEventHandler LinkAdded;


        private void button1_Click(object sender, EventArgs e)
        {
            string linkValue = txtLink.Text;
            if (LinkAdded != null)
            {
                LinkAdded(linkValue);
            }
            this.Close();
        }

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    }
}
