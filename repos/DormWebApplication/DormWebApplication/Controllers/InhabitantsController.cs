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
    public class InhabitantsController : Controller
    {
        private readonly DBDormContext _context;

        public InhabitantsController(DBDormContext context)
        {
            _context = context;
        }

        // GET: Inhabitants
        public async Task<IActionResult> Index(int? id, string? name)
        {
            //var dBDormContext = _context.Inhabitants.Include(i => i.Category).Include(i => i.Faculty).Include(i => i.Room);
            //return View(await dBDormContext.ToListAsync());
            if (id == null) return RedirectToAction("Categories", "Index");
            //находимо мешканців за категорією
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            var inhabitantsByCategory = _context.Inhabitants.Where(b => b.CategoryId == id).Include(b => b.Category);

            return View(await inhabitantsByCategory.ToListAsync());

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
        public IActionResult Create(/*int categoryId*/)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name"); //
            //ViewBag.CategoryId = categoryId;
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: Inhabitants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*int categoryId,*/ [Bind("Id,Name,RoomId,YearStudy,CategoryId,FacultyId,Act")] Inhabitant inhabitant)
        {
            //inhabitant.CategoryId = categoryId;
            if (ModelState.IsValid)
            {
                _context.Add(inhabitant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); //
                //return RedirectToAction("Index", "Inhabitants", new {Id= categoryId, name =_context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name});
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", inhabitant.CategoryId); //
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", inhabitant.RoomId);
            return View(inhabitant);//
            //return RedirectToAction("Index","Inhabitants", new {id = categoryId, name = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name});
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", inhabitant.CategoryId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", inhabitant.RoomId);
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", inhabitant.FacultyId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", inhabitant.RoomId);
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
