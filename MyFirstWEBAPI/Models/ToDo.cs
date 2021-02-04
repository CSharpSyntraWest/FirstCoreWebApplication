using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWEBAPI.Models
{
    public enum PrioriteitsNiveau
    { 
        Laag,
        Middel,
        Hoog
    }
    public class ToDo
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public PrioriteitsNiveau Prioriteit { get; set; }
        public bool IsGedaan { get; set; }
    }
}
