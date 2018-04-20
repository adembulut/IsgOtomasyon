using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Calisma
    {
        public int Id { get; set; }
        public Nullable<int> CalismaPlaniID { get; set; }
        public string CalismayiYapanKisi { get; set; }
        public string CalismayiYapanAdiSoyadi { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Aciklama { get; set; }
        public virtual CalismaPlani CalismaPlani { get; set; }
    }
}
