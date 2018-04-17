using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class CalisanAsi
    {
        public int Id { get; set; }
        public int AsiID { get; set; }
        public int CalisanID { get; set; }
        public System.DateTime AsiTarihi { get; set; }
        public int DoktorID { get; set; }
        public string Aciklama { get; set; }
        public virtual Asi Asi { get; set; }
        public virtual Calisan Calisan { get; set; }
        public virtual Doktor Doktor { get; set; }
    }
}
