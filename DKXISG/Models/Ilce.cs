using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Ilce
    {
        public Ilce()
        {
            this.Firmas = new List<Firma>();
        }

        public int Id { get; set; }
        public Nullable<int> IlID { get; set; }
        public string IlceAdi { get; set; }
        public virtual ICollection<Firma> Firmas { get; set; }
        public virtual Il Il { get; set; }
    }
}
