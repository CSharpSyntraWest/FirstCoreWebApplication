using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        //CRUD methodes methodes toevoegen voor ToDo's(C = Create/ R = Read / U = Update / D = Delete)
        private ToDoDbContext _context;
        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        //GET: api/todo
        [HttpGet]
        public ActionResult<IEnumerable<ToDo>> GetToDos()
        {
            return Ok(_context.ToDos.ToList());
        }
        //GET: api/todo/1
        [HttpGet("{id}")]
        public ActionResult<ToDo> GetToDo(int id)
        {
            ToDo toDo = _context.ToDos.Find(id);// _context.ToDos.Where(t => t.Id == id).FirstOrDefault();
            if (toDo == null)
            {
                return NotFound();//Om status code 404 Not found terug te geven
            }
            return Ok(toDo); // Om status code 200 OK terug te geven

        }
        //POST: api/todo (nieuwe ToDo aanmaken)
        [HttpPost]
        public ActionResult<ToDo> PostToDo(ToDo toDo)
        {
            _context.ToDos.Add(toDo);
            _context.SaveChanges();

            return CreatedAtAction("PostToDo", toDo);
        }
        //PUT: api/todo/1 (bestaande ToDo wijzigen)
        [HttpPut("{id}")]
        public IActionResult PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.ToDos.Update(toDo);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Bestaat(toDo.Id))
                {
                    return NotFound(); // Status code 404 - Not Found (ondertussen is de ToDo al door iemand anders verwijderd)
                }
                else
                {
                    throw;
                }

            }
            return Ok(toDo); // return NoContent(); -- Geef status code 204 terug (aanpassing gelukt, maar geen body in HTTP response)
        }

        //DELETE api/ToDo/1
        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            ToDo toDo = _context.ToDos.Find(id);
            if (toDo == null)// of hier kan je ook Bestaat methode gebruiken
            {
                return NotFound();
            }
            _context.ToDos.Remove(toDo);
            _context.SaveChanges();
            return Ok(toDo);
        }

        private bool Bestaat(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
