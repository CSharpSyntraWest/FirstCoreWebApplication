using Microsoft.AspNetCore.Mvc;
using MyFirstMVCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVCWebApp.Controllers
{
    public class BrouwersController : Controller
    {
        private readonly BierenDbContext _context;

        public BrouwersController(BierenDbContext context)
        {
            _context = context;
        }
       //standaard [HttpGet]
        public IActionResult Index()
        {
            List<Brouwers> brouwers = _context.Brouwers.ToList();
            return View(brouwers);
        }
        //standaard [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); //404 - Not found status teruggeven
            }
            Brouwers brouwer = _context.Brouwers.Where(b => b.BrouwerNr == id).SingleOrDefault();
            if (brouwer == null)
            {
                return NotFound(); //404 - Not found status teruggeven
            }
            return View(brouwer);
        }
    }
}
