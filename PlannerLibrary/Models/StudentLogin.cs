using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class StudentLogin
    {
        [Required]
        public int StudentNumber { get; set; }
        [Required]
        public string StudentHashPassword { get; set; }
    }
}
