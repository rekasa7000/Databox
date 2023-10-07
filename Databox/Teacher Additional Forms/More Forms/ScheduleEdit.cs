using MaterialSkin;
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

namespace Databox.Teacher_Additional_Forms.More_Forms
{
    public partial class ScheduleEdit : Form
    {
        int teacher_id, schedule_id;
        Panel desktopPanel;
        string scheduleName, scheduleMessage, selectedSchedule, selectedFrequency;
        string selectedStartTime, selectedEndTime;
        DateTime selectedDate;
        int selectedDay, selectedMonth;
        int? monthOfYear = null;
        int? dayOfMonth = null;
        string dayOfWeek = null;
        List<int> selectedDaysOfWeek = new List<int>();
        public ScheduleEdit(int teacherID, int scheduleID,Panel pan)
        {
            teacher_id = teacherID;
            schedule_id = scheduleID;
            desktopPanel = pan;
            InitializeComponent();
            customizeControls(); 
        }

        private void ScheduleEdit_Load(object sender, EventArgs e)
        {
            loadStudent();
            loadClasses();
            loadSchedules();
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
        void loadStudent()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = "SELECT CONCAT(s.studLastname, ', ', s.studFirstname, ' ', s.studMiddlename) as Fullname FROM student s " +
                           "INNER JOIN student_class sc ON s.student_id = sc.student_id " +
                           "INNER JOIN class c ON sc.class_id = c.class_id " +
                           "WHERE c.teacher_id = " + teacher_id + " and c.classArchive = 'NO'";

            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                string fullName = dataReader["Fullname"].ToString();
                cmbStudent.Items.Add(fullName);
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
                string className = dataReader["className"].ToString();
                cmbClass.Items.Add(className);
            }
            dataReader.Close();
            dbConnection.CloseConnection();
        }

