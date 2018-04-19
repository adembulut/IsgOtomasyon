using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Egitim
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public System.DateTime EgitimTarihi { get; set; }
        public string Lokasyonu { get; set; }
        public bool isYapildi { get; set; }
        public string EgitimYapan { get; set; }
        public string EgitimEkleyen { get; set; }
        public string EkleyenAdiSoyadi { get; set; }
        public int FirmaID { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public virtual Firma Firma { get; set; }
    }
}
