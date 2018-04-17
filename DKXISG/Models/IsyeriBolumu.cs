using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class IsyeriBolumu
    {
        public IsyeriBolumu()
        {
            this.Calisans = new List<Calisan>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public virtual ICollection<Calisan> Calisans { get; set; }
        public virtual Firma Firma { get; set; }
    }
}
