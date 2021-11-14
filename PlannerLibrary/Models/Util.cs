using System;

namespace PlannerLibrary.Models
{

    public static class Util
    {
        /// <summary>
        /// method to get the current week dependent on the start date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetCurrentWeek(DateTime date)
        {
            return (date - Convert.ToDateTime(Global.StartDate)).Days / 7 + 1;
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

            return string.Format("{0} Remaining Self-Study Hours : " +
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
