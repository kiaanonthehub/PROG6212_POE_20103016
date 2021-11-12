using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblStudent
    {
        public TblStudent()
        {
            TblStudentModules = new HashSet<TblStudentModule>();
            TblTrackStudies = new HashSet<TblTrackStudy>();
        }

      [Required]
        public int StudentNumber { get; set; }

        [Required]
        public string StudentName { get; set; }
        [Required]
        public string StudentSurname { get; set; }
        [Required]

        public string StudentEmail { get; set; }
        [Required]
        public string StudentHashPassword { get; set; }
        [Required]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        public int NumberOfWeeks { get; set; }

        public virtual ICollection<TblStudentModule> TblStudentModules { get; set; }
        public virtual ICollection<TblTrackStudy> TblTrackStudies { get; set; }
    }
}
