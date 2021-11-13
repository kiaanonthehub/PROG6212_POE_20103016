
using PlannerLibrary.DbModels;
using System;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class Module
    {
        PlannerContext db = new PlannerContext();
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleCredits { get; set; }
        public int ModuleClassHours { get; set; }
        public decimal? ModuleSelfStudyHour { get; set; }

        public double SelfStudyHours()
        {
            if (Global.NoOfWeeks <= 0)
            {
                return 0; // redirect to custom error page
            }
            else
            {
                return (ModuleCredits * 10 / Convert.ToInt32(Global.NoOfWeeks)) - ModuleClassHours;
            }
        }

        public async Task<bool> IsModuleAdded()
        {
            TblModule tblModule = new TblModule();
            tblModule.ModuleId = ModuleId;
            tblModule.ModuleName = ModuleName;
            tblModule.ModuleCredits = (int)ModuleCredits;
            tblModule.ModuleClassHours = (int)ModuleClassHours;
            //tblModule.ModuleSelfStudyHour = (decimal)selfStudyHours;

            await Task.Run(() => db.AddAsync(tblModule));
            await Task.Run(() => db.SaveChangesAsync());

            return true;
        }


        public async Task<bool> IsStudentModuleAdded()
        {
            TblStudentModule tblStudentModule = new TblStudentModule();
            tblStudentModule.StudentNumber = Global.StudentNumber;
            tblStudentModule.ModuleId = ModuleId;
            tblStudentModule.ModuleSelfStudyHour = Convert.ToDecimal(SelfStudyHours());

            await Task.Run(() => db.AddAsync(tblStudentModule));
            await Task.Run(() => db.SaveChangesAsync());

            return true;
        }


    }
}
