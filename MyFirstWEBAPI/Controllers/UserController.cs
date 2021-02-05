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
    public class UserController : ControllerBase
    {
        private ToDoDbContext _context;
        public UserController(ToDoDbContext context)
        {
            _context = context;  
        }
        //GET api/User (de lijst van alles users teruggeven)
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_context.Users.Include(u => u.ToDos).ToList()); //OK = 200 status code 
        }
        //GET api/user/1 (de gegevens van één user opvragen via Id)
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _context.Users.Find(id);// _context.Users.Where(t => t.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();//Om status code 404 Not found terug te geven
            }
            return Ok(user); // Om status code 200 OK terug te geven

        }
        //[HttpGet("{id}")]
        //public User GetUserMetId(int id)
        //{
        //    User user = _context.Users.Find(id);
        //    return user;
        //}
        //PUT api/user/1
        [HttpPut("{id}")]
        public ActionResult<User> WijzigUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }
        //POST api/user  (een nieuwe User aanmaken)
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction("PostUser", user);
        }
        //DELETE api/user/id (een user te verwijderen)
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _context.Users.Find(id);
            if (user == null)// of hier kan je ook Bestaat methode gebruiken
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }

    }
}
