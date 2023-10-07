
namespace Databox.Teacher_Additional_Forms.More_Forms
{
    partial class ScheduleAdd
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelYearMonth = new System.Windows.Forms.Panel();
            this.panelDays = new System.Windows.Forms.Panel();
            this.cmbDay = new MaterialSkin.Controls.MaterialComboBox();
            this.panelMonth = new System.Windows.Forms.Panel();
            this.cmbMonth = new MaterialSkin.Controls.MaterialComboBox();
            this.panelWeek = new System.Windows.Forms.Panel();
            this.groupWeek = new System.Windows.Forms.GroupBox();
            this.checkSaturday = new System.Windows.Forms.CheckBox();
            this.checkFriday = new System.Windows.Forms.CheckBox();
            this.checkSunday = new System.Windows.Forms.CheckBox();
            this.checkThursday = new System.Windows.Forms.CheckBox();
            this.checkWednesday = new System.Windows.Forms.CheckBox();
            this.checkTuesday = new System.Windows.Forms.CheckBox();
            this.checkMonday = new System.Windows.Forms.CheckBox();
            this.panelSchedule = new System.Windows.Forms.Panel();
            this.panelScheduleTime = new System.Windows.Forms.Panel();
            this.endTimepicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.startTimepicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimepicker = new System.Windows.Forms.DateTimePicker();
            this.groupFrequency = new System.Windows.Forms.GroupBox();
            this.rdbtnYearly = new System.Windows.Forms.RadioButton();
            this.rdbtnMonthly = new System.Windows.Forms.RadioButton();
            this.rdbtnWeekly = new System.Windows.Forms.RadioButton();
            this.rdbtnOnetime = new System.Windows.Forms.RadioButton();
            this.rdbtnDaily = new System.Windows.Forms.RadioButton();
            this.panelFor = new System.Windows.Forms.Panel();
            this.panelSelections = new System.Windows.Forms.Panel();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.lblForstudent = new System.Windows.Forms.Label();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.lblForclass = new System.Windows.Forms.Label();
            this.groupFor = new System.Windows.Forms.GroupBox();
            this.rdbtnClass = new System.Windows.Forms.RadioButton();
            this.rdbtnStudent = new System.Windows.Forms.RadioButton();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.txtMessage = new MaterialSkin.Controls.MaterialTextBox();
            this.panelName = new System.Windows.Forms.Panel();
            this.txtName = new MaterialSkin.Controls.MaterialTextBox();
            this.lblRegister = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.btnCreateschedule = new FontAwesome.Sharp.IconButton();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panelBlankBottom = new System.Windows.Forms.Panel();
            this.panelContainer.SuspendLayout();
            this.panelYearMonth.SuspendLayout();
            this.panelDays.SuspendLayout();
            this.panelMonth.SuspendLayout();
            this.panelWeek.SuspendLayout();
            this.groupWeek.SuspendLayout();
            this.panelSchedule.SuspendLayout();
            this.panelScheduleTime.SuspendLayout();
            this.groupFrequency.SuspendLayout();
            this.panelFor.SuspendLayout();
            this.panelSelections.SuspendLayout();
            this.groupFor.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.panelName.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelContainer.Controls.Add(this.panelYearMonth);
            this.panelContainer.Controls.Add(this.panelWeek);
            this.panelContainer.Controls.Add(this.panelSchedule);
            this.panelContainer.Controls.Add(this.panelFor);
            this.panelContainer.Controls.Add(this.panelMessage);
            this.panelContainer.Controls.Add(this.panelName);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContainer.Location = new System.Drawing.Point(100, 25);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(25);
            this.panelContainer.Size = new System.Drawing.Size(767, 531);
            this.panelContainer.TabIndex = 33;
            // 
            // panelYearMonth
            // 
            this.panelYearMonth.Controls.Add(this.panelDays);
            this.panelYearMonth.Controls.Add(this.panelMonth);
            this.panelYearMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelYearMonth.Location = new System.Drawing.Point(25, 474);
            this.panelYearMonth.Name = "panelYearMonth";
            this.panelYearMonth.Padding = new System.Windows.Forms.Padding(10);
            this.panelYearMonth.Size = new System.Drawing.Size(717, 71);
            this.panelYearMonth.TabIndex = 47;
            this.panelYearMonth.Visible = false;
            // 
            // panelDays
            // 
            this.panelDays.Controls.Add(this.cmbDay);
            this.panelDays.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDays.Location = new System.Drawing.Point(191, 10);
            this.panelDays.Name = "panelDays";
            this.panelDays.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelDays.Size = new System.Drawing.Size(183, 51);
            this.panelDays.TabIndex = 3;
            // 
            // cmbDay
            // 
            this.cmbDay.AutoResize = false;
            this.cmbDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDay.Depth = 0;
            this.cmbDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDay.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbDay.DropDownHeight = 174;
            this.cmbDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDay.DropDownWidth = 121;
            this.cmbDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbDay.ForeColor = System.Drawing.Color.White;
            this.cmbDay.FormattingEnabled = true;
            this.cmbDay.Hint = "Day of the Month";
            this.cmbDay.IntegralHeight = false;
            this.cmbDay.ItemHeight = 43;
            this.cmbDay.Location = new System.Drawing.Point(0, 0);
            this.cmbDay.MaxDropDownItems = 4;
            this.cmbDay.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbDay.Name = "cmbDay";
            this.cmbDay.Size = new System.Drawing.Size(173, 49);
            this.cmbDay.StartIndex = 0;
            this.cmbDay.TabIndex = 2;
            // 
            // panelMonth
            // 
            this.panelMonth.Controls.Add(this.cmbMonth);
            this.panelMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMonth.Location = new System.Drawing.Point(10, 10);
            this.panelMonth.Name = "panelMonth";
            this.panelMonth.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelMonth.Size = new System.Drawing.Size(181, 51);
            this.panelMonth.TabIndex = 2;
            // 
            // cmbMonth
            // 
            this.cmbMonth.AutoResize = false;
            this.cmbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMonth.Depth = 0;
            this.cmbMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbMonth.DropDownHeight = 174;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.DropDownWidth = 121;
            this.cmbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMonth.ForeColor = System.Drawing.Color.White;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Hint = "Month";
            this.cmbMonth.IntegralHeight = false;
            this.cmbMonth.ItemHeight = 43;
            this.cmbMonth.Location = new System.Drawing.Point(0, 0);
            this.cmbMonth.MaxDropDownItems = 4;
            this.cmbMonth.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(171, 49);
            this.cmbMonth.StartIndex = 0;
            this.cmbMonth.TabIndex = 2;
            // 
            // panelWeek
            // 
            this.panelWeek.Controls.Add(this.groupWeek);
            this.panelWeek.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWeek.Location = new System.Drawing.Point(25, 371);
            this.panelWeek.Name = "panelWeek";
            this.panelWeek.Size = new System.Drawing.Size(717, 103);
            this.panelWeek.TabIndex = 46;
            this.panelWeek.Visible = false;
            // 
            // groupWeek
            // 
            this.groupWeek.Controls.Add(this.checkSaturday);
            this.groupWeek.Controls.Add(this.checkFriday);
            this.groupWeek.Controls.Add(this.checkSunday);
            this.groupWeek.Controls.Add(this.checkThursday);
            this.groupWeek.Controls.Add(this.checkWednesday);
            this.groupWeek.Controls.Add(this.checkTuesday);
            this.groupWeek.Controls.Add(this.checkMonday);
            this.groupWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupWeek.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupWeek.ForeColor = System.Drawing.Color.White;
            this.groupWeek.Location = new System.Drawing.Point(0, 0);
            this.groupWeek.Name = "groupWeek";
            this.groupWeek.Size = new System.Drawing.Size(717, 103);
            this.groupWeek.TabIndex = 42;
            this.groupWeek.TabStop = false;
            // 
            // checkSaturday
            // 
            this.checkSaturday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSaturday.Location = new System.Drawing.Point(208, 59);
            this.checkSaturday.Name = "checkSaturday";
            this.checkSaturday.Size = new System.Drawing.Size(150, 30);
            this.checkSaturday.TabIndex = 7;
            this.checkSaturday.Text = "Saturday";
            this.checkSaturday.UseVisualStyleBackColor = true;
            // 
            // checkFriday
            // 
            this.checkFriday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFriday.Location = new System.Drawing.Point(41, 59);
            this.checkFriday.Name = "checkFriday";
            this.checkFriday.Size = new System.Drawing.Size(150, 30);
            this.checkFriday.TabIndex = 6;
            this.checkFriday.Text = "Friday";
            this.checkFriday.UseVisualStyleBackColor = true;
            // 
            // checkSunday
            // 
            this.checkSunday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSunday.Location = new System.Drawing.Point(385, 59);
            this.checkSunday.Name = "checkSunday";
            this.checkSunday.Size = new System.Drawing.Size(150, 30);
            this.checkSunday.TabIndex = 4;
            this.checkSunday.Text = "Sunday";
            this.checkSunday.UseVisualStyleBackColor = true;
            // 
            // checkThursday
            // 
            this.checkThursday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkThursday.Location = new System.Drawing.Point(562, 28);
            this.checkThursday.Name = "checkThursday";
            this.checkThursday.Size = new System.Drawing.Size(150, 30);
            this.checkThursday.TabIndex = 3;
            this.checkThursday.Text = "Monday";
            this.checkThursday.UseVisualStyleBackColor = true;
            // 
            // checkWednesday
            // 
            this.checkWednesday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkWednesday.Location = new System.Drawing.Point(385, 28);
            this.checkWednesday.Name = "checkWednesday";
            this.checkWednesday.Size = new System.Drawing.Size(150, 30);
            this.checkWednesday.TabIndex = 2;
            this.checkWednesday.Text = "Wednesday";
            this.checkWednesday.UseVisualStyleBackColor = true;
            // 
            // checkTuesday
            // 
            this.checkTuesday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTuesday.Location = new System.Drawing.Point(208, 28);
            this.checkTuesday.Name = "checkTuesday";
            this.checkTuesday.Size = new System.Drawing.Size(150, 30);
            this.checkTuesday.TabIndex = 1;
            this.checkTuesday.Text = "Tuesday";
            this.checkTuesday.UseVisualStyleBackColor = true;
            // 
            // checkMonday
            // 
            this.checkMonday.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMonday.Location = new System.Drawing.Point(41, 28);
            this.checkMonday.Name = "checkMonday";
            this.checkMonday.Size = new System.Drawing.Size(150, 30);
            this.checkMonday.TabIndex = 0;
            this.checkMonday.Text = "Monday";
            this.checkMonday.UseVisualStyleBackColor = true;
            // 
            // panelSchedule
            // 
            this.panelSchedule.Controls.Add(this.panelScheduleTime);
            this.panelSchedule.Controls.Add(this.groupFrequency);
            this.panelSchedule.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSchedule.Location = new System.Drawing.Point(25, 262);
            this.panelSchedule.Name = "panelSchedule";
            this.panelSchedule.Size = new System.Drawing.Size(717, 109);
            this.panelSchedule.TabIndex = 41;
            // 
            // panelScheduleTime
            // 
            this.panelScheduleTime.Controls.Add(this.endTimepicker);
            this.panelScheduleTime.Controls.Add(this.label2);
            this.panelScheduleTime.Controls.Add(this.startTimepicker);
            this.panelScheduleTime.Controls.Add(this.label1);
            this.panelScheduleTime.Controls.Add(this.dateTimepicker);
            this.panelScheduleTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScheduleTime.Location = new System.Drawing.Point(0, 63);
            this.panelScheduleTime.Name = "panelScheduleTime";
            this.panelScheduleTime.Padding = new System.Windows.Forms.Padding(10);
            this.panelScheduleTime.Size = new System.Drawing.Size(717, 46);
            this.panelScheduleTime.TabIndex = 46;
            // 
            // endTimepicker
            // 
            this.endTimepicker.CalendarFont = new System.Drawing.Font("Nirmala UI", 11F);
            this.endTimepicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.endTimepicker.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTimepicker.Location = new System.Drawing.Point(535, 10);
            this.endTimepicker.Name = "endTimepicker";
            this.endTimepicker.Size = new System.Drawing.Size(172, 27);
            this.endTimepicker.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(491, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 21);
            this.label2.TabIndex = 49;
            this.label2.Text = "End:";
            // 
            // startTimepicker
            // 
            this.startTimepicker.CalendarFont = new System.Drawing.Font("Nirmala UI", 11F);
            this.startTimepicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.startTimepicker.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimepicker.Location = new System.Drawing.Point(319, 10);
            this.startTimepicker.Name = "startTimepicker";
            this.startTimepicker.Size = new System.Drawing.Size(172, 27);
            this.startTimepicker.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(267, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 48;
            this.label1.Text = "Start:";
            // 
            // dateTimepicker
            // 
            this.dateTimepicker.CalendarFont = new System.Drawing.Font("Nirmala UI", 11F);
            this.dateTimepicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimepicker.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimepicker.Location = new System.Drawing.Point(10, 10);
            this.dateTimepicker.Name = "dateTimepicker";
            this.dateTimepicker.Size = new System.Drawing.Size(257, 27);
            this.dateTimepicker.TabIndex = 45;
            // 
            // groupFrequency
            // 
            this.groupFrequency.Controls.Add(this.rdbtnYearly);
            this.groupFrequency.Controls.Add(this.rdbtnMonthly);
            this.groupFrequency.Controls.Add(this.rdbtnWeekly);
            this.groupFrequency.Controls.Add(this.rdbtnOnetime);
            this.groupFrequency.Controls.Add(this.rdbtnDaily);
            this.groupFrequency.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupFrequency.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFrequency.ForeColor = System.Drawing.Color.White;
            this.groupFrequency.Location = new System.Drawing.Point(0, 0);
            this.groupFrequency.Name = "groupFrequency";
            this.groupFrequency.Padding = new System.Windows.Forms.Padding(50, 0, 10, 10);
            this.groupFrequency.Size = new System.Drawing.Size(717, 63);
            this.groupFrequency.TabIndex = 45;
            this.groupFrequency.TabStop = false;
            this.groupFrequency.Text = "Schedule";
            // 
            // rdbtnYearly
            // 
            this.rdbtnYearly.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdbtnYearly.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnYearly.ForeColor = System.Drawing.Color.White;
            this.rdbtnYearly.Location = new System.Drawing.Point(570, 22);
            this.rdbtnYearly.Name = "rdbtnYearly";
            this.rdbtnYearly.Size = new System.Drawing.Size(130, 31);
            this.rdbtnYearly.TabIndex = 1;
            this.rdbtnYearly.TabStop = true;
            this.rdbtnYearly.Text = "Yearly";
            this.rdbtnYearly.UseVisualStyleBackColor = true;
            this.rdbtnYearly.CheckedChanged += new System.EventHandler(this.rdbtnYearly_CheckedChanged);
            // 
            // rdbtnMonthly
            // 
            this.rdbtnMonthly.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdbtnMonthly.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnMonthly.ForeColor = System.Drawing.Color.White;
            this.rdbtnMonthly.Location = new System.Drawing.Point(440, 22);
            this.rdbtnMonthly.Name = "rdbtnMonthly";
            this.rdbtnMonthly.Size = new System.Drawing.Size(130, 31);
            this.rdbtnMonthly.TabIndex = 3;
            this.rdbtnMonthly.TabStop = true;
            this.rdbtnMonthly.Text = "Monthly";
            this.rdbtnMonthly.UseVisualStyleBackColor = true;
            this.rdbtnMonthly.CheckedChanged += new System.EventHandler(this.rdbtnMonthly_CheckedChanged);
            // 
            // rdbtnWeekly
            // 
            this.rdbtnWeekly.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdbtnWeekly.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnWeekly.ForeColor = System.Drawing.Color.White;
            this.rdbtnWeekly.Location = new System.Drawing.Point(310, 22);
            this.rdbtnWeekly.Name = "rdbtnWeekly";
            this.rdbtnWeekly.Size = new System.Drawing.Size(130, 31);
            this.rdbtnWeekly.TabIndex = 4;
            this.rdbtnWeekly.TabStop = true;
            this.rdbtnWeekly.Text = "Weekly";
            this.rdbtnWeekly.UseVisualStyleBackColor = true;
            this.rdbtnWeekly.CheckedChanged += new System.EventHandler(this.rdbtnWeekly_CheckedChanged);
            // 
            // rdbtnOnetime
            // 
            this.rdbtnOnetime.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdbtnOnetime.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnOnetime.ForeColor = System.Drawing.Color.White;
            this.rdbtnOnetime.Location = new System.Drawing.Point(180, 22);
            this.rdbtnOnetime.Name = "rdbtnOnetime";
            this.rdbtnOnetime.Size = new System.Drawing.Size(130, 31);
            this.rdbtnOnetime.TabIndex = 2;
            this.rdbtnOnetime.TabStop = true;
            this.rdbtnOnetime.Text = "One-Time";
            this.rdbtnOnetime.UseVisualStyleBackColor = true;
            this.rdbtnOnetime.CheckedChanged += new System.EventHandler(this.rdbtnOnetime_CheckedChanged);
            // 
            // rdbtnDaily
            // 
            this.rdbtnDaily.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdbtnDaily.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnDaily.ForeColor = System.Drawing.Color.White;
            this.rdbtnDaily.Location = new System.Drawing.Point(50, 22);
            this.rdbtnDaily.Name = "rdbtnDaily";
            this.rdbtnDaily.Size = new System.Drawing.Size(130, 31);
            this.rdbtnDaily.TabIndex = 0;
            this.rdbtnDaily.TabStop = true;
            this.rdbtnDaily.Text = "Daily";
            this.rdbtnDaily.UseVisualStyleBackColor = true;
            this.rdbtnDaily.CheckedChanged += new System.EventHandler(this.rdbtnDaily_CheckedChanged);
            // 
            // panelFor
            // 
            this.panelFor.Controls.Add(this.panelSelections);
            this.panelFor.Controls.Add(this.groupFor);
            this.panelFor.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFor.Location = new System.Drawing.Point(25, 156);
            this.panelFor.Name = "panelFor";
            this.panelFor.Size = new System.Drawing.Size(717, 106);
            this.panelFor.TabIndex = 40;
            // 
            // panelSelections
            // 
            this.panelSelections.Controls.Add(this.cmbStudent);
            this.panelSelections.Controls.Add(this.lblForstudent);
            this.panelSelections.Controls.Add(this.cmbClass);
            this.panelSelections.Controls.Add(this.lblForclass);
            this.panelSelections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelections.Location = new System.Drawing.Point(291, 0);
            this.panelSelections.Name = "panelSelections";
            this.panelSelections.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panelSelections.Size = new System.Drawing.Size(426, 106);
            this.panelSelections.TabIndex = 42;
            // 
            // cmbStudent
            // 
            this.cmbStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.cmbStudent.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudent.Enabled = false;
            this.cmbStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStudent.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStudent.ForeColor = System.Drawing.Color.White;
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(10, 74);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(406, 29);
            this.cmbStudent.TabIndex = 58;
            // 
            // lblForstudent
            // 
            this.lblForstudent.AutoSize = true;
            this.lblForstudent.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblForstudent.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForstudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(205)))));
            this.lblForstudent.Location = new System.Drawing.Point(10, 53);
            this.lblForstudent.Name = "lblForstudent";
            this.lblForstudent.Size = new System.Drawing.Size(73, 21);
            this.lblForstudent.TabIndex = 57;
            this.lblForstudent.Text = "Student";
            // 
            // cmbClass
            // 
            this.cmbClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.cmbClass.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.Enabled = false;
            this.cmbClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClass.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClass.ForeColor = System.Drawing.Color.White;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(10, 24);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(406, 29);
            this.cmbClass.TabIndex = 56;
            // 
            // lblForclass
            // 
            this.lblForclass.AutoSize = true;
            this.lblForclass.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblForclass.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForclass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(205)))));
            this.lblForclass.Location = new System.Drawing.Point(10, 3);
            this.lblForclass.Name = "lblForclass";
            this.lblForclass.Size = new System.Drawing.Size(49, 21);
            this.lblForclass.TabIndex = 55;
            this.lblForclass.Text = "Class";
            // 
            // groupFor
            // 
            this.groupFor.Controls.Add(this.rdbtnClass);
            this.groupFor.Controls.Add(this.rdbtnStudent);
            this.groupFor.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupFor.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFor.ForeColor = System.Drawing.Color.White;
            this.groupFor.Location = new System.Drawing.Point(0, 0);
            this.groupFor.Name = "groupFor";
            this.groupFor.Size = new System.Drawing.Size(291, 106);
            this.groupFor.TabIndex = 41;
            this.groupFor.TabStop = false;
            this.groupFor.Text = "For";
            // 
            // rdbtnClass
            // 
            this.rdbtnClass.AutoSize = true;
            this.rdbtnClass.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnClass.ForeColor = System.Drawing.Color.White;
            this.rdbtnClass.Location = new System.Drawing.Point(50, 28);
            this.rdbtnClass.Name = "rdbtnClass";
            this.rdbtnClass.Size = new System.Drawing.Size(155, 29);
            this.rdbtnClass.TabIndex = 2;
            this.rdbtnClass.TabStop = true;
            this.rdbtnClass.Text = "Class Schedule";
            this.rdbtnClass.UseVisualStyleBackColor = true;
            this.rdbtnClass.CheckedChanged += new System.EventHandler(this.rdbtnClass_CheckedChanged);
            // 
            // rdbtnStudent
            // 
            this.rdbtnStudent.AutoSize = true;
            this.rdbtnStudent.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnStudent.ForeColor = System.Drawing.Color.White;
            this.rdbtnStudent.Location = new System.Drawing.Point(50, 59);
            this.rdbtnStudent.Name = "rdbtnStudent";
            this.rdbtnStudent.Size = new System.Drawing.Size(176, 29);
            this.rdbtnStudent.TabIndex = 1;
            this.rdbtnStudent.TabStop = true;
            this.rdbtnStudent.Text = "Student Schedule";
            this.rdbtnStudent.UseVisualStyleBackColor = true;
            this.rdbtnStudent.CheckedChanged += new System.EventHandler(this.rdbtnStudent_CheckedChanged);
            // 
            // panelMessage
            // 
            this.panelMessage.Controls.Add(this.txtMessage);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMessage.Location = new System.Drawing.Point(25, 90);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelMessage.Size = new System.Drawing.Size(717, 66);
            this.panelMessage.TabIndex = 45;
            // 
            // txtMessage
            // 
            this.txtMessage.AnimateReadOnly = false;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Depth = 0;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMessage.ForeColor = System.Drawing.Color.White;
            this.txtMessage.Hint = "Message";
            this.txtMessage.LeadingIcon = null;
            this.txtMessage.Location = new System.Drawing.Point(10, 5);
            this.txtMessage.MaxLength = 50;
            this.txtMessage.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMessage.Multiline = false;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(697, 50);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "";
            this.txtMessage.TrailingIcon = null;
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.txtName);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelName.Location = new System.Drawing.Point(25, 25);
            this.panelName.Name = "panelName";
            this.panelName.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelName.Size = new System.Drawing.Size(717, 65);
            this.panelName.TabIndex = 42;
            // 
            // txtName
            // 
            this.txtName.AnimateReadOnly = false;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Depth = 0;
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Hint = "Schedule Name";
            this.txtName.LeadingIcon = null;
            this.txtName.Location = new System.Drawing.Point(10, 5);
            this.txtName.MaxLength = 50;
            this.txtName.MouseState = MaterialSkin.MouseState.OUT;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(697, 50);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "";
            this.txtName.TrailingIcon = null;
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(205)))));
            this.lblRegister.Location = new System.Drawing.Point(117, 9);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(217, 29);
            this.lblRegister.TabIndex = 32;
            this.lblRegister.Text = "Create Schedule";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelTitle.Controls.Add(this.lblRegister);
            this.panelTitle.Controls.Add(this.btnCreateschedule);
            this.panelTitle.Controls.Add(this.btnCancel);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(3, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(984, 50);
            this.panelTitle.TabIndex = 55;
            // 
            // btnCreateschedule
            // 
            this.btnCreateschedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.btnCreateschedule.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCreateschedule.FlatAppearance.BorderSize = 0;
            this.btnCreateschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateschedule.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateschedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCreateschedule.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnCreateschedule.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCreateschedule.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCreateschedule.IconSize = 30;
            this.btnCreateschedule.Location = new System.Drawing.Point(796, 0);
            this.btnCreateschedule.Name = "btnCreateschedule";
            this.btnCreateschedule.Size = new System.Drawing.Size(188, 50);
            this.btnCreateschedule.TabIndex = 0;
            this.btnCreateschedule.Text = "Create Schedule";
            this.btnCreateschedule.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCreateschedule.UseVisualStyleBackColor = false;
            this.btnCreateschedule.Click += new System.EventHandler(this.btnCreateschedule_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCancel.IconChar = FontAwesome.Sharp.IconChar.ArrowLeftLong;
            this.btnCancel.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(196)))), ((int)(((byte)(165)))));
            this.btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancel.IconSize = 30;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 50);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Back";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.mainPanel.Controls.Add(this.panelBlankBottom);
            this.mainPanel.Controls.Add(this.panelContainer);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(3, 50);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(100, 25, 100, 50);
            this.mainPanel.Size = new System.Drawing.Size(984, 467);
            this.mainPanel.TabIndex = 57;
            // 
            // panelBlankBottom
            // 
            this.panelBlankBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBlankBottom.Location = new System.Drawing.Point(100, 506);
            this.panelBlankBottom.Name = "panelBlankBottom";
            this.panelBlankBottom.Size = new System.Drawing.Size(767, 62);
            this.panelBlankBottom.TabIndex = 44;
            // 
            // ScheduleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(113)))), ((int)(((byte)(117)))));
            this.ClientSize = new System.Drawing.Size(990, 520);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panelTitle);
            this.Name = "ScheduleAdd";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Text = "ScheduleAdd";
            this.Load += new System.EventHandler(this.ScheduleAdd_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelYearMonth.ResumeLayout(false);
            this.panelDays.ResumeLayout(false);
            this.panelMonth.ResumeLayout(false);
            this.panelWeek.ResumeLayout(false);
            this.groupWeek.ResumeLayout(false);
            this.panelSchedule.ResumeLayout(false);
            this.panelScheduleTime.ResumeLayout(false);
            this.panelScheduleTime.PerformLayout();
            this.groupFrequency.ResumeLayout(false);
            this.panelFor.ResumeLayout(false);
            this.panelSelections.ResumeLayout(false);
            this.panelSelections.PerformLayout();
            this.groupFor.ResumeLayout(false);
            this.groupFor.PerformLayout();
            this.panelMessage.ResumeLayout(false);
            this.panelName.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelFor;
        private System.Windows.Forms.Panel panelSelections;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.Label lblForstudent;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.Label lblForclass;
        private System.Windows.Forms.GroupBox groupFor;
        private System.Windows.Forms.RadioButton rdbtnClass;
        private System.Windows.Forms.RadioButton rdbtnStudent;
        private System.Windows.Forms.Panel panelSchedule;
        private System.Windows.Forms.GroupBox groupFrequency;
        private System.Windows.Forms.RadioButton rdbtnWeekly;
        private System.Windows.Forms.RadioButton rdbtnMonthly;
        private System.Windows.Forms.RadioButton rdbtnYearly;
        private System.Windows.Forms.RadioButton rdbtnDaily;
        private System.Windows.Forms.Panel panelScheduleTime;
        private System.Windows.Forms.DateTimePicker endTimepicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker startTimepicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimepicker;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Panel panelTitle;
        private FontAwesome.Sharp.IconButton btnCancel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.RadioButton rdbtnOnetime;
        private System.Windows.Forms.Panel panelWeek;
        private System.Windows.Forms.GroupBox groupWeek;
        private System.Windows.Forms.CheckBox checkSaturday;
        private System.Windows.Forms.CheckBox checkFriday;
        private System.Windows.Forms.CheckBox checkSunday;
        private System.Windows.Forms.CheckBox checkThursday;
        private System.Windows.Forms.CheckBox checkWednesday;
        private System.Windows.Forms.CheckBox checkTuesday;
        private System.Windows.Forms.CheckBox checkMonday;
        private System.Windows.Forms.Panel panelMessage;
        private FontAwesome.Sharp.IconButton btnCreateschedule;
        private System.Windows.Forms.Panel panelYearMonth;
        private MaterialSkin.Controls.MaterialTextBox txtMessage;
        private MaterialSkin.Controls.MaterialTextBox txtName;
        private System.Windows.Forms.Panel panelDays;
        private MaterialSkin.Controls.MaterialComboBox cmbDay;
        private System.Windows.Forms.Panel panelMonth;
        private MaterialSkin.Controls.MaterialComboBox cmbMonth;
        private System.Windows.Forms.Panel panelBlankBottom;
    }
}