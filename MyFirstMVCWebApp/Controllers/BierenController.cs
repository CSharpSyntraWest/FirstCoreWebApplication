using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMVCWebApp.Models;

namespace MyFirstMVCWebApp.Controllers
{
    public class BierenController : Controller
    {
        private readonly BierenDbContext _context;

        public BierenController(BierenDbContext context)
        {
            _context = context;
        }

        // GET: Bieren
        public async Task<IActionResult> Index()
        {
            var bierenDbContext = _context.Bieren.Include(b => b.BrouwerNrNavigation).Include(b => b.SoortNrNavigation);
            return View(await bierenDbContext.ToListAsync());
        }

        // GET: Bieren/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bieren = await _context.Bieren
                .Include(b => b.BrouwerNrNavigation)
                .Include(b => b.SoortNrNavigation)
                .FirstOrDefaultAsync(m => m.BierNr == id);
            if (bieren == null)
            {
                return NotFound();
            }

            return View(bieren);
        }

        // GET: Bieren/Create
        public IActionResult Create()
        {
            ViewData["BrouwerNr"] = new SelectList(_context.Brouwers, "BrouwerNr", "BrouwerNr");
            ViewData["SoortNr"] = new SelectList(_context.Soorten, "SoortNr", "SoortNr");
            return View();
        }

        // POST: Bieren/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bieren bieren)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bieren);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrouwerNr"] = new SelectList(_context.Brouwers, "BrouwerNr", "BrouwerNr", bieren.BrouwerNr);
            ViewData["SoortNr"] = new SelectList(_context.Soorten, "SoortNr", "SoortNr", bieren.SoortNr);
            return View(bieren);
        }

        // GET: Bieren/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bieren = await _context.Bieren.FindAsync(id);
            if (bieren == null)
            {
                return NotFound();
            }
            ViewData["BrouwerNr"] = new SelectList(_context.Brouwers, "BrouwerNr", "BrouwerNr", bieren.BrouwerNr);
            ViewData["SoortNr"] = new SelectList(_context.Soorten, "SoortNr", "SoortNr", bieren.SoortNr);
            return View(bieren);
        }

        // POST: Bieren/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bieren bieren)
        {
            if (id != bieren.BierNr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bieren);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BierenExists(bieren.BierNr))
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
            ViewData["BrouwerNr"] = new SelectList(_context.Brouwers, "BrouwerNr", "BrouwerNr", bieren.BrouwerNr);
            ViewData["SoortNr"] = new SelectList(_context.Soorten, "SoortNr", "SoortNr", bieren.SoortNr);
            return View(bieren);
        }

        // GET: Bieren/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bieren = await _context.Bieren
                .Include(b => b.BrouwerNrNavigation)
                .Include(b => b.SoortNrNavigation)
                .FirstOrDefaultAsync(m => m.BierNr == id);
            if (bieren == null)
            {
                return NotFound();
            }

            return View(bieren);
        }

        // POST: Bieren/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bieren = await _context.Bieren.FindAsync(id);
            _context.Bieren.Remove(bieren);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BierenExists(int id)
        {
            return _context.Bieren.Any(e => e.BierNr == id);
        }
    }
}
