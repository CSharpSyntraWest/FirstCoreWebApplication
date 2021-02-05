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
    public class SoortenController : Controller
    {
        private readonly BierenDbContext _context;

        public SoortenController(BierenDbContext context)
        {
            _context = context;
        }

        // GET: Soorten
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soorten.ToListAsync());
        }

        // GET: Soorten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soorten = await _context.Soorten
                .FirstOrDefaultAsync(m => m.SoortNr == id);
            if (soorten == null)
            {
                return NotFound();
            }

            return View(soorten);
        }

        // GET: Soorten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soorten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoortNr,Soort")] Soorten soorten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soorten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soorten);
        }

        // GET: Soorten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soorten = await _context.Soorten.FindAsync(id);
            if (soorten == null)
            {
                return NotFound();
            }
            return View(soorten);
        }

        // POST: Soorten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoortNr,Soort")] Soorten soorten)
        {
            if (id != soorten.SoortNr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soorten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoortenExists(soorten.SoortNr))
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
            return View(soorten);
        }

        // GET: Soorten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soorten = await _context.Soorten
                .FirstOrDefaultAsync(m => m.SoortNr == id);
            if (soorten == null)
            {
                return NotFound();
            }

            return View(soorten);
        }

        // POST: Soorten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soorten = await _context.Soorten.FindAsync(id);
            _context.Soorten.Remove(soorten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoortenExists(int id)
        {
            return _context.Soorten.Any(e => e.SoortNr == id);
        }
    }
}
