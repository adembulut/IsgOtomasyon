using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class FirmaDoktor
    {
        public int Id { get; set; }
        public int FirmaID { get; set; }
        public int DoktorID { get; set; }
        public System.DateTime AtanmaTarihi { get; set; }
        public int AtayanID { get; set; }
        public virtual Doktor Doktor { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual Yonetici Yonetici { get; set; }
    }
}
