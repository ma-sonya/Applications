using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormWebApplication;
using Microsoft.AspNetCore.Authorization;

namespace DormWebApplication.Controllers
{
    [Authorize(Roles ="admin, user")]
    public class InhabitantsController : Controller
    {
        private readonly DBDormContext _context;

        public InhabitantsController(DBDormContext context)
        {
            _context = context;
        }

        // GET: Inhabitants
        public async Task<IActionResult> Index()
        {
            var dBDormContext = _context.Inhabitants.Include(i => i.Category).Include(i => i.Faculty).Include(i => i.Room);
            return View(await dBDormContext.ToListAsync());
        }

        // GET: Inhabitants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inhabitant = await _context.Inhabitants
                .Include(i => i.Category)
                .Include(i => i.Faculty)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inhabitant == null)
            {
                return NotFound();
            }

            return View(inhabitant);
        }

        // GET: Inhabitants/Create
        public IActionResult Create()
        {
            var cats = _context.Categories.Where(c => c.Name != "Староста").Where(c => c.Name != "Вахта");
            var rooms = _context.Rooms.Where(r => r.CountPlace > r.Inhabitants.Count());

            ViewData["CategoryId"] = new SelectList(cats, "Id", "Name");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Inhabitants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RoomId,YearStudy,CategoryId,FacultyId,Act")] Inhabitant inhabitant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inhabitant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", inhabitant.CategoryId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", inhabitant.RoomId);
            return View(inhabitant);
        }

        // GET: Inhabitants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inhabitant = await _context.Inhabitants.FindAsync(id);
            if (inhabitant == null)
            {
                return NotFound();
            }

            var cats = _context.Categories.Where(c => c.Name != "Вахта");
            var rooms = _context.Rooms.Where(r => r.CountPlace > r.Inhabitants.Count());

            ViewData["CategoryId"] = new SelectList(cats, "Id", "Name", inhabitant.CategoryId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(rooms, "Id", "RoomNumber", inhabitant.RoomId);
            return View(inhabitant);
        }

        // POST: Inhabitants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RoomId,YearStudy,CategoryId,FacultyId,Act")] Inhabitant inhabitant)
        {
            if (id != inhabitant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inhabitant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InhabitantExists(inhabitant.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", inhabitant.CategoryId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", inhabitant.RoomId);
            return View(inhabitant);
        }

        // GET: Inhabitants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inhabitant = await _context.Inhabitants
                .Include(i => i.Category)
                .Include(i => i.Faculty)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inhabitant == null)
            {
                return NotFound();
            }

            return View(inhabitant);
        }

        // POST: Inhabitants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inhabitant = await _context.Inhabitants.FindAsync(id);
            var isChief = _context.Floors.Where(f => f.ChiefId == inhabitant.Name).ToList();

            if (isChief != null)
            {
                if (isChief.Count() != 0)
                {
                    foreach (var item in isChief)
                    {
                        item.ChiefId = "";
                        _context.SaveChanges();
                    }
                }
            }

            _context.Inhabitants.Remove(inhabitant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InhabitantExists(int id)
        {
            return _context.Inhabitants.Any(e => e.Id == id);
        }
    }
}
