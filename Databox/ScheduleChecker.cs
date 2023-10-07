using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databox
{
    class ScheduleChecker
    {
        private SessionData sessionData = SessionManager.LoadSession();
        private List<Schedule> schedules = new List<Schedule>();
        private Form activeForm;
        private CancellationTokenSource cancellationTokenSource;

        private class Schedule
        {
            public string ScheduleID { get; set; }
            public string ClassSelected { get; set; }
            public string ScheduleName { get; set; }
            public string Frequency { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public int? DayOfWeek { get; set; }
            public int? DayOfMonth { get; set; }
            public int? MonthOfYear { get; set; }
            public string ScheduleDate { get; set; }
        }

        public ScheduleChecker(Form form)
        {
            activeForm = form;
        }

        string email;
        void getEmail()
        {
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            string query = $"SELECT email FROM user JOIN teacher ON user.user_id = teacher.user_id WHERE teacher_id = {sessionData.TeacherId}";

            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@teacherId", sessionData.TeacherId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        email = reader.GetString("email");
                    }
                }
            }
        }


        private void FetchSchedules()
        {
            schedules.Clear();
            Database_Connection db = new Database_Connection();
            db.OpenConnection();
            string query = "SELECT schedule.schedule_id, CONCAT(class.className,' - ', class.classSection) AS classSelected, schedule.schedName, schedule.frequency, schedule.startTime, schedule.endTime, schedule.dayOfWeek, schedule.dayOfMonth, schedule.monthOfYear, schedule.scheduleDate " +
                           "FROM schedule " +
                           "JOIN class ON class.class_id = schedule.class_id " +
                           "WHERE schedule.teacher_id = @teacherId AND isActive = 1";

            using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
            {
                cmd.Parameters.AddWithValue("@teacherId", sessionData.TeacherId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Schedule schedule = new Schedule();
                        schedule.ScheduleID = reader.GetString("schedule_id");
                        schedule.ClassSelected = reader.GetString("classSelected");
                        schedule.ScheduleName = reader.GetString("schedName");
                        schedule.Frequency = reader.GetString("frequency");
                        schedule.StartTime = reader.GetString("startTime");
                        schedule.EndTime = reader.GetString("endTime");

                        if (!reader.IsDBNull(reader.GetOrdinal("dayOfWeek")) && int.TryParse(reader.GetString("dayOfWeek"), out int dayOfWeek))
                        {
                            schedule.DayOfWeek = dayOfWeek;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("dayOfMonth")) && int.TryParse(reader.GetString("dayOfMonth"), out int dayOfMonth))
                        {
                            schedule.DayOfMonth = dayOfMonth;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("monthOfYear")) && int.TryParse(reader.GetString("monthOfYear"), out int monthOfYear))
                        {
                            schedule.MonthOfYear = monthOfYear;
                        }

                        schedule.ScheduleDate = reader.IsDBNull(reader.GetOrdinal("scheduleDate")) ? null : reader.GetString("scheduleDate");

                        schedules.Add(schedule);
                    }
                }
                db.CloseConnection();
            }
        }

        public void StartCheckingSchedules()
        {
            getEmail();
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CheckSchedulesContinuously(cancellationTokenSource.Token));
        }

        public void StopCheckingSchedules()
        {
            cancellationTokenSource?.Cancel();
        }

        public void CheckSchedulesContinuously(CancellationToken cancellationToken)
        {
            bool reminderShown = false; // Track if a reminder has been shown

            while (!cancellationToken.IsCancellationRequested && !reminderShown)
            {
                FetchSchedules();

                foreach (var schedule in schedules)
                {
                    string reminder = $"Upcoming schedule: {schedule.ScheduleName} is happening now!" +
                        $"You have class right now at {schedule.ClassSelected}";

                    if (schedule.Frequency == "Weekly")
                    {
                        DayOfWeek? currentDayOfWeek = DateTime.Now.DayOfWeek;
                        DayOfWeek? scheduleDayOfWeek = GetDayOfWeekFromValue(schedule.DayOfWeek + 1);
                        if (currentDayOfWeek == scheduleDayOfWeek && schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                        {
                            ShowReminderForm(schedule.ClassSelected, reminder);
                            reminderShown = true; // Set the flag to true to indicate that a reminder has been shown
                            break; // Exit the loop since we don't need to check other schedules anymore
                        }
                    }
                    else if (schedule.Frequency == "One-time")
                    {
                        DateTime? scheduleDateTimeStr = ParseNullableDateTime(schedule.ScheduleDate);
                        DateTime scheduleDateTime;
                        if (scheduleDateTimeStr.HasValue)
                        {
                            scheduleDateTime = scheduleDateTimeStr.Value.Date;
                            if (DateTime.Now.ToString("MM/dd/yyyy") == scheduleDateTime.ToString("MM/dd/yyyy") && schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                            {
                                ShowReminderForm(schedule.ClassSelected, reminder);
                                reminderShown = true; // Set the flag to true to indicate that a reminder has been shown
                                break; // Exit the loop since we don't need to check other schedules anymore
                            }
                        }
                    }
                    else if (schedule.Frequency == "Yearly")
                    {
                        DateTime? scheduleDate = ParseNullableDateTime($"{schedule.MonthOfYear}/{schedule.DayOfMonth}");
                        if (scheduleDate.HasValue)
                        {
                            string formattedDate = scheduleDate.Value.ToString("MM/dd");
                            DateTime currentDateTime = DateTime.Now;
                            if (currentDateTime.ToString("MM/dd") == formattedDate && schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                            {
                                ShowReminderForm(schedule.ClassSelected, reminder);
                                reminderShown = true; // Set the flag to true to indicate that a reminder has been shown
                                break; // Exit the loop since we don't need to check other schedules anymore
                            }
                        }
                    }
                    else if (schedule.Frequency == "Monthly")
                    {
                        int? scheduleDayOfMonth = schedule.DayOfMonth;
                        DateTime currentDateTime = DateTime.Now;
                        if (scheduleDayOfMonth.HasValue && currentDateTime.Day == scheduleDayOfMonth && schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                        {
                            ShowReminderForm(schedule.ClassSelected, reminder);
                            reminderShown = true; // Set the flag to true to indicate that a reminder has been shown
                            break; // Exit the loop since we don't need to check other schedules anymore
                        }
                    }
                    else if (schedule.Frequency == "Daily")
                    {
                        if (schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                        {
                            ShowReminderForm(schedule.ClassSelected, reminder);
                            reminderShown = true; // Set the flag to true to indicate that a reminder has been shown
                            break; // Exit the loop since we don't need to check other schedules anymore
                        }
                    }
                }
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }


        private void ShowReminderForm(string classSelected, string reminder)
        {
            string rem = $"Class - {classSelected}";
            ReminderForm remind = new ReminderForm(rem, reminder);

            // Invoke the ShowDialog method on the UI thread
            activeForm.Invoke(new Action(() =>
            {
                remind.ShowDialog(activeForm);
            }));
            Email em = new Email(email, rem, reminder);
            System.Media.SystemSounds.Exclamation.Play();
        }


        private DayOfWeek? GetDayOfWeekFromValue(int? value)
        {
            if (value.HasValue && value >= 1 && value <= 7)
                return (DayOfWeek)(value - 1);

            return null;
        }

        private bool IsCurrentTimeInRange(string startTime, string endTime)
        {
            DateTime currentTime = DateTime.Now;
            DateTime scheduleStartTime;
            DateTime scheduleEndTime;

            if (DateTime.TryParseExact(startTime, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out scheduleStartTime) &&
                DateTime.TryParseExact(endTime, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out scheduleEndTime))
            {
                return currentTime >= scheduleStartTime && currentTime <= scheduleEndTime;
            }
            return false;
        }

        private DateTime? ParseNullableDateTime(string dateTimeString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeString, out dateTime))
                return dateTime;

            return null;
        }
    }
}
