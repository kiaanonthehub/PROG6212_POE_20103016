using System;
using System.Collections.Generic;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblStudentModule
    {
        public int StudentModuleId { get; set; }
        public int? StudentNumber { get; set; }
        public string ModuleId { get; set; }
        public decimal? ModuleSelfStudyHour { get; set; }

        public virtual TblModule Module { get; set; }
        public virtual TblStudent StudentNumberNavigation { get; set; }
    }
}
