using System;
using System.Collections.Generic;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblTrackStudy
    {
        public int TrackStudiesId { get; set; }
        public decimal? HoursWorked { get; set; }
        public DateTime? DateWorked { get; set; }
        public int? WeekNumber { get; set; }
        public int? StudentNumber { get; set; }
        public string ModuleId { get; set; }

        public virtual TblModule Module { get; set; }
        public virtual TblStudent StudentNumberNavigation { get; set; }
    }
}
