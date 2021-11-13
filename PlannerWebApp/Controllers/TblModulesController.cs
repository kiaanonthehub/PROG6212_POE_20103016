using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;

namespace PlannerLibrary.Controllers
{
    public class TblModulesController : Controller
    {
        private readonly PlannerContext _context;

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

            var tblModule = await _context.TblModules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (tblModule == null)
            {
                return NotFound();
            }

            return View(tblModule);
        }

        // GET: TblModules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleName,ModuleCredits,ModuleClassHours")] TblModule tblModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblModule);
        }

        // GET: TblModules/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblModule = await _context.TblModules.FindAsync(id);
            if (tblModule == null)
            {
                return NotFound();
            }
            return View(tblModule);
        }

        // POST: TblModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var tblModule = await _context.TblModules
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
            var tblModule = await _context.TblModules.FindAsync(id);
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
