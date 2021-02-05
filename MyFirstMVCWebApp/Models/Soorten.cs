using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyFirstMVCWebApp.Models
{
    public partial class Soorten
    {
        public Soorten()
        {
            Bieren = new HashSet<Bieren>();
        }

        public int SoortNr { get; set; }
        public string Soort { get; set; }

        public virtual ICollection<Bieren> Bieren { get; set; }
    }
}
