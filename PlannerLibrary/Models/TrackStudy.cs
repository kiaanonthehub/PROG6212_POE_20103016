using PlannerLibrary.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLibrary.Models
{
    public class TrackStudy
    {
        PlannerContext db = new PlannerContext();

        public TrackStudy()
        {
            WeekNo = Util.GetCurrentWeek(Convert.ToDateTime(DateWorked));
        }

        public decimal? HoursWorked { get; set; }
        public DateTime? DateWorked { get; set; }
        public string ModuleId { get; set; }

        // properties for calculation
        public int WeekNo { get; set; }
        public int RemainingWeeks { get; set; }
        public decimal? DistributeHours { get; set; }
        public decimal? SelfStudyHours { get; set; }
        public decimal? TotalModuleIncompleteHours { get; set; }
        public decimal? ModuleIncompleteHours { get; set; }
        public double TotalHoursWorked { get; set; }
        public double RemainingHours { get; set; }


        public virtual string IsStudyHoursTracked(string mc)
        {
            // calculate the total required self studied hours that have not been completed 
            TotalModuleIncompleteHours = Convert.ToDecimal(db.TblStudentModules.Where(x => x.StudentNumber == Global.StudentNumber && x.ModuleId == mc).Select(x => x.ModuleSelfStudyHour * (Global.currentWeekNo - 1)).First());

            // check id noOfWeeks and currentWeekNo = 0 
            if (Global.currentWeekNo == 0)
            {
                Global.currentWeekNo = Util.GetCurrentWeek(DateTime.Now);
            }

            // calculate the remaining weeks weeks
            RemainingWeeks = Convert.ToInt32(Global.NoOfWeeks) - Global.currentWeekNo;

            // distribute hours per remaining weeks until semester ends
            DistributeHours = TotalModuleIncompleteHours / Convert.ToDecimal(RemainingWeeks);

            ModuleIncompleteHours = Convert.ToDecimal(db.TblStudentModules.Where(x => x.StudentNumber == Global.StudentNumber && x.ModuleId == mc).Select(x => x.ModuleSelfStudyHour).First());

            // adds the distributed incompleted hours per week to the required incomplete module self study hours
            ModuleIncompleteHours = ModuleIncompleteHours + DistributeHours;

            if (ModuleIncompleteHours <= 0)
            {
                // displays a message to the student that the self study hours has been complete
                //MessageBox.Show("Congratulations you have completed all your study hours", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);

                // returns null
                return null;
            }
            else
            {
                // sums up all the hours spent studying on a module
                List<decimal?> filtHours = db.TblTrackStudies.Where(x => x.StudentNumber == Global.StudentNumber && x.ModuleId == mc && x.DateWorked == DateWorked).Select(x => x.HoursWorked).ToList();
                filtHours.ForEach(x => TotalHoursWorked += Convert.ToDouble(x.Value));

                // calculates the remaining self study hours left for the current week
                RemainingHours = Convert.ToDouble(ModuleIncompleteHours) - TotalHoursWorked;

                // returns a converted display
                return Util.TimeFormatDisplay(RemainingHours, mc).ToString();
            }
        }

    }
}
