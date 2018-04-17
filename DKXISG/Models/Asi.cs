using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Asi
    {
        public Asi()
        {
            this.CalisanAsis = new List<CalisanAsi>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public int MinUygulamaYasi { get; set; }
        public int MaxUygulamaYasi { get; set; }
        public int IslemPeriyodu { get; set; }
        public string Kontrendikasyon { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public Nullable<System.DateTime> DuzenlenmeTarihi { get; set; }
        public virtual ICollection<CalisanAsi> CalisanAsis { get; set; }
    }
}
