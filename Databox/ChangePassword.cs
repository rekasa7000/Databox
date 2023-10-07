using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    public partial class ChangePassword : Form
    {
        private Database_Connection dbConnection;
        SessionData sessionData = SessionManager.LoadSession();
        public ChangePassword()
        {
            InitializeComponent();
            dbConnection = new Database_Connection();
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
        void clear()
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            txtConfirm.Text = "";
            txtNew.Text = "";
            txtPassword.Text = "";
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int userId = sessionData.UserId;
            string previousPassword = txtPassword.Text.Trim();
            string newPassword = txtNew.Text.Trim();
            string confirmNewPassword = txtConfirm.Text.Trim();

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("The new password and confirm password do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (previousPassword == newPassword)
            {
                MessageBox.Show("Your new password is the same as your previous password.", "Password Same as Previous", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"SELECT password FROM user WHERE user_id = '{userId}'";
            DataTable resultTable = dbConnection.ExecuteSelectQuery(query);
            if (resultTable.Rows.Count > 0)
            {
                string currentPassword = resultTable.Rows[0]["password"].ToString();
                if (previousPassword != currentPassword)
                {
                    MessageBox.Show("Invalid previous password. Please enter the correct previous password.", "Invalid Previous Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Failed to retrieve user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            query = $"UPDATE user SET password = '{newPassword}' WHERE user_id = '{userId}'";
            int rowsAffected = dbConnection.ExecuteNonQuery(query);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Password changed successfully.", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
            {
                MessageBox.Show("Failed to change password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmailPassword forgot = new EmailPassword();
            forgot.Show();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            clear();
        }
    }
}
