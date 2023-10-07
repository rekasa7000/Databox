
namespace Databox
{
    partial class EmailPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelFill = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtConfirm = new MaterialSkin.Controls.MaterialTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtNew = new MaterialSkin.Controls.MaterialTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelCode = new System.Windows.Forms.Panel();
            this.btnVerify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new MaterialSkin.Controls.MaterialTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.panelSpace1 = new System.Windows.Forms.Panel();
            this.txtEmail = new MaterialSkin.Controls.MaterialTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelName = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblRegister = new System.Windows.Forms.Label();
            this.panelFill.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelPassword.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelCode.SuspendLayout();
            this.panelEmail.SuspendLayout();
            this.panelName.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFill
            // 
            this.panelFill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panelFill.Controls.Add(this.panelContainer);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 50);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(800, 366);
            this.panelFill.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panelPassword);
            this.panelContainer.Controls.Add(this.panelCode);
            this.panelContainer.Controls.Add(this.panelEmail);
            this.panelContainer.Controls.Add(this.panelName);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelContainer.Size = new System.Drawing.Size(800, 366);
            this.panelContainer.TabIndex = 0;
            // 
            // panelPassword
            // 
            this.panelPassword.Controls.Add(this.btnSave);
            this.panelPassword.Controls.Add(this.panel3);
            this.panelPassword.Controls.Add(this.panel4);
            this.panelPassword.Controls.Add(this.label4);
            this.panelPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPassword.Location = new System.Drawing.Point(20, 350);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(760, 218);
            this.panelPassword.TabIndex = 2;
            this.panelPassword.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(563, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 40);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel3.Controls.Add(this.txtConfirm);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 91);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(760, 70);
            this.panel3.TabIndex = 47;
            // 
            // txtConfirm
            // 
            this.txtConfirm.AnimateReadOnly = false;
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirm.Depth = 0;
            this.txtConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfirm.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtConfirm.Hint = "Confirm New Password";
            this.txtConfirm.LeadingIcon = null;
            this.txtConfirm.Location = new System.Drawing.Point(10, 10);
            this.txtConfirm.MaxLength = 50;
            this.txtConfirm.MouseState = MaterialSkin.MouseState.OUT;
            this.txtConfirm.Multiline = false;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Password = true;
            this.txtConfirm.Size = new System.Drawing.Size(740, 50);
            this.txtConfirm.TabIndex = 0;
            this.txtConfirm.Text = "";
            this.txtConfirm.TrailingIcon = null;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel4.Controls.Add(this.txtNew);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 21);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(760, 70);
            this.panel4.TabIndex = 46;
            // 
            // txtNew
            // 
            this.txtNew.AnimateReadOnly = false;
            this.txtNew.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNew.Depth = 0;
            this.txtNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNew.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNew.Hint = "New Password";
            this.txtNew.LeadingIcon = null;
            this.txtNew.Location = new System.Drawing.Point(10, 10);
            this.txtNew.MaxLength = 50;
            this.txtNew.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNew.Multiline = false;
            this.txtNew.Name = "txtNew";
            this.txtNew.Password = true;
            this.txtNew.Size = new System.Drawing.Size(740, 50);
            this.txtNew.TabIndex = 0;
            this.txtNew.Text = "";
            this.txtNew.TrailingIcon = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 21);
            this.label4.TabIndex = 50;
            this.label4.Text = "Create new password.";
            // 
            // panelCode
            // 
            this.panelCode.Controls.Add(this.btnVerify);
            this.panelCode.Controls.Add(this.label1);
            this.panelCode.Controls.Add(this.txtCode);
            this.panelCode.Controls.Add(this.label3);
            this.panelCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCode.Location = new System.Drawing.Point(20, 210);
            this.panelCode.Name = "panelCode";
            this.panelCode.Padding = new System.Windows.Forms.Padding(10);
            this.panelCode.Size = new System.Drawing.Size(760, 140);
            this.panelCode.TabIndex = 1;
            this.panelCode.Visible = false;
            // 
            // btnVerify
            // 
            this.btnVerify.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerify.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerify.ForeColor = System.Drawing.Color.White;
            this.btnVerify.Location = new System.Drawing.Point(631, 98);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(119, 32);
            this.btnVerify.TabIndex = 5;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(602, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "If you don\'t see the email, check other places it might be, like your junk, spam," +
    " social, or other folders.";
            // 
            // txtCode
            // 
            this.txtCode.AnimateReadOnly = false;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Depth = 0;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCode.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCode.Hint = "Enter Code here";
            this.txtCode.LeadingIcon = null;
            this.txtCode.Location = new System.Drawing.Point(10, 31);
            this.txtCode.MaxLength = 50;
            this.txtCode.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCode.Multiline = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(740, 50);
            this.txtCode.TabIndex = 4;
            this.txtCode.Text = "";
            this.txtCode.TrailingIcon = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Enter Code from Email";
            // 
            // panelEmail
            // 
            this.panelEmail.Controls.Add(this.btnNext);
            this.panelEmail.Controls.Add(this.panelSpace1);
            this.panelEmail.Controls.Add(this.txtEmail);
            this.panelEmail.Controls.Add(this.label2);
            this.panelEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEmail.Location = new System.Drawing.Point(20, 70);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Padding = new System.Windows.Forms.Padding(10);
            this.panelEmail.Size = new System.Drawing.Size(760, 140);
            this.panelEmail.TabIndex = 3;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(631, 98);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(119, 32);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panelSpace1
            // 
            this.panelSpace1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSpace1.Location = new System.Drawing.Point(10, 81);
            this.panelSpace1.Name = "panelSpace1";
            this.panelSpace1.Size = new System.Drawing.Size(740, 17);
            this.panelSpace1.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.AnimateReadOnly = false;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Depth = 0;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmail.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEmail.Hint = "Enter your email here";
            this.txtEmail.LeadingIcon = null;
            this.txtEmail.Location = new System.Drawing.Point(10, 31);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEmail.Multiline = false;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(740, 50);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Text = "";
            this.txtEmail.TrailingIcon = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Send an email to your account.";
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.lblName);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelName.Location = new System.Drawing.Point(20, 20);
            this.panelName.Name = "panelName";
            this.panelName.Padding = new System.Windows.Forms.Padding(10);
            this.panelName.Size = new System.Drawing.Size(760, 50);
            this.panelName.TabIndex = 0;
            this.panelName.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(10, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(19, 30);
            this.lblName.TabIndex = 3;
            this.lblName.Text = " ";
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelTitle.Controls.Add(this.lblRegister);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(10);
            this.panelTitle.Size = new System.Drawing.Size(800, 50);
            this.panelTitle.TabIndex = 40;
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRegister.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(205)))));
            this.lblRegister.Location = new System.Drawing.Point(10, 10);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(254, 29);
            this.lblRegister.TabIndex = 32;
            this.lblRegister.Text = "Password Recovery";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EmailPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelTitle);
            this.Name = "EmailPassword";
            this.Text = "Databox";
            this.panelFill.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelCode.ResumeLayout(false);
            this.panelCode.PerformLayout();
            this.panelEmail.ResumeLayout(false);
            this.panelEmail.PerformLayout();
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelCode;
        private System.Windows.Forms.Button btnVerify;
        private MaterialSkin.Controls.MaterialTextBox txtCode;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private MaterialSkin.Controls.MaterialTextBox txtConfirm;
        private System.Windows.Forms.Panel panel4;
        private MaterialSkin.Controls.MaterialTextBox txtNew;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panelEmail;
        private System.Windows.Forms.Button btnNext;
        private MaterialSkin.Controls.MaterialTextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelSpace1;
    }
}