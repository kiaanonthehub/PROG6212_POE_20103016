using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLibrary.Controllers
{
    public class TblTrackStudiesController : Controller
    {
        private readonly PlannerContext _context;

        public TblTrackStudiesController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblTrackStudies
        public async Task<IActionResult> Index()
        {
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TblTrackStudy, TblStudent> plannerContext = _context.TblTrackStudies.Include(t => t.Module).Include(t => t.StudentNumberNavigation);
            return View(await plannerContext.ToListAsync());
        }

        // GET: TblTrackStudies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblTrackStudy tblTrackStudy = await _context.TblTrackStudies
                .Include(t => t.Module)
                .Include(t => t.StudentNumberNavigation)
                .FirstOrDefaultAsync(m => m.TrackStudiesId == id);
            if (tblTrackStudy == null)
            {
                return NotFound();
            }

            return View(tblTrackStudy);
        }

        // GET: TblTrackStudies/Create
        public IActionResult TrackStudies()
        {
            ViewData["ModuleId"] = new SelectList(_context.TblStudentModules.Where(x=>x.StudentNumber == Global.StudentNumber), "ModuleId", "ModuleId");
            return View();
        }

        // POST: TblTrackStudies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrackStudies([Bind("HoursWorked,DateWorked,ModuleId")] TrackStudy tracker)
        {
            TblTrackStudy tblTrackStudy = new TblTrackStudy();
            if (ModelState.IsValid)
            {
                tblTrackStudy.HoursWorked = tracker.HoursWorked;
                tblTrackStudy.DateWorked = Convert.ToDateTime(tracker.DateWorked);
                tblTrackStudy.WeekNumber = Util.GetCurrentWeek(Convert.ToDateTime(tracker.DateWorked));
                tblTrackStudy.StudentNumber = Global.StudentNumber;
                tblTrackStudy.ModuleId = tracker.ModuleId;

                _context.Add(tblTrackStudy);
                await _context.SaveChangesAsync();

                ViewBag.RemHours =  tracker.IsStudyHoursTracked(tracker.ModuleId);

                return RedirectToAction(nameof(TrackStudies));
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblTrackStudy.ModuleId);
            return View(tracker);
        }

        // GET: TblTrackStudies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblTrackStudy tblTrackStudy = await _context.TblTrackStudies.FindAsync(id);
            if (tblTrackStudy == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblTrackStudy.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblTrackStudy.StudentNumber);
            return View(tblTrackStudy);
        }

        // POST: TblTrackStudies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackStudiesId,HoursWorked,DateWorked,WeekNumber,StudentNumber,ModuleId")] TblTrackStudy tblTrackStudy)
        {
            if (id != tblTrackStudy.TrackStudiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTrackStudy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTrackStudyExists(tblTrackStudy.TrackStudiesId))
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
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblTrackStudy.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblTrackStudy.StudentNumber);
            return View(tblTrackStudy);
        }

        // GET: TblTrackStudies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblTrackStudy tblTrackStudy = await _context.TblTrackStudies
                .Include(t => t.Module)
                .Include(t => t.StudentNumberNavigation)
                .FirstOrDefaultAsync(m => m.TrackStudiesId == id);
            if (tblTrackStudy == null)
            {
                return NotFound();
            }

            return View(tblTrackStudy);
        }

        // POST: TblTrackStudies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TblTrackStudy tblTrackStudy = await _context.TblTrackStudies.FindAsync(id);
            _context.TblTrackStudies.Remove(tblTrackStudy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTrackStudyExists(int id)
        {
            return _context.TblTrackStudies.Any(e => e.TrackStudiesId == id);
        }
    }
}
