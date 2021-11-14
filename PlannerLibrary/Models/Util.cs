using System;

namespace PlannerLibrary.Models
{

    public static class Util
    {
        public static int studentNumber;
        public static DateTime startDate;
        public static int noOfWeeks;
        public static int currentWeekNo;
        public static string userEmail;
        public static int OTPPin;

        /// <summary>
        /// method to get the current week dependent on the start date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetCurrentWeek(DateTime date)
        {
            return (date - startDate).Days / 7 + 1;
        }

        public static DateTime LastDayOfWeek(DateTime dt)
        {
            System.Globalization.CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            int diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-diff).Date.AddDays(7);
        }

        public static string TimeFormatDisplay(double time, string module)
        {
            TimeSpan timeSpan = TimeSpan.FromHours(time);
            int hh = timeSpan.Hours;
            int mm = timeSpan.Minutes;

            return string.Format("{0} remaining self-study hours : " +
                "\n {1} hours, {2} minutes", module, hh, mm);
        }

        public static string UITimeFormat(double time)
        {
            TimeSpan timeSpan = TimeSpan.FromHours(time);
            int hh = timeSpan.Hours;
            int mm = timeSpan.Minutes;

            return string.Format("{0} hours, {1} minutes", hh, mm);
        }

    }
}
