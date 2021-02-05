using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyFirstMVCWebApp.Models
{
    public partial class Brouwers
    {
        public Brouwers()
        {
            Bieren = new HashSet<Bieren>();
        }

        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
        public int? Omzet { get; set; }

        public virtual ICollection<Bieren> Bieren { get; set; }
    }
}