        void loadSchedules()
        {
            Database_Connection dbConnection = new Database_Connection();
            string query = $"SELECT * FROM schedule where schedule_id = {schedule_id} AND teacher_id = {teacher_id}";
            MySqlDataReader dataReader = dbConnection.GetData(query);
            while (dataReader.Read())
            {
                string schedName = dataReader["schedName"].ToString();
                txtName.Text = schedName;

                string schedMessage = dataReader["schedMessage"].ToString();
                txtMessage.Text = schedMessage;

                int student_id = Convert.ToInt32(dataReader["student_id"].ToString());
                int class_id = Convert.ToInt32(dataReader["class_id"].ToString());

                if (student_id != 0)
                {
                    rdbtnStudent.Checked = true;
                    cmbStudent.Enabled = true;
                    cmbStudent.SelectedItem = getStudentName(student_id);
                }
                else
                {
                    rdbtnClass.Checked = true;
                    cmbClass.Enabled = true;
                    cmbClass.SelectedItem = getClassName(class_id);
                }

                string frequency = dataReader["frequency"].ToString();
                if (frequency == "Weekly")
                {
                    rdbtnWeekly.Checked = true;
                    panelWeek.Visible = true;

                    dateTimepicker.Enabled = false;
                    panelYearMonth.Visible = false;
                    string dayOfWeek = dataReader["dayOfWeek"].ToString();
                    List<int> selectedDays = dayOfWeek.Split(',').Select(int.Parse).ToList();
                    foreach (int day in selectedDays)
                    {
                        switch (day)
                        {
                            case 1:
                                checkMonday.Checked = true;
                                break;
                            case 2:
                                checkTuesday.Checked = true;
                                break;
                            case 3:
                                checkWednesday.Checked = true;
                                break;
                            case 4:
                                checkThursday.Checked = true;
                                break;
                            case 5:
                                checkFriday.Checked = true;
                                break;
                            case 6:
                                checkSaturday.Checked = true;
                                break;
                            case 7:
                                checkSunday.Checked = true;
                                break;
                        }
                    }
                }
                else if (frequency == "One-time")
                {
                    rdbtnOnetime.Checked = true;
                    dateTimepicker.Enabled = true;

                    panelWeek.Visible = false;
                    panelYearMonth.Visible = false;
                    string scheduleDate = dataReader["scheduleDate"].ToString();
                    DateTime dateValue;

                    if (DateTime.TryParse(scheduleDate, out dateValue))
                    {
                        dateTimepicker.Value = dateValue;
                    }
                    else
                    {
                        MessageBox.Show("Invalid date");
                    }

                }
                else if (frequency == "Monthly")
                {
                    rdbtnMonthly.Checked = true;
                    panelYearMonth.Visible = true;
                    panelDays.Visible = true;

                    panelMonth.Visible = false;
                    dateTimepicker.Enabled = false;
                    panelWeek.Visible = false;
                    string day = dataReader["dayOfMonth"].ToString();
                    cmbDay.SelectedItem = day;
                } else if (frequency == "Yearly")
                {
                    rdbtnYearly.Checked = true;
                    panelYearMonth.Visible = true;
                    panelDays.Visible = true;
                    panelMonth.Visible = true;

                    dateTimepicker.Enabled = false;
                    panelWeek.Visible = false;
                    string monthOfYearString = dataReader["monthOfYear"].ToString();
                    int month;

                    if (int.TryParse(monthOfYearString, out month))
                    {
                        if (month >= 1 && month <= 12)
                        {
                            cmbMonth.SelectedIndex = month - 1;
                        }
                    }

                    string day = dataReader["dayOfMonth"].ToString();
                    cmbDay.SelectedItem = day;
                }
                else
                {
                    rdbtnDaily.Checked = true;
                    dateTimepicker.Enabled = false;
                    panelWeek.Visible = false;
                    panelYearMonth.Visible = false;
                }

                string startTimeString = dataReader["startTime"].ToString();
                DateTime startTimeValue = DateTime.ParseExact(startTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);
                string startTime = startTimeValue.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                startTimepicker.Value = DateTime.ParseExact(startTime, "hh:mm tt", CultureInfo.InvariantCulture);


                string endTimeString = dataReader["endTime"].ToString();
                DateTime endTimes = DateTime.ParseExact(endTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);
                string endTime = endTimes.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                endTimepicker.Value = DateTime.ParseExact(endTime, "hh:mm tt", CultureInfo.InvariantCulture);


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
        private string getStudentName(int studentID)
        {
            Database_Connection db = new Database_Connection();
            string queryGetstudentname = $"SELECT CONCAT(studLastname, ', ', studFirstname, ' ', studMiddlename) FROM student WHERE student_id = '{studentID}'";

            string studentName = db.ExecuteScalar(queryGetstudentname).ToString();
            return studentName;
        }
        private string getClassName(int classID)
        {
            Database_Connection db = new Database_Connection();
            string queryGetclassname = $"SELECT className FROM class WHERE class_id = '{classID}'";
            string className = db.ExecuteScalar(queryGetclassname).ToString();
            return className;
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

        private void btnUpdateschedule_Click(object sender, EventArgs e)
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
            UpdateSchedule(scheduleName, scheduleMessage, studentId, classId, selectedStartTime, selectedEndTime, selectedDate, selectedFrequency, monthOfYear, dayOfMonth, dayOfWeek, teacher_id);
        }

        private void UpdateSchedule(string schedName, string schedMessage, int studentId, int classId, string startTime, string endTime, DateTime scheduleDate, string frequency, int? monthOfYear, int? dayOfMonth, string dayOfWeek, int teacherId)
        {
            Database_Connection dbConnection = new Database_Connection();
            if (dbConnection.OpenConnection())
            {
                string query = "UPDATE schedule SET " +
                               "schedName = @SchedName, " +
                               "schedMessage = @SchedMessage, " +
                               "student_id = @StudentId, " +
                               "class_id = @ClassId, " +
                               "startTime = @StartTime, " +
                               "endTime = @EndTime, " +
                               "scheduleDate = @ScheduleDate, " +
                               "frequency = @Frequency, " +
                               "scheduleInterval = @Interval, " +
                               "monthOfYear = @MonthOfYear, " +
                               "dayOfMonth = @DayOfMonth, " +
                               "dayOfWeek = @DayOfWeek " +
                               "WHERE schedule_id = @schedule_id";

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
                command.Parameters.AddWithValue("@schedule_id", schedule_id);

                command.ExecuteNonQuery();
                dbConnection.CloseConnection();
            }

            Clear();
            this.ActiveControl = this.Controls[0];

            MessageBox.Show("Schedule updated successfully!");
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
