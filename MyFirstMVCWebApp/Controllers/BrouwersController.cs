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
        public IActionResult Index()
        {
            List<Brouwers> brouwers = _context.Brouwers.ToList();
            return View(brouwers);
        }
    }
}
