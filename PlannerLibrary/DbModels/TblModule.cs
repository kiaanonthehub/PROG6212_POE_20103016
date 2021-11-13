using System;
using System.Collections.Generic;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblModule
    {
        public TblModule()
        {
            TblStudentModules = new HashSet<TblStudentModule>();
            TblTrackStudies = new HashSet<TblTrackStudy>();
        }

        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleCredits { get; set; }
        public int ModuleClassHours { get; set; }

        public virtual ICollection<TblStudentModule> TblStudentModules { get; set; }
        public virtual ICollection<TblTrackStudy> TblTrackStudies { get; set; }
    }
}
