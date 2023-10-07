
namespace Databox
{
    partial class PersonalInformation
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblRegister = new System.Windows.Forms.Label();
            this.panelFill = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtCollege = new MaterialSkin.Controls.MaterialTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtContact = new MaterialSkin.Controls.MaterialTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtLastname = new MaterialSkin.Controls.MaterialTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMiddlename = new MaterialSkin.Controls.MaterialTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFirstName = new MaterialSkin.Controls.MaterialTextBox();
            this.panelTitle.SuspendLayout();
            this.panelFill.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelTitle.Controls.Add(this.lblRegister);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(10);
            this.panelTitle.Size = new System.Drawing.Size(990, 50);
            this.panelTitle.TabIndex = 37;
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRegister.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(205)))));
            this.lblRegister.Location = new System.Drawing.Point(10, 10);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(265, 29);
            this.lblRegister.TabIndex = 32;
            this.lblRegister.Text = "Personal Information";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelFill
            // 
            this.panelFill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panelFill.Controls.Add(this.btnCancel);
            this.panelFill.Controls.Add(this.btnSave);
            this.panelFill.Controls.Add(this.panel6);
            this.panelFill.Controls.Add(this.panel4);
            this.panelFill.Controls.Add(this.panel3);
            this.panelFill.Controls.Add(this.panel2);
            this.panelFill.Controls.Add(this.panel1);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 50);
            this.panelFill.Name = "panelFill";
            this.panelFill.Padding = new System.Windows.Forms.Padding(50, 20, 50, 10);
            this.panelFill.Size = new System.Drawing.Size(990, 470);
            this.panelFill.TabIndex = 38;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(560, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(187, 40);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(753, 389);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 40);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel6.Controls.Add(this.txtCollege);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(50, 300);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10);
            this.panel6.Size = new System.Drawing.Size(890, 70);
            this.panel6.TabIndex = 43;
            // 
            // txtCollege
            // 
            this.txtCollege.AnimateReadOnly = false;
            this.txtCollege.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCollege.Depth = 0;
            this.txtCollege.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCollege.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCollege.Hint = "College/ Department";
            this.txtCollege.LeadingIcon = null;
            this.txtCollege.Location = new System.Drawing.Point(10, 10);
            this.txtCollege.MaxLength = 50;
            this.txtCollege.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCollege.Multiline = false;
            this.txtCollege.Name = "txtCollege";
            this.txtCollege.Size = new System.Drawing.Size(870, 50);
            this.txtCollege.TabIndex = 0;
            this.txtCollege.Text = "";
            this.txtCollege.TrailingIcon = null;
            this.txtCollege.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel4.Controls.Add(this.txtContact);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(50, 230);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(890, 70);
            this.panel4.TabIndex = 41;
            // 
            // txtContact
            // 
            this.txtContact.AnimateReadOnly = false;
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContact.Depth = 0;
            this.txtContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContact.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtContact.Hint = "Contact Number";
            this.txtContact.LeadingIcon = null;
            this.txtContact.Location = new System.Drawing.Point(10, 10);
            this.txtContact.MaxLength = 50;
            this.txtContact.MouseState = MaterialSkin.MouseState.OUT;
            this.txtContact.Multiline = false;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(870, 50);
            this.txtContact.TabIndex = 0;
            this.txtContact.Text = "";
            this.txtContact.TrailingIcon = null;
            this.txtContact.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel3.Controls.Add(this.txtLastname);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(50, 160);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(890, 70);
            this.panel3.TabIndex = 40;
            // 
            // txtLastname
            // 
            this.txtLastname.AnimateReadOnly = false;
            this.txtLastname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLastname.Depth = 0;
            this.txtLastname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLastname.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLastname.Hint = "Last Name";
            this.txtLastname.LeadingIcon = null;
            this.txtLastname.Location = new System.Drawing.Point(10, 10);
            this.txtLastname.MaxLength = 50;
            this.txtLastname.MouseState = MaterialSkin.MouseState.OUT;
            this.txtLastname.Multiline = false;
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(870, 50);
            this.txtLastname.TabIndex = 0;
            this.txtLastname.Text = "";
            this.txtLastname.TrailingIcon = null;
            this.txtLastname.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel2.Controls.Add(this.txtMiddlename);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(50, 90);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(890, 70);
            this.panel2.TabIndex = 39;
            // 
            // txtMiddlename
            // 
            this.txtMiddlename.AnimateReadOnly = false;
            this.txtMiddlename.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMiddlename.Depth = 0;
            this.txtMiddlename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMiddlename.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMiddlename.Hint = "Middle Name";
            this.txtMiddlename.LeadingIcon = null;
            this.txtMiddlename.Location = new System.Drawing.Point(10, 10);
            this.txtMiddlename.MaxLength = 50;
            this.txtMiddlename.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMiddlename.Multiline = false;
            this.txtMiddlename.Name = "txtMiddlename";
            this.txtMiddlename.Size = new System.Drawing.Size(870, 50);
            this.txtMiddlename.TabIndex = 0;
            this.txtMiddlename.Text = "";
            this.txtMiddlename.TrailingIcon = null;
            this.txtMiddlename.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.panel1.Controls.Add(this.txtFirstName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(50, 20);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(890, 70);
            this.panel1.TabIndex = 38;
            // 
            // txtFirstName
            // 
            this.txtFirstName.AnimateReadOnly = false;
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFirstName.Depth = 0;
            this.txtFirstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFirstName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirstName.Hint = "First Name";
            this.txtFirstName.LeadingIcon = null;
            this.txtFirstName.Location = new System.Drawing.Point(10, 10);
            this.txtFirstName.MaxLength = 50;
            this.txtFirstName.MouseState = MaterialSkin.MouseState.OUT;
            this.txtFirstName.Multiline = false;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(870, 50);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.Text = "";
            this.txtFirstName.TrailingIcon = null;
            this.txtFirstName.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // PersonalInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(990, 520);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelTitle);
            this.Name = "PersonalInformation";
            this.Text = "PersonalInformation";
            this.Load += new System.EventHandler(this.PersonalInformation_Load);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.panelFill.ResumeLayout(false);
            this.panelFill.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.Panel panel3;
        private MaterialSkin.Controls.MaterialTextBox txtLastname;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialTextBox txtMiddlename;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialTextBox txtFirstName;
        private System.Windows.Forms.Panel panel6;
        private MaterialSkin.Controls.MaterialTextBox txtCollege;
        private System.Windows.Forms.Panel panel4;
        private MaterialSkin.Controls.MaterialTextBox txtContact;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}