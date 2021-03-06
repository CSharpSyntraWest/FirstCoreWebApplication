﻿using Microsoft.AspNetCore.Mvc;
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

        //standaard [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] // Als de eindgebruiker op de Create button (submit) klikt in de browser-pagina
        public IActionResult Create(Brouwers brouwer)
        {
            if (brouwer == null)
            {
                return BadRequest();
            }
            _context.Brouwers.Add(brouwer);
            _context.SaveChanges();

           return View(brouwer);

        }

        //[HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); //Error 404 -- Not found
            }
            Brouwers brouwer = _context.Brouwers.Find(id);//Find gaat automatisch op PK waarde zoeken
            if (brouwer == null)
            {
                return NotFound(); //Error 404 -- Not found
            }
            return View(brouwer);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Brouwers brouwer)
        {
            if (id != brouwer.BrouwerNr)
            {
                return BadRequest();
            }
            bool bestaatBrouwer = _context.Brouwers.Any(b => b.BrouwerNr == id);
            if (!bestaatBrouwer)
            {
                return NotFound();
            }
            _context.Brouwers.Update(brouwer);
            _context.SaveChanges();

            return View(brouwer);           
        }
    }
}
