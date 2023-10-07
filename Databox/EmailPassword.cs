using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    public partial class EmailPassword : Form
    {
        private Database_Connection dbConnection;
        private string generatedCode;

        public EmailPassword()
        {
            InitializeComponent();
            dbConnection = new Database_Connection();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            generatedCode = GenerateVerificationCode();

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool emailExists = CheckEmailExists(email);

            if (emailExists)
            {
                SendVerificationCode(email, generatedCode);
                MessageBox.Show("Verification code sent to your email. Please check your inbox and enter the code.", "Verification Code Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelEmail.Visible = false;
                panelName.Visible = true;
                panelCode.Visible = true;
                getName(email);
            }
            else
            {
                MessageBox.Show("The email does not have an account.", "Email Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
        void getName(string email)
        {
            string querySelectaccountType = $"SELECT user_id, accountType FROM user WHERE email = '{email}'";
            string accountType = "";
            int user_id = 0;

            using (MySqlDataReader reader = dbConnection.GetData(querySelectaccountType))
            {
                if (reader.Read())
                {
                    user_id = reader.GetInt32(0);
                    accountType = reader.GetString(1);
                }
                else
                {
                    lblName.Text = "User not found";
                    return; // Return early since the user is not found
                }
            }

            dbConnection.CloseConnection();

            string query = "";
            if (accountType == "Teacher")
            {
                query = $"SELECT CONCAT(teacherFirstname, ' ', teacherLastname) AS name FROM teacher WHERE user_id = {user_id}";
            }
            else
            {
                query = $"SELECT CONCAT(studFirstname, ' ', studLastname) AS name FROM student WHERE user_id = {user_id}";
            }

            using (MySqlDataReader reader1 = dbConnection.GetData(query))
            {
                if (reader1.Read())
                {
                    string name = reader1["name"].ToString();
                    lblName.Text = $"[{accountType}] - {name}";
                }
                else
                {
                    lblName.Text = "Name not found";
                }
            }
        }


        private bool CheckEmailExists(string email)
        {
            string query = $"SELECT COUNT(*) FROM user WHERE email = '{email}'";
            object result = dbConnection.ExecuteScalar(query);

            int count;
            if (result != null && int.TryParse(result.ToString(), out count))
            {
                return count > 0;
            }

            return false;
        }

        private void SendVerificationCode(string email, string code)
        {
            Authenticate authenticator = new Authenticate();
            authenticator.Email(email, code);
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            string enteredCode = txtCode.Text.Trim();
            if (enteredCode == generatedCode)
            {
                MessageBox.Show("Verification successful.", "Code Verified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelCode.Visible = false;
                panelPassword.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.", "Code Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string newPassword = txtNew.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            // Get the current password from the database
            string querySelectPassword = $"SELECT password FROM user WHERE email = '{email}'";
            string currentPassword = dbConnection.ExecuteScalar(querySelectPassword)?.ToString();

            if (newPassword == confirmPassword)
            {
                if (currentPassword == newPassword)
                {
                    MessageBox.Show("New password cannot be the same as the previous password.", "Password Not Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update the password in the database
                string queryUpdatePassword = $"UPDATE user SET password = '{newPassword}' WHERE email = '{email}'";
                int rowsAffected = dbConnection.ExecuteNonQuery(queryUpdatePassword);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully. You will be back to login.", "Password Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    SessionManager.ClearSession();
                    LogReg login = new LogReg();
                    login.Show();
                }
                else
                {
                    MessageBox.Show("Failed to update the password.", "Password Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Password and confirmation do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
