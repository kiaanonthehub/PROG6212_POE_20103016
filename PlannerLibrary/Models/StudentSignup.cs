using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class StudentSignup
    {
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
    }
}
