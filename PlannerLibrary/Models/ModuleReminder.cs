using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class ModuleReminder
    {
        public static List<string> lstDaysOfWeek = new List<string>
        {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };

        public string ModuleId { get; set; }
        public string StudyReminderDay { get; set; }
    }
}
