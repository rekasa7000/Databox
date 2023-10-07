using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace Databox
{
    class AppointmentChecker
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

        public AppointmentChecker(Form form)
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
            string query = "SELECT schedule.schedule_id, CONCAT(student.studFirstname,' ', student.studLastname) AS classSelected, schedule.schedName, schedule.frequency, schedule.startTime, schedule.endTime, schedule.dayOfWeek, schedule.dayOfMonth, schedule.monthOfYear, schedule.scheduleDate " +
                           "FROM schedule " +
                           "JOIN student ON student.student_id = schedule.student_id " +
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
            while (!cancellationToken.IsCancellationRequested)
            {
                FetchSchedules();

                foreach (var schedule in schedules)
                {
                    string reminder = $"Upcoming appointment: {schedule.ScheduleName} is happening now!" +
                        $"You have appointment right now with {schedule.ClassSelected}";

                    if (schedule.Frequency == "Weekly")
                    {
                        DayOfWeek? currentDayOfWeek = DateTime.Now.DayOfWeek;
                        DayOfWeek? scheduleDayOfWeek = GetDayOfWeekFromValue(schedule.DayOfWeek + 1);
                        if (currentDayOfWeek == scheduleDayOfWeek && schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                        {
                            ShowReminderForm(schedule.ClassSelected, reminder);
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
                        }
                    }
                    else if (schedule.Frequency == "Daily")
                    {
                        if (schedule.StartTime == DateTime.Now.ToString("HH:mm:00"))
                        {
                            ShowReminderForm(schedule.ClassSelected, reminder);
                        }
                    }
                }
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
        private void ShowReminderForm(string classSelected, string reminder)
        {
            string rem = $"Appointment - {classSelected}";
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

        private DateTime? ParseNullableDateTime(string dateTimeString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeString, out dateTime))
                return dateTime;

            return null;
        }
    }
}
