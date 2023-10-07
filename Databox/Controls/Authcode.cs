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
    public partial class Authcode : Form
    {
        string thisCode, thisEmail,  tConfirm;
        string thisAccount, thisPass, thisPassword1;


        private void btnConfirm_Click(object sender, EventArgs e)
        {            
            tConfirm = txtCode.Text;
            if (tConfirm == thisCode)
            {
                Register reg = new Register();
                reg.accountReg(thisEmail, thisPass, thisPassword1, thisAccount);
            } else
                MessageBox.Show("Code Invalid, Please try again.", "Error Code", MessageBoxButtons.OK);
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Show();
        }

        private void btnResend_Click(object sender, EventArgs e)
        {
            Authenticate auth = new Authenticate();
            auth.Email(thisEmail, thisCode);
            MessageBox.Show("Check your email", "Resend Code", MessageBoxButtons.OK);
        }

        public Authcode(string code, string email, string password, string password1, string accountType)
        {
            InitializeComponent();
            thisCode = code;
            thisEmail = email;
            thisPass = password;
            thisPassword1 = password1;
            thisAccount = accountType;
        }
    }
}
