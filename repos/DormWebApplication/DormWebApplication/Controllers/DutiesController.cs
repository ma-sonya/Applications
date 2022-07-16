using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormWebApplication;

namespace DormWebApplication.Controllers
{
    public class DutiesController : Controller
    {
        private readonly DBDormContext _context;

        public DutiesController(DBDormContext context)
        {
            _context = context;
        }

        // GET: Duties
        public async Task<IActionResult> Index()
        {
            var dBDormContext = _context.Duties.Include(d => d.Category);
            return View(await dBDormContext.ToListAsync());
        }

        // GET: Duties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        // GET: Duties/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Duties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Coworker,Workday,CategoryId,Name")] Duty duty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", duty.CategoryId);
            return View(duty);
        }

        // GET: Duties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties.FindAsync(id);
            if (duty == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", duty.CategoryId);
            return View(duty);
        }

        // POST: Duties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Coworker,Workday,CategoryId,Name")] Duty duty)
        {
            if (id != duty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyExists(duty.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", duty.CategoryId);
            return View(duty);
        }

        // GET: Duties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        // POST: Duties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duty = await _context.Duties.FindAsync(id);
            _context.Duties.Remove(duty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyExists(int id)
        {
            return _context.Duties.Any(e => e.Id == id);
        }
    }
}
