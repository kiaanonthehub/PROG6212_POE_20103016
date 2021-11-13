using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.Models;
using PlannerLibrary.DbModels;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerLibrary.Controllers
{
    public class TblModulesController : Controller
    {
        private readonly PlannerContext _context;
        PlannerContext db = new PlannerContext();

        public TblModulesController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblModules
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblModules.ToListAsync());
        }

        // GET: TblModules/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblModule tblModule = await _context.TblModules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (tblModule == null)
            {
                return NotFound();
            }

            return View(tblModule);
        }

        // GET: TblModules/Create
        public IActionResult AddModule()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddModule([Bind("ModuleId,ModuleName,ModuleCredits,ModuleClassHours")] Module module)
        {
            if (ModelState.IsValid)
            {
                // check if module exists in the database for module table
                TblModule filtModule = await Task.Run(() =>
                                        db.TblModules
                                       .Where(x => x.ModuleId == module.ModuleId || x.ModuleName == module.ModuleName)
                                       .FirstOrDefault());

                // check if module exists in the database for student_module table
                TblStudentModule filtStudentModule = await Task.Run(() =>
                                                     db.TblStudentModules
                                                     .Where(x => x.ModuleId == module.ModuleId && x.StudentNumber == Global.StudentNumber)
                                                     .FirstOrDefault());

                if (filtModule == null)
                {
                    await module.IsModuleAdded();
                }

                if (filtStudentModule == null)
                {
                    await module.IsStudentModuleAdded();
                    ViewBag.ModuleMessage = module.ModuleId + " has been added successfully.";
                }
                else
                {
                    ViewBag.ModuleMessage = module.ModuleId + " already exits. Please add a new module.";
                }

                return RedirectToAction(nameof(Index));
            }
            return View(module);
        }

        // GET: TblModules/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblModule tblModule = await _context.TblModules.FindAsync(id);
            if (tblModule == null)
            {
                return NotFound();
            }
            return View(tblModule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ModuleId,ModuleName,ModuleCredits,ModuleClassHours")] TblModule tblModule)
        {
            if (id != tblModule.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblModuleExists(tblModule.ModuleId))
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
            return View(tblModule);
        }

        // GET: TblModules/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblModule tblModule = await _context.TblModules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (tblModule == null)
            {
                return NotFound();
            }

            return View(tblModule);
        }

        // POST: TblModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            TblModule tblModule = await _context.TblModules.FindAsync(id);
            _context.TblModules.Remove(tblModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblModuleExists(string id)
        {
            return _context.TblModules.Any(e => e.ModuleId == id);
        }
    }
}
