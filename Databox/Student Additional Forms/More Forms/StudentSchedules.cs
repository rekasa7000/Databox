using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox.Student_Additional_Forms.More_Forms
{
    public partial class StudentSchedules : Form
    {
        int student_id, totalClasses, currentIndex;
        Panel desktopPanel;

        public StudentSchedules(int student_id, Panel desktopPan)
        {
            this.student_id = student_id;
            desktopPanel = desktopPan;
            InitializeComponent();
            CalculateTotalClasses();
        }

        private void StudentSchedules_Load(object sender, EventArgs e)
        {
            try
            {
                classLoad(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void classLoad(int startIndex)
        {
            try
            {
                Database_Connection db = new Database_Connection();
                List<string> classNames = new List<string>();
                List<string> scheduleNames = new List<string>();
                List<string> frequencies = new List<string>();
                List<string> timeDurations = new List<string>();
                List<string> schedules = new List<string>();
                List<string> scheduleIDs = new List<string>();

                db.OpenConnection();

                string query = "SELECT schedule.schedule_id, CONCAT(teacher.teacherFirstname, ' ', teacher.teacherLastname) AS classSelected, schedule.schedName, schedule.frequency, schedule.startTime, schedule.endTime, schedule.scheduleInterval, " +
                                "CASE " +
                                    "WHEN schedule.frequency = 'Daily' THEN NULL " +
                                    "WHEN schedule.frequency = 'Weekly' THEN schedule.dayOfWeek " +
                                    "WHEN schedule.frequency = 'Monthly' THEN schedule.dayOfMonth " +
                                    "WHEN schedule.frequency = 'Yearly' THEN CONCAT(schedule.monthOfYear, ', ', schedule.dayOfMonth) " +
                                    "ELSE schedule.scheduleDate " +
                                "END AS result " +
                                "FROM schedule " +
                                "JOIN student ON student.student_id = schedule.student_id " +
                                "JOIN teacher ON teacher.teacher_id = schedule.teacher_id " +
                                "WHERE schedule.student_id = @studentId AND isActive = 1 LIMIT @startIndex, 8";

                using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
                {
                    cmd.Parameters.AddWithValue("@studentId", student_id); // Replace with the desired student ID
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string scheduleID = reader.GetString("schedule_id");
                            scheduleIDs.Add(scheduleID);
                            string classSelected = reader.GetString("classSelected");
                            classNames.Add(classSelected);
                            string scheduleName = reader.GetString("schedName");
                            scheduleNames.Add(scheduleName);
                            string frequency = reader.GetString("frequency");
                            frequencies.Add(frequency);
                            string startTimeString = reader.GetString("startTime");
                            DateTime startTimes = DateTime.ParseExact(startTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);
                            string startTime = startTimes.ToString("hh:mm tt", CultureInfo.InvariantCulture);

                            string endTimeString = reader.GetString("endTime");
                            DateTime endTimes = DateTime.ParseExact(endTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);
                            string endTime = endTimes.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                            string timeDuration = $"{startTime} - {endTime}";
                            timeDurations.Add(timeDuration);
                            string result = reader.IsDBNull(reader.GetOrdinal("result")) ? null : reader.GetString("result");
                            if (frequency == "Weekly")
                            {
                                string weekdays = GetSeparatedWeekdays(result);
                                schedules.Add(weekdays);
                            }
                            else if (frequency == "Monthly")
                            {
                                string monthDay = $"Every day {result} ";
                                schedules.Add(monthDay);
                            }
                            else if (frequency == "Yearly")
                            {
                                string monthAndDay = GetYearlySchedule(result);
                                schedules.Add(monthAndDay);
                            }
                            else if (frequency == "One-time")
                            {
                                DateTime dueDate = DateTime.Parse(result);
                                string formattedDuedate = $"{dueDate.ToString("MMMM d, yyyy")}";
                                schedules.Add(formattedDuedate);
                            }
                            else
                            {
                                schedules.Add(null);
                            }
                        }
                    }
                }

                db.CloseConnection();

                for (int i = 0; i < classNames.Count; i++)
                {
                    Label lblSchedule = Controls.Find("lblSchedule" + (i + 1), true).FirstOrDefault() as Label;
                    if (lblSchedule != null)
                    {
                        lblSchedule.Text = scheduleNames[i];
                    }
                    Label lblClass = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
                    if (lblClass != null)
                    {
                        lblClass.Text = classNames[i];
                    }
                    Label lblTime = Controls.Find("lblTime" + (i + 1), true).FirstOrDefault() as Label;
                    if (lblTime != null)
                    {
                        lblTime.Text = timeDurations[i];
                    }
                    Label lblDate = Controls.Find("lblDate" + (i + 1), true).FirstOrDefault() as Label;
                    if (lblDate != null)
                    {
                        lblDate.Text = $"{frequencies[i]} - {schedules[i]}";
                    }
                    Button btnEdit = Controls.Find("btnEdit" + (i + 1), true).FirstOrDefault() as Button;
                    if (btnEdit != null)
                    {
                        btnEdit.Enabled = true;
                        btnEdit.Tag = scheduleIDs[i];
                    }
                    Button btnArchive = Controls.Find("btnActive" + (i + 1), true).FirstOrDefault() as Button;
                    if (btnArchive != null)
                    {
                        btnArchive.Enabled = true;
                        btnArchive.Tag = scheduleIDs[i];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private static string GetYearlySchedule(string yearlySchedule)
        {
            string[] values = yearlySchedule.Split(',');

            if (values.Length == 2 && int.TryParse(values[0], out int month) && int.TryParse(values[1], out int day))
            {
                string monthName = GetMonthNameFromValue(month);
                if (!string.IsNullOrEmpty(monthName))
                    return $"{monthName} {day}";
            }

            return string.Empty;
        }
        private static string GetMonthNameFromValue(int value)
        {
            if (value >= 1 && value <= 12)
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(value);

            return string.Empty;
        }
        private static string GetSeparatedWeekdays(string weeklySchedule)
        {
            string[] values = weeklySchedule.Split(',');
            StringBuilder weekdaysBuilder = new StringBuilder();

            foreach (string value in values)
            {
                string weekday = GetWeekdayFromValue(value.Trim());

                if (!string.IsNullOrEmpty(weekday))
                {
                    if (weekdaysBuilder.Length > 0)
                        weekdaysBuilder.Append(", ");

                    weekdaysBuilder.Append(weekday);
                }
            }

            return weekdaysBuilder.ToString();
        }
        private static string GetWeekdayFromValue(string value)
        {
            switch (value)
            {
                case "1":
                    return "Monday";
                case "2":
                    return "Tuesday";
                case "3":
                    return "Wednesday";
                case "4":
                    return "Thursday";
                case "5":
                    return "Friday";
                case "6":
                    return "Saturday";
                case "7":
                    return "Sunday";
                default:
                    return string.Empty;
            }
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            classLoad(0);
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            ClassSchedules view = new ClassSchedules(student_id, desktopPanel);
            view.TopLevel = false;
            desktopPanel.Controls.Clear(); // Clear existing controls in panelOpen
            desktopPanel.Controls.Add(view);
            view.Dock = DockStyle.Fill;
            view.FormBorderStyle = FormBorderStyle.None;
            view.Show();
            this.Close();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                currentIndex -= 8; // Decrement the current index by 5
                if (currentIndex < 0)
                {
                    MessageBox.Show("You are already at the beginning.");
                    currentIndex = 0; // Ensure the index doesn't go below 0
                }
                classLoad(currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                currentIndex += 8;

                if (currentIndex >= totalClasses)
                {
                    MessageBox.Show("No more appointment schedules to display.");
                    currentIndex -= 8;
                }
                Console.WriteLine(currentIndex);
                classLoad(currentIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        void Clear()
        {
            for (int i = 0; i < 8; i++)
            {
                Label lblSchedule = Controls.Find("lblSchedule" + (i + 1), true).FirstOrDefault() as Label;
                if (lblSchedule != null)
                {
                    lblSchedule.Text = "Empty";
                }
                Label lblClass = Controls.Find("lblClass" + (i + 1), true).FirstOrDefault() as Label;
                if (lblClass != null)
                {
                    lblClass.Text = "";
                }
                Label lblTime = Controls.Find("lblTime" + (i + 1), true).FirstOrDefault() as Label;
                if (lblTime != null)
                {
                    lblTime.Text = "";
                }
                Label lblDate = Controls.Find("lblDate" + (i + 1), true).FirstOrDefault() as Label;
                if (lblDate != null)
                {
                    lblDate.Text = "";
                }
            }
        }

        void CalculateTotalClasses()
        {
            try
            {
                Database_Connection db = new Database_Connection();
                db.OpenConnection();
                using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM schedule WHERE student_id = @student_id AND isActive = 1", db.connection))
                {
                    countCmd.Parameters.AddWithValue("@student_id", student_id);
                    totalClasses = Convert.ToInt32(countCmd.ExecuteScalar());
                }
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}

