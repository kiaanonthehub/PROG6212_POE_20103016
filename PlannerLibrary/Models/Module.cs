using PlannerLibrary.DbModels;

namespace PlannerLibrary.Models
{
    public class Module
    {
        PlannerContext db = new PlannerContext();
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleCredits { get; set; }
        public int ModuleClassHours { get; set; }

        public double SelfStudyHours()
        {
            if (Global.NoOfWeeks <= 0)
            {
                return 0; // redirect to custom error page
            }
            else
            {
                return (ModuleCredits * 10 / Global.NoOfWeeks) - ModuleClassHours;
            }
        }

    }
}
