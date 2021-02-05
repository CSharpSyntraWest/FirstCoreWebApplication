using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyFirstMVCWebApp.Models
{
    public partial class Bieren
    {
        public int BierNr { get; set; }
        public string Naam { get; set; }
        public int? BrouwerNr { get; set; }
        public int? SoortNr { get; set; }
        public double? Alcohol { get; set; }

        public virtual Brouwers BrouwerNrNavigation { get; set; }
        public virtual Soorten SoortNrNavigation { get; set; }
    }
}
