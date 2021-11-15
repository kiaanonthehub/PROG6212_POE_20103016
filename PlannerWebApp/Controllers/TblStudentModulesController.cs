using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerWebApp.Controllers
{
    public class TblStudentModulesController : Controller
    {
        private readonly PlannerContext _context;
        PlannerContext db = new PlannerContext();

        public TblStudentModulesController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblStudentModules
        public async Task<IActionResult> Index()
        {
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TblStudentModule, TblStudent> plannerContext = _context.TblStudentModules.Include(t => t.Module).Include(t => t.StudentNumberNavigation);
            return View(await plannerContext.ToListAsync());
        }

        // GET: TblStudentModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudentModule tblStudentModule = await _context.TblStudentModules
                .Include(t => t.Module)
                .Include(t => t.StudentNumberNavigation)
                .FirstOrDefaultAsync(m => m.StudentModuleId == id);
            if (tblStudentModule == null)
            {
                return NotFound();
            }

            return View(tblStudentModule);
        }

        // GET: TblStudentModules/Create
        public IActionResult SetDayReminder()
        {
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId");
            ViewData["lstDaysOfWeek"] = new SelectList(ModuleReminder.lstDaysOfWeek, "lstDaysOfWeek");
        
            return View();
        }

        // POST: TblStudentModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDayReminder([Bind("ModuleId,StudyReminderDay")] ModuleReminder moduleReminder)
        {
            if (ModelState.IsValid)
            {
                TblStudentModule filtStudentModules = db.TblStudentModules.Where(x => x.StudentNumber == Global.StudentNumber && x.ModuleId == moduleReminder.ModuleId && x.ModuleId == moduleReminder.ModuleId).First();

                filtStudentModules.StudyReminderDay = moduleReminder.StudyReminderDay;
                db.Update(filtStudentModules);

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["lstDaysOfWeek"] = new SelectList(ModuleReminder.lstDaysOfWeek, "lstDaysOfWeek");
            TblStudentModule tblStudentModule = new TblStudentModule();
            ViewData["ModuleId"] = new SelectList(_context.TblStudentModules.Where(x=> x.StudentNumber == Global.StudentNumber), "ModuleId", "ModuleId", tblStudentModule.ModuleId);
            return View(moduleReminder);
        }

        // GET: TblStudentModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudentModule tblStudentModule = await _context.TblStudentModules.FindAsync(id);
            if (tblStudentModule == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblStudentModule.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblStudentModule.StudentNumber);
            return View(tblStudentModule);
        }

        // POST: TblStudentModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentModuleId,StudentNumber,ModuleId,ModuleSelfStudyHour,StudyHoursRemains,StudyReminderDay")] TblStudentModule tblStudentModule)
        {
            if (id != tblStudentModule.StudentModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudentModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentModuleExists(tblStudentModule.StudentModuleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblStudentModule.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblStudentModule.StudentNumber);
            return View(tblStudentModule);
        }

        // GET: TblStudentModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblStudentModule tblStudentModule = await _context.TblStudentModules
                .Include(t => t.Module)
                .Include(t => t.StudentNumberNavigation)
                .FirstOrDefaultAsync(m => m.StudentModuleId == id);
            if (tblStudentModule == null)
            {
                return NotFound();
            }

            return View(tblStudentModule);
        }

        // POST: TblStudentModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TblStudentModule tblStudentModule = await _context.TblStudentModules.FindAsync(id);
            _context.TblStudentModules.Remove(tblStudentModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentModuleExists(int id)
        {
            return _context.TblStudentModules.Any(e => e.StudentModuleId == id);
        }
    }
}
