using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooWebApp.Data;
using ZooWebApp.Models;

namespace ZooWebApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            return _context.Animal != null ?
                        View(await _context.Animal.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Animal'  is null.");
        }

        // GET: Animals/ShowSearchFormAnimals
        public async Task<IActionResult> ShowSearchFormAnimals()
        {
            return View();
        }

        // POST: Animals/ShowAnimalResults
        public async Task<IActionResult> ShowAnimalResults(String Search)
        {
            return View("index", await _context.Animal.Where(j => j.Species.Contains
            (Search)).ToListAsync());
        }



        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = (await _context.Animal
                .Include(s => s.Snacks).FirstOrDefaultAsync(m => m.Id == id));
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.Y
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Species,Name")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Species = char.ToUpper(animal.Species[0]) + animal.Species.Substring(1);
                animal.Name = char.ToUpper(animal.Name[0]) + animal.Name.Substring(1);
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animals/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Species,Name")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            return View(animal);
        }

        // GET: Animals/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animal'  is null.");
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {

                _context.Animal.Remove(animal);
            }
            var snacks = _context.Snack.Where(s => s.AnimalId == animal.Id);
            _context.Snack.RemoveRange(snacks);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
