using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooWebApp.Data;
using ZooWebApp.Models;

namespace ZooWebApp.Controllers
{
    public class SnacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SnacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Snacks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Snack.Include(s => s.Animal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Snacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack
                .Include(s => s.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // GET: Snacks/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id");
            return View();
        }

        // POST: Snacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,AnimalId")] Snack snack)
        {
            if (ModelState.IsValid)
            {
                Animal animal = await _context.Animal.FindAsync(snack.AnimalId);
                snack.Animal = animal;
                snack.Animal.Snacks.Add(snack);
                _context.Add(snack);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", snack.AnimalId);
            ;
            return View(snack);
        }

        // GET: Snacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack.FindAsync(id);
            if (snack == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", snack.AnimalId);
            return View(snack);
        }

        // POST: Snacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,AnimalId")] Snack snack)
        {
            if (id != snack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnackExists(snack.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", snack.AnimalId);
            return View(snack);
        }

        // GET: Snacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Snack == null)
            {
                return NotFound();
            }

            var snack = await _context.Snack
                .Include(s => s.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // POST: Snacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Snack == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Snack'  is null.");
            }
            var snack = await _context.Snack.FindAsync(id);
            if (snack != null)
            {
                _context.Snack.Remove(snack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnackExists(int id)
        {
            return (_context.Snack?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
