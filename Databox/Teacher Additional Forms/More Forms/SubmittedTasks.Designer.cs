
namespace Databox.Teacher_Additional_Forms.More_Forms
{
    partial class SubmittedTasks
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
            this.btnGo = new FontAwesome.Sharp.IconButton();
            this.lblRegister = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSubmissions = new System.Windows.Forms.Panel();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.panTitle = new System.Windows.Forms.Panel();
            this.panelGraded = new System.Windows.Forms.Panel();
            this.lblGradedcount = new System.Windows.Forms.Label();
            this.lblGraded = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelTurnedin = new System.Windows.Forms.Panel();
            this.lblTurnedincount = new System.Windows.Forms.Label();
            this.lblTurnedin = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelAssigned = new System.Windows.Forms.Panel();
            this.lblAssignedcount = new System.Windows.Forms.Label();
            this.lblAssigned = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panTop = new System.Windows.Forms.Panel();
            this.lblTasktitle = new System.Windows.Forms.Label();
            this.panelScores = new System.Windows.Forms.Panel();
            this.groupAssigned = new System.Windows.Forms.GroupBox();
            this.groupLate = new System.Windows.Forms.GroupBox();
            this.groupWell = new System.Windows.Forms.GroupBox();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelReturn = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelSubmissions.SuspendLayout();
            this.panTitle.SuspendLayout();
            this.panelGraded.SuspendLayout();
            this.panelTurnedin.SuspendLayout();
            this.panelAssigned.SuspendLayout();
            this.panTop.SuspendLayout();
            this.panelScores.SuspendLayout();
            this.panelReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelTitle.Controls.Add(this.btnGo);
            this.panelTitle.Controls.Add(this.lblRegister);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1039, 50);
            this.panelTitle.TabIndex = 60;
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.btnGo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGo.FlatAppearance.BorderSize = 0;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnGo.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.btnGo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnGo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGo.IconSize = 30;
            this.btnGo.Location = new System.Drawing.Point(0, 0);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(139, 50);
            this.btnGo.TabIndex = 34;
            this.btnGo.Text = "Back to Tasks";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblRegister.Location = new System.Drawing.Point(145, 9);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(214, 29);
            this.lblRegister.TabIndex = 32;
            this.lblRegister.Text = "Submitted Tasks";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.panelSubmissions);
            this.panelMain.Controls.Add(this.panelScores);
            this.panelMain.Controls.Add(this.panelReturn);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1039, 561);
            this.panelMain.TabIndex = 62;
            // 
            // panelSubmissions
            // 
            this.panelSubmissions.Controls.Add(this.flow);
            this.panelSubmissions.Controls.Add(this.panTitle);
            this.panelSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubmissions.Location = new System.Drawing.Point(375, 50);
            this.panelSubmissions.Name = "panelSubmissions";
            this.panelSubmissions.Size = new System.Drawing.Size(664, 511);
            this.panelSubmissions.TabIndex = 63;
            // 
            // flow
            // 
            this.flow.AutoScroll = true;
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.Location = new System.Drawing.Point(0, 104);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(664, 407);
            this.flow.TabIndex = 2;
            // 
            // panTitle
            // 
            this.panTitle.Controls.Add(this.panelGraded);
            this.panTitle.Controls.Add(this.panelTurnedin);
            this.panTitle.Controls.Add(this.panelAssigned);
            this.panTitle.Controls.Add(this.panTop);
            this.panTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTitle.Location = new System.Drawing.Point(0, 0);
            this.panTitle.Name = "panTitle";
            this.panTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panTitle.Size = new System.Drawing.Size(664, 104);
            this.panTitle.TabIndex = 1;
            // 
            // panelGraded
            // 
            this.panelGraded.Controls.Add(this.lblGradedcount);
            this.panelGraded.Controls.Add(this.lblGraded);
            this.panelGraded.Controls.Add(this.panel4);
            this.panelGraded.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGraded.Location = new System.Drawing.Point(210, 46);
            this.panelGraded.Name = "panelGraded";
            this.panelGraded.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelGraded.Size = new System.Drawing.Size(100, 58);
            this.panelGraded.TabIndex = 5;
            this.panelGraded.Visible = false;
            // 
            // lblGradedcount
            // 
            this.lblGradedcount.AutoSize = true;
            this.lblGradedcount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGradedcount.Font = new System.Drawing.Font("Nirmala UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGradedcount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblGradedcount.Location = new System.Drawing.Point(13, 0);
            this.lblGradedcount.Name = "lblGradedcount";
            this.lblGradedcount.Size = new System.Drawing.Size(33, 40);
            this.lblGradedcount.TabIndex = 37;
            this.lblGradedcount.Text = "0";
            this.lblGradedcount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblGraded
            // 
            this.lblGraded.AutoSize = true;
            this.lblGraded.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGraded.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGraded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblGraded.Location = new System.Drawing.Point(13, 37);
            this.lblGraded.Name = "lblGraded";
            this.lblGraded.Size = new System.Drawing.Size(61, 21);
            this.lblGraded.TabIndex = 36;
            this.lblGraded.Text = "Graded";
            this.lblGraded.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(10, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 58);
            this.panel4.TabIndex = 38;
            // 
            // panelTurnedin
            // 
            this.panelTurnedin.Controls.Add(this.lblTurnedincount);
            this.panelTurnedin.Controls.Add(this.lblTurnedin);
            this.panelTurnedin.Controls.Add(this.panel3);
            this.panelTurnedin.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTurnedin.Location = new System.Drawing.Point(110, 46);
            this.panelTurnedin.Name = "panelTurnedin";
            this.panelTurnedin.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelTurnedin.Size = new System.Drawing.Size(100, 58);
            this.panelTurnedin.TabIndex = 4;
            this.panelTurnedin.Visible = false;
            // 
            // lblTurnedincount
            // 
            this.lblTurnedincount.AutoSize = true;
            this.lblTurnedincount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTurnedincount.Font = new System.Drawing.Font("Nirmala UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnedincount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblTurnedincount.Location = new System.Drawing.Point(13, 0);
            this.lblTurnedincount.Name = "lblTurnedincount";
            this.lblTurnedincount.Size = new System.Drawing.Size(33, 40);
            this.lblTurnedincount.TabIndex = 37;
            this.lblTurnedincount.Text = "0";
            this.lblTurnedincount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTurnedin
            // 
            this.lblTurnedin.AutoSize = true;
            this.lblTurnedin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTurnedin.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnedin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblTurnedin.Location = new System.Drawing.Point(13, 37);
            this.lblTurnedin.Name = "lblTurnedin";
            this.lblTurnedin.Size = new System.Drawing.Size(75, 21);
            this.lblTurnedin.TabIndex = 36;
            this.lblTurnedin.Text = "Turned In";
            this.lblTurnedin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(10, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 58);
            this.panel3.TabIndex = 38;
            // 
            // panelAssigned
            // 
            this.panelAssigned.Controls.Add(this.lblAssignedcount);
            this.panelAssigned.Controls.Add(this.lblAssigned);
            this.panelAssigned.Controls.Add(this.panel5);
            this.panelAssigned.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAssigned.Location = new System.Drawing.Point(10, 46);
            this.panelAssigned.Name = "panelAssigned";
            this.panelAssigned.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelAssigned.Size = new System.Drawing.Size(100, 58);
            this.panelAssigned.TabIndex = 3;
            this.panelAssigned.Visible = false;
            // 
            // lblAssignedcount
            // 
            this.lblAssignedcount.AutoSize = true;
            this.lblAssignedcount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAssignedcount.Font = new System.Drawing.Font("Nirmala UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignedcount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblAssignedcount.Location = new System.Drawing.Point(13, 0);
            this.lblAssignedcount.Name = "lblAssignedcount";
            this.lblAssignedcount.Size = new System.Drawing.Size(33, 40);
            this.lblAssignedcount.TabIndex = 37;
            this.lblAssignedcount.Text = "0";
            this.lblAssignedcount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAssigned
            // 
            this.lblAssigned.AutoSize = true;
            this.lblAssigned.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAssigned.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssigned.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblAssigned.Location = new System.Drawing.Point(13, 37);
            this.lblAssigned.Name = "lblAssigned";
            this.lblAssigned.Size = new System.Drawing.Size(73, 21);
            this.lblAssigned.TabIndex = 36;
            this.lblAssigned.Text = "Assigned";
            this.lblAssigned.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(10, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 58);
            this.panel5.TabIndex = 39;
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.lblTasktitle);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(10, 0);
            this.panTop.Name = "panTop";
            this.panTop.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panTop.Size = new System.Drawing.Size(654, 46);
            this.panTop.TabIndex = 2;
            // 
            // lblTasktitle
            // 
            this.lblTasktitle.AutoSize = true;
            this.lblTasktitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTasktitle.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasktitle.ForeColor = System.Drawing.Color.Aqua;
            this.lblTasktitle.Location = new System.Drawing.Point(10, 0);
            this.lblTasktitle.Name = "lblTasktitle";
            this.lblTasktitle.Size = new System.Drawing.Size(24, 37);
            this.lblTasktitle.TabIndex = 34;
            this.lblTasktitle.Text = " ";
            this.lblTasktitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelScores
            // 
            this.panelScores.AutoScroll = true;
            this.panelScores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelScores.Controls.Add(this.groupAssigned);
            this.panelScores.Controls.Add(this.groupLate);
            this.panelScores.Controls.Add(this.groupWell);
            this.panelScores.Controls.Add(this.cmbSort);
            this.panelScores.Controls.Add(this.checkBox1);
            this.panelScores.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelScores.Location = new System.Drawing.Point(0, 50);
            this.panelScores.Name = "panelScores";
            this.panelScores.Padding = new System.Windows.Forms.Padding(10);
            this.panelScores.Size = new System.Drawing.Size(375, 511);
            this.panelScores.TabIndex = 62;
            // 
            // groupAssigned
            // 
            this.groupAssigned.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupAssigned.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupAssigned.Location = new System.Drawing.Point(10, 432);
            this.groupAssigned.Name = "groupAssigned";
            this.groupAssigned.Size = new System.Drawing.Size(338, 184);
            this.groupAssigned.TabIndex = 6;
            this.groupAssigned.TabStop = false;
            this.groupAssigned.Text = " ";
            this.groupAssigned.Visible = false;
            // 
            // groupLate
            // 
            this.groupLate.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupLate.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupLate.Location = new System.Drawing.Point(10, 248);
            this.groupLate.Name = "groupLate";
            this.groupLate.Size = new System.Drawing.Size(338, 184);
            this.groupLate.TabIndex = 4;
            this.groupLate.TabStop = false;
            this.groupLate.Text = " ";
            this.groupLate.Visible = false;
            // 
            // groupWell
            // 
            this.groupWell.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupWell.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupWell.Location = new System.Drawing.Point(10, 64);
            this.groupWell.Name = "groupWell";
            this.groupWell.Size = new System.Drawing.Size(338, 184);
            this.groupWell.TabIndex = 5;
            this.groupWell.TabStop = false;
            this.groupWell.Text = " ";
            this.groupWell.Visible = false;
            // 
            // cmbSort
            // 
            this.cmbSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.cmbSort.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSort.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSort.ForeColor = System.Drawing.Color.White;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Sort by date submitted",
            "Sort by First name",
            "Sort by Last name"});
            this.cmbSort.Location = new System.Drawing.Point(10, 35);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(338, 29);
            this.cmbSort.TabIndex = 2;
            this.cmbSort.Text = "Sort by Last name";
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(10, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(338, 25);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "All Students";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panelReturn
            // 
            this.panelReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelReturn.Controls.Add(this.btnReturn);
            this.panelReturn.Controls.Add(this.lblScore);
            this.panelReturn.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReturn.Location = new System.Drawing.Point(0, 0);
            this.panelReturn.Name = "panelReturn";
            this.panelReturn.Padding = new System.Windows.Forms.Padding(10);
            this.panelReturn.Size = new System.Drawing.Size(1039, 50);
            this.panelReturn.TabIndex = 61;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(56)))));
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnReturn.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReturn.Location = new System.Drawing.Point(12, 10);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(224, 30);
            this.btnReturn.TabIndex = 34;
            this.btnReturn.Text = "Return Score";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.lblScore.Location = new System.Drawing.Point(905, 19);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(52, 21);
            this.lblScore.TabIndex = 33;
            this.lblScore.Text = "Points";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SubmittedTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(1039, 611);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTitle);
            this.Name = "SubmittedTasks";
            this.Text = "SubmittedTasks";
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelSubmissions.ResumeLayout(false);
            this.panTitle.ResumeLayout(false);
            this.panelGraded.ResumeLayout(false);
            this.panelGraded.PerformLayout();
            this.panelTurnedin.ResumeLayout(false);
            this.panelTurnedin.PerformLayout();
            this.panelAssigned.ResumeLayout(false);
            this.panelAssigned.PerformLayout();
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panelScores.ResumeLayout(false);
            this.panelScores.PerformLayout();
            this.panelReturn.ResumeLayout(false);
            this.panelReturn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private FontAwesome.Sharp.IconButton btnGo;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelScores;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panelReturn;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.GroupBox groupLate;
        private System.Windows.Forms.GroupBox groupWell;
        private System.Windows.Forms.GroupBox groupAssigned;
        private System.Windows.Forms.Panel panelSubmissions;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private System.Windows.Forms.Panel panTitle;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Label lblTasktitle;
        private System.Windows.Forms.Panel panelGraded;
        private System.Windows.Forms.Label lblGradedcount;
        private System.Windows.Forms.Label lblGraded;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelTurnedin;
        private System.Windows.Forms.Label lblTurnedincount;
        private System.Windows.Forms.Label lblTurnedin;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelAssigned;
        private System.Windows.Forms.Label lblAssignedcount;
        private System.Windows.Forms.Label lblAssigned;
        private System.Windows.Forms.Panel panel5;
    }
}