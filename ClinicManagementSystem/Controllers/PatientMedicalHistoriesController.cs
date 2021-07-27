using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Controllers
{
    public class PatientMedicalHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientMedicalHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientMedicalHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatientMedicalHistories.Include(p => p.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatientMedicalHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMedicalHistory = await _context.PatientMedicalHistories
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientMedicalHistory == null)
            {
                return NotFound();
            }

            return View(patientMedicalHistory);
        }

        // GET: PatientMedicalHistories/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName");
            return View();
        }

        // POST: PatientMedicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,Description,DataEntry")] PatientMedicalHistory patientMedicalHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientMedicalHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName", patientMedicalHistory.PatientId);
            return View(patientMedicalHistory);
        }

        // GET: PatientMedicalHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMedicalHistory = await _context.PatientMedicalHistories.FindAsync(id);
            if (patientMedicalHistory == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", patientMedicalHistory.PatientId);
            return View(patientMedicalHistory);
        }

        // POST: PatientMedicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PatientId,Description,DataEntry")] PatientMedicalHistory patientMedicalHistory)
        {
            if (id != patientMedicalHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientMedicalHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientMedicalHistoryExists(patientMedicalHistory.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PatientName", patientMedicalHistory.PatientId);
            return View(patientMedicalHistory);
        }

        // GET: PatientMedicalHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMedicalHistory = await _context.PatientMedicalHistories
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientMedicalHistory == null)
            {
                return NotFound();
            }

            return View(patientMedicalHistory);
        }

        // POST: PatientMedicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var patientMedicalHistory = await _context.PatientMedicalHistories.FindAsync(id);
            _context.PatientMedicalHistories.Remove(patientMedicalHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientMedicalHistoryExists(long id)
        {
            return _context.PatientMedicalHistories.Any(e => e.Id == id);
        }
    }
}
