using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(_context.Users.ToList()); //OK = 200 status code 
        }
        //POST api/user  (een nieuwe User aanmaken)
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction("PostUser", user);
        }
    }
}
