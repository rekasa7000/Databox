using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ScheduleAdd : Form
    {
        int teacher_id;
        Panel desktopPanel;
        string scheduleName, scheduleMessage, selectedSchedule, selectedFrequency;
        string selectedStartTime, selectedEndTime;
        DateTime selectedDate;
        int selectedDay, selectedMonth;
        int? monthOfYear = null;
        int? dayOfMonth = null;
        string dayOfWeek = null;

        List<int> selectedDaysOfWeek = new List<int>();

        public ScheduleAdd(int teacherID, Panel pan)
        {
            teacher_id = teacherID;
            desktopPanel = pan;
            InitializeComponent();
            customizeControls();

        }
        private void customizeControls()
        {
            startTimepicker.Format = DateTimePickerFormat.Custom;
            startTimepicker.CustomFormat = "hh:mm tt";
            startTimepicker.Value = DateTime.Today.AddHours(0);
            startTimepicker.ShowUpDown = true;
            endTimepicker.Format = DateTimePickerFormat.Custom;
            endTimepicker.CustomFormat = "hh:mm tt";
            endTimepicker.Value = DateTime.Today.AddHours(0);
            endTimepicker.ShowUpDown = true;
            var skin = MaterialSkinManager.Instance;
            skin.Theme = MaterialSkinManager.Themes.DARK;
            skin.ColorScheme = new ColorScheme(
                Color.FromArgb(30, 31, 34),
                Color.FromArgb(43, 45, 49),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(92, 132, 217),
                TextShade.WHITE
                );
            for (int month = 1; month <= 12; month++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                cmbMonth.Items.Add(monthName);
            }
            for (int day = 1; day <= 31; day++)
            {
                cmbDay.Items.Add(day);
            }
        }
        private void ScheduleAdd_Load(object sender, EventArgs e)
        {
            loadStudent();
            loadClasses();
        }
        void loadStudent()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = "SELECT CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) as Fullname FROM student s " +
                            "INNER JOIN student_class sc ON s.student_id = sc.student_id " +
                            "INNER JOIN class c ON sc.class_id = c.class_id " +
                            "WHERE c.teacher_id = " + teacher_id + " and c.classArchive = 'NO' ORDER BY Fullname";

            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                cmbStudent.Items.Add(dataReader["Fullname"].ToString());
            }
            dataReader.Close();
            dbConnection.CloseConnection();
        }
        void loadClasses()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = "SELECT className FROM class where teacher_id = " + teacher_id + " and classArchive = 'NO'";
            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                cmbClass.Items.Add(dataReader["className"].ToString());
            }

            dataReader.Close();
            dbConnection.CloseConnection();
        }

        //database table IDs retrieval methods
        private int getStudentID(string selectedStudent)
        {
            Database_Connection db = new Database_Connection();
            string queryGetStudentId = $"SELECT student_id FROM student WHERE CONCAT(studLastname, ', ', studFirstname, ' ', studMiddlename) = '{selectedStudent}'";

            int studentId = Convert.ToInt32(db.ExecuteScalar(queryGetStudentId));
            return studentId;
        }
        private int getClassID(string selectedClass)
        {
            Database_Connection db = new Database_Connection();
            string queryGetClassId = $"SELECT class_id FROM class WHERE className = '{selectedClass}' AND teacher_id = '{teacher_id}' AND classArchive = 'NO'";
            int class_id = Convert.ToInt32(db.ExecuteScalar(queryGetClassId));
            return class_id;
        }

        //event listeners
        private void btnCreateschedule_Click(object sender, EventArgs e)
        {
            Fill();
            int studentId = 0;
            int classId = 0;

            if (rdbtnClass.Checked)
            {
                selectedSchedule = cmbClass.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedSchedule))
                {
                    classId = getClassID(selectedSchedule);
                }
            }
            else if (rdbtnStudent.Checked)
            {
                selectedSchedule = cmbStudent.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedSchedule))
                {
                    studentId = getStudentID(selectedSchedule);
                }
            }

            List<string> conflictingSchedules = CheckScheduleConflict(selectedStartTime, selectedEndTime, selectedDate, selectedFrequency, monthOfYear, dayOfMonth, dayOfWeek, teacher_id);

            if (conflictingSchedules.Count > 0)
            {
                string message = "There is a schedule conflict. Please choose a different time or date.\n\nConflicting Schedules:\n\n";
                foreach (string conflictingSchedule in conflictingSchedules)
                {
                    message += conflictingSchedule + "\n\n";
                }

                // Ask the user for confirmation to proceed
                DialogResult result = MessageBox.Show(message + "\nDo you want to proceed with adding the schedule?", "Schedule Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // Cancel adding the schedule
                }
            }

            // Insert the schedule into the database
            InsertSchedule(scheduleName, scheduleMessage, studentId, classId, selectedStartTime, selectedEndTime, selectedDate, selectedFrequency, monthOfYear, dayOfMonth, dayOfWeek, teacher_id);

            Clear();
            this.ActiveControl = this.Controls[0];
        }



        private void InsertSchedule(string schedName, string schedMessage, int studentId, int classId, string startTime, string endTime, DateTime scheduleDate, string frequency, int? monthOfYear, int? dayOfMonth, string dayOfWeek, int teacherId)
        {
            Database_Connection dbConnection = new Database_Connection();
            if (dbConnection.OpenConnection())
            {
                string query = "INSERT INTO schedule (schedName, schedMessage, student_id, class_id, startTime, endTime, scheduleDate, frequency, scheduleInterval, monthOfYear, dayOfMonth, dayOfWeek, isActive, teacher_id) " +
                    "VALUES (@SchedName, @SchedMessage, @StudentId, @ClassId, @StartTime, @EndTime, @ScheduleDate, @Frequency, @interval, @MonthOfYear, @DayOfMonth, @DayOfWeek, 1, @TeacherId)";

                MySqlCommand command = new MySqlCommand(query, dbConnection.connection);
                command.Parameters.AddWithValue("@SchedName", schedName);
                command.Parameters.AddWithValue("@SchedMessage", schedMessage);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@ClassId", classId);
                command.Parameters.AddWithValue("@StartTime", startTime);
                command.Parameters.AddWithValue("@EndTime", endTime);
                command.Parameters.AddWithValue("@ScheduleDate", scheduleDate != null ? scheduleDate.ToString("yyyy-MM-dd") : (object)DBNull.Value);
                command.Parameters.AddWithValue("@Frequency", frequency);
                command.Parameters.AddWithValue("@Interval", frequency != "One-time" ? 1 : 0);
                command.Parameters.AddWithValue("@MonthOfYear", monthOfYear.HasValue ? monthOfYear.Value : (object)DBNull.Value);
                command.Parameters.AddWithValue("@DayOfMonth", dayOfMonth.HasValue ? dayOfMonth.Value : (object)DBNull.Value);
                command.Parameters.AddWithValue("@DayOfWeek", !string.IsNullOrEmpty(dayOfWeek) ? dayOfWeek : (object)DBNull.Value);
                command.Parameters.AddWithValue("@TeacherId", teacherId);

                command.ExecuteNonQuery();
                dbConnection.CloseConnection();
            }
            Clear();
            this.ActiveControl = this.Controls[0];

            MessageBox.Show("Schedule added successfully!");
        }
        private List<string> CheckScheduleConflict(string startTime, string endTime, DateTime scheduleDate, string frequency, int? monthOfYear, int? dayOfMonth, string dayOfWeek, int teacherId)
        {
            List<string> conflictingSchedules = new List<string>();

            // Query the database to check for conflicts
            Database_Connection dbConnection = new Database_Connection();
            if (dbConnection.OpenConnection())
            {
                string query = "SELECT schedName, startTime, endTime, scheduleDate, frequency, monthOfYear, dayOfMonth, dayOfWeek " +
                   "FROM schedule " +
                   "WHERE teacher_id = @TeacherId " +
                   "AND (" +
                       "(frequency = 'One-time' AND scheduleDate = @ScheduleDate AND startTime <= @EndTime AND endTime >= @StartTime) " +
                       "OR " +
                       "(frequency = 'Monthly' AND dayOfMonth = @DayOfMonth AND startTime <= @EndTime AND endTime >= @StartTime AND scheduleDate <= @ScheduleDate) " +
                       "OR " +
                       "(frequency = 'Yearly' AND monthOfYear = @MonthOfYear AND dayOfMonth = @DayOfMonth AND startTime <= @EndTime AND endTime >= @StartTime AND scheduleDate <= @ScheduleDate) " +
                       "OR " +
                       "(frequency = 'Weekly' AND FIND_IN_SET(dayOfWeek, @DayOfWeek) > 0 AND startTime <= @EndTime AND endTime >= @StartTime AND scheduleDate <= @ScheduleDate) " +
                   ")";

                MySqlCommand command = new MySqlCommand(query, dbConnection.connection);
                command.Parameters.AddWithValue("@TeacherId", teacherId);
                command.Parameters.AddWithValue("@ScheduleDate", scheduleDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@StartTime", startTime);
                command.Parameters.AddWithValue("@EndTime", endTime);
                command.Parameters.AddWithValue("@DayOfMonth", dayOfMonth.HasValue ? dayOfMonth.Value : (object)DBNull.Value);
                command.Parameters.AddWithValue("@MonthOfYear", monthOfYear.HasValue ? monthOfYear.Value : (object)DBNull.Value);
                command.Parameters.AddWithValue("@DayOfWeek", !string.IsNullOrEmpty(dayOfWeek) ? dayOfWeek : (object)DBNull.Value);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    string schedName = dataReader["schedName"].ToString();
                    string conflictingStartTime = dataReader["startTime"].ToString();
                    string conflictingEndTime = dataReader["endTime"].ToString();
                    string conflictingScheduleDate = dataReader["scheduleDate"].ToString();
                    string conflictingFrequency = dataReader["frequency"].ToString();
                    string conflictingMonthOfYear = dataReader["monthOfYear"].ToString();
                    string conflictingDayOfMonth = dataReader["dayOfMonth"].ToString();
                    string conflictingDayOfWeek = dataReader["dayOfWeek"].ToString();

                    // Build a string representation of the conflicting schedule
                    string conflictingSchedule = $"Schedule Name: {schedName}\n" +
                                                 $"Start Time: {conflictingStartTime}\n" +
                                                 $"End Time: {conflictingEndTime}\n" +
                                                 $"Frequency: {conflictingFrequency}\n";
                    if (!string.IsNullOrEmpty(conflictingMonthOfYear))
                    {
                        conflictingSchedule += $"Month of Year: {conflictingMonthOfYear}\n";
                    }
                    if (!string.IsNullOrEmpty(conflictingDayOfMonth))
                    {
                        conflictingSchedule += $"Day of Month: {conflictingDayOfMonth}\n";
                    }
                    if (!string.IsNullOrEmpty(conflictingDayOfWeek))
                    {
                        conflictingSchedule += $"Day of Week: {conflictingDayOfWeek}";
                    }
                    conflictingSchedules.Add(conflictingSchedule);
                }

                dataReader.Close();
                dbConnection.CloseConnection();
            }

            return conflictingSchedules;
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            ScheduleView view = new ScheduleView(teacher_id, desktopPanel);
            view.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(view);
            view.Dock = DockStyle.Fill;
            view.FormBorderStyle = FormBorderStyle.None;
            view.Show();
            this.Close();
        }
        private void rdbtnClass_CheckedChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = true;
            cmbStudent.Enabled = false;
        }
        private void rdbtnStudent_CheckedChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = false;
            cmbStudent.Enabled = true;            
        }   

        private void rdbtnDaily_CheckedChanged(object sender, EventArgs e)
        {
            dateTimepicker.Enabled = false;
            panelWeek.Visible = false;
            panelYearMonth.Visible = false;
        }

        private void rdbtnOnetime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimepicker.Enabled = true;

            panelWeek.Visible = false;
            panelYearMonth.Visible = false;
        }
        private void rdbtnWeekly_CheckedChanged(object sender, EventArgs e)
        {
            panelWeek.Visible = true;

            dateTimepicker.Enabled = false;
            panelYearMonth.Visible = false;
        }
        private void rdbtnMonthly_CheckedChanged(object sender, EventArgs e)
        {            
            panelYearMonth.Visible = true;
            panelDays.Visible = true;

            panelMonth.Visible = false;
            dateTimepicker.Enabled = false;            
            panelWeek.Visible = false;
        }

        private void rdbtnYearly_CheckedChanged(object sender, EventArgs e)
        {
            panelYearMonth.Visible = true;
            panelDays.Visible = true;
            panelMonth.Visible = true;

            dateTimepicker.Enabled = false;
            panelWeek.Visible = false;
        }
        
        //Additional methods      
        void Fill()
        {
            scheduleName = txtName.Text;
            scheduleMessage = txtMessage.Text;            

            if (rdbtnOnetime.Checked)
            {
                selectedFrequency = "One-time";
                selectedDate = dateTimepicker.Value;
            }
            else if (rdbtnDaily.Checked)
            {
                selectedFrequency = "Daily";
            }
            else if (rdbtnWeekly.Checked)
            {
                selectedFrequency = "Weekly";
                if (checkMonday.Checked)
                    selectedDaysOfWeek.Add(1);
                if (checkTuesday.Checked)
                    selectedDaysOfWeek.Add(2);
                if (checkWednesday.Checked)
                    selectedDaysOfWeek.Add(3);
                if (checkThursday.Checked)
                    selectedDaysOfWeek.Add(4);
                if (checkFriday.Checked)
                    selectedDaysOfWeek.Add(5);
                if (checkSaturday.Checked)
                    selectedDaysOfWeek.Add(6);
                if (checkSunday.Checked)
                    selectedDaysOfWeek.Add(7);
                dayOfWeek = selectedDaysOfWeek.Count > 0 ? string.Join(",", selectedDaysOfWeek) : null;
            }
            else if (rdbtnMonthly.Checked)
            {
                selectedFrequency = "Monthly";
                selectedDay = int.Parse(cmbDay.SelectedItem.ToString());
                dayOfMonth = selectedDay;
            }
            else if (rdbtnYearly.Checked)
            {
                selectedFrequency = "Yearly";
                selectedMonth = cmbMonth.SelectedIndex + 1;
                selectedDay = int.Parse(cmbDay.SelectedItem.ToString());
                monthOfYear = selectedMonth;
                dayOfMonth = selectedDay;
            }

            //get start and end time
            string selectedStartTimeString = startTimepicker.Value.ToString("hh:mm tt");
            DateTime startTime = DateTime.ParseExact(selectedStartTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
            selectedStartTime = startTime.ToString("HH:mm", CultureInfo.InvariantCulture);

            string selectedEndTimeString = endTimepicker.Value.ToString("hh:mm tt");
            DateTime endTime = DateTime.ParseExact(selectedEndTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
            selectedEndTime = endTime.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
        void Clear()
        {
            txtName.Text = "";
            txtMessage.Text = "";
            scheduleName = null;
            scheduleMessage = null;
            selectedSchedule = null;
            selectedFrequency = null;
            selectedStartTime = null;
            selectedEndTime = null;
            selectedDate = DateTime.Now;
            selectedDay = 0;
            selectedMonth = 0;
            monthOfYear = null;
            dayOfMonth = null;
            dayOfWeek = null;
            ResetPanelControls(panelContainer);

        }

        private void ResetPanelControls(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Today.AddHours(0);
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                if (control is Panel nestedPanel)
                {                    
                    ResetPanelControls(nestedPanel);
                }
            }

            foreach (Control control in groupFor.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                if (control is RadioButton radio)
                {
                    radio.Checked = false;
                }
            }
            foreach (Control control in groupFrequency.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                if (control is RadioButton radio)
                {
                    radio.Checked = false;
                }
            }
            foreach (Control control in groupWeek.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                if (control is RadioButton radio)
                {
                    radio.Checked = false;
                }
            }
        }
    }
}
