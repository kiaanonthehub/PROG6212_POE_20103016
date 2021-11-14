﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;

namespace PlannerWebApp.Controllers
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
            var plannerContext = _context.TblTrackStudies.Include(t => t.Module).Include(t => t.StudentNumberNavigation);
            return View(await plannerContext.ToListAsync());
        }

        // GET: TblTrackStudies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTrackStudy = await _context.TblTrackStudies
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
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId");
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackStudiesId,HoursWorked,DateWorked,WeekNumber,StudentNumber,ModuleId")] TblTrackStudy tblTrackStudy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTrackStudy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblTrackStudy.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblTrackStudy.StudentNumber);
            return View(tblTrackStudy);
        }

        // GET: TblTrackStudies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTrackStudy = await _context.TblTrackStudies.FindAsync(id);
            if (tblTrackStudy == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.TblModules, "ModuleId", "ModuleId", tblTrackStudy.ModuleId);
            ViewData["StudentNumber"] = new SelectList(_context.TblStudents, "StudentNumber", "StudentEmail", tblTrackStudy.StudentNumber);
            return View(tblTrackStudy);
        }

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

            var tblTrackStudy = await _context.TblTrackStudies
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
            var tblTrackStudy = await _context.TblTrackStudies.FindAsync(id);
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
