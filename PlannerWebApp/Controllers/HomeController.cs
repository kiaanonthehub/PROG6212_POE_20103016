using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace PlannerLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PlannerContext db = new PlannerContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string Today = null, Module = null, StudyReminder = null;

            StudyReminder = db.TblStudentModules.Where(x => x.StudentNumber == Global.StudentNumber).Select(x => x.StudyReminderDay).First();

            if (StudyReminder != null)
            {
                Today = DateTime.Now.DayOfWeek.ToString();
                Module = db.TblStudentModules.Where(x => x.StudentNumber == Global.StudentNumber).Select(x => x.ModuleId).First();



                if (Today == StudyReminder)
                {
                    Global.StudyReminder = true;
                    ViewBag.StudyReminder = String.Format("Reminder! Study {0} for today {1}", Module, Today);
                    Global.StudyReminder = false;
                }
                else
                {
                    Global.StudyReminder = false;
                }

            }
            return View();
        }

        public IActionResult ErrorSignIn()
        {
            return View();
        }

        public IActionResult NoConnection()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
