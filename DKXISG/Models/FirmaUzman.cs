using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class FirmaUzman
    {
        public int Id { get; set; }
        public int FirmaID { get; set; }
        public int UzmanID { get; set; }
        public System.DateTime AtanmaTarihi { get; set; }
        public int AtayanID { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual Uzman Uzman { get; set; }
        public virtual Yonetici Yonetici { get; set; }
    }
}
