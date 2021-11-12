using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class StudentModule
    {
        public int StudentNumber { get; set; }
        public string ModuleId { get; set; }
        public decimal? ModuleSelfStudyHour { get; set; }
    }
}
