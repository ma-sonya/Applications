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
    public class FloorsController : Controller
    {
        private readonly DBDormContext _context;

        public FloorsController(DBDormContext context)
        {
            _context = context;
        }

        // GET: Floors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Floors.ToListAsync());
        }

        // GET: Floors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floor = await _context.Floors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (floor == null)
            {
                return NotFound();
            }

            return View(floor);
        }

        // GET: Floors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Floors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FloorNumber,ChiefId,IsKitchenOpen")] Floor floor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(floor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(floor);
        }

        // GET: Floors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            var room = _context.Rooms.Where(r => r.FloorNumber == id).Select(s => s.Id).ToList();
            var students = _context.Inhabitants.Where(s => room.Contains(s.RoomId)).ToList();

            ViewBag.Students = new SelectList(students, "Name", "Name");

            return View(floor);
        }

        // POST: Floors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FloorNumber,ChiefId,IsKitchenOpen")] Floor floor)
        {
            if (id != floor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(floor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FloorExists(floor.Id))
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
            return View(floor);
        }

        // GET: Floors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var floor = await _context.Floors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (floor == null)
            {
                return NotFound();
            }

            return View(floor);
        }

        // POST: Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var floor = await _context.Floors.FindAsync(id);
            var rooms = _context.Rooms.Where(r => r.FloorNumber == id).ToList();

            if (rooms != null)
            {
                if (rooms.Count() != 0)
                {
                    foreach (var room in rooms)
                    {
                        var inhabs = _context.Inhabitants.Where(i => i.RoomId == room.Id).ToList();
                        var furnts = _context.Furnitures.Where(f => f.RoomId == room.Id).ToList();

                        if (furnts != null)
                        {
                            if (furnts.Count() != 0)
                            {
                                foreach (var fur in furnts)
                                {
                                    fur.RoomId = null;
                                    _context.SaveChanges();
                                }
                            }
                        }

                        if (inhabs != null)
                        {
                            if (inhabs.Count() != 0)
                            {
                                foreach (var inhab in inhabs)
                                {
                                    _context.Remove(inhab);
                                }
                            }
                        }

                        _context.Remove(room);
                    }
                }
            }

            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FloorExists(int id)
        {
            return _context.Floors.Any(e => e.Id == id);
        }
    }
}
