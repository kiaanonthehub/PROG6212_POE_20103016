using PlannerLibrary.DbModels;
using System;

namespace PlannerLibrary.Models
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentEmail { get; set; }
        public string StudentHashPassword { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfWeeks { get; set; }
    }
}
