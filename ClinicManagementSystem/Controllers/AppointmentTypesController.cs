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
    public class AppointmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppointmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppointmentTypes.ToListAsync());
        }

        // GET: AppointmentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentType = await _context.AppointmentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentType == null)
            {
                return NotFound();
            }

            return View(appointmentType);
        }

        // GET: AppointmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Price")] AppointmentType appointmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentType);
        }

        // GET: AppointmentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentType = await _context.AppointmentTypes.FindAsync(id);
            if (appointmentType == null)
            {
                return NotFound();
            }
            return View(appointmentType);
        }

        // POST: AppointmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,Price")] AppointmentType appointmentType)
        {
            if (id != appointmentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentTypeExists(appointmentType.Id))
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
            return View(appointmentType);
        }

        // GET: AppointmentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentType = await _context.AppointmentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentType == null)
            {
                return NotFound();
            }

            return View(appointmentType);
        }

        // POST: AppointmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appointmentType = await _context.AppointmentTypes.FindAsync(id);
            _context.AppointmentTypes.Remove(appointmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentTypeExists(long id)
        {
            return _context.AppointmentTypes.Any(e => e.Id == id);
        }
    }
}
