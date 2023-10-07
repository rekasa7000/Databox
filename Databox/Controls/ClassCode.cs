using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Controls
{
    public partial class ClassCode : Form
    {
        public ClassCode(string Code)
        {
            InitializeComponent();
            txtCode.Text = Code;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCode.Text);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string clipboardText = Clipboard.GetText();
        }
    }
}
