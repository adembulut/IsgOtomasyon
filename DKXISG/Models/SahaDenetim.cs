using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class SahaDenetim
    {
        public SahaDenetim()
        {
            this.Items = new List<Item>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public int UzmanID { get; set; }
        public int FirmaID { get; set; }
        public System.DateTime EklenenTarih { get; set; }
        public Nullable<System.DateTime> RaporTarihi { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual Uzman Uzman { get; set; }
    }
}
