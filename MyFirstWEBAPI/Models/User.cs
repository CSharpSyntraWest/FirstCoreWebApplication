using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWEBAPI.Models
{
    public class User
    {
        public User()
        {
            ToDos = new List<ToDo>();
        }
        public int Id { get; set; }
        public string VoorNaam { get; set; }
        public string FamilieNaam { get; set; }
        public int Leeftijd { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
