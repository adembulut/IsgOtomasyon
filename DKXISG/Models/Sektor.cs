using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Sektor
    {
        public Sektor()
        {
            this.Firmas = new List<Firma>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public virtual ICollection<Firma> Firmas { get; set; }
    }
}
