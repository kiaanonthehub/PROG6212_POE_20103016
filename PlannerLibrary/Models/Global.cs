using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PlannerLibrary.Models
{
    public static class Global
    {
        public static int StudentNumber;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public static DateTime? StartDate;
        public static string HoursRemains;
        public static string DisplayRemHours;
        public static string StudentEmail;
        public static bool IsLoggedIn = false;
        public static bool StudyReminder = false;
        public static int? NoOfWeeks;
        public static int currentWeekNo;
        public static int OneTimePin;
        public static string StudentName;
        public static bool NetworkConn = false;  
        public static string AzureConnectionString = "Server=tcp:svr20103016.database.windows.net,1433;Initial Catalog=Planner;Persist Security Info=False;User ID=admin20103016;Password=Password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        

        //public enum PasswordScore
        //{
        //    Blank = 0,
        //    VeryWeak = 1,
        //    Weak = 2,
        //    Medium = 3,
        //    Strong = 4,
        //    VeryStrong = 5
        //}

        //    public static PasswordScore CheckStrength(string password)
        //    {
        //        int score = 0;

        //        if (password.Length < 1)
        //        {
        //            return PasswordScore.Blank;
        //        }

        //        if (password.Length < 4)
        //        {
        //            return PasswordScore.VeryWeak;
        //        }

        //        if (password.Length >= 8)
        //        {
        //            score++;
        //        }

        //        if (password.Length >= 12)
        //        {
        //            score++;
        //        }

        //        if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
        //        {
        //            score++;
        //        }

        //        if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
        //          Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
        //        {
        //            score++;
        //        }

        //        if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
        //        {
        //            score++;
        //        }

        //        return (PasswordScore)score;
        //    }
    }
}
