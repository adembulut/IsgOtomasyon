using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Uzman
    {
        public Uzman()
        {
            this.FirmaUzmen = new List<FirmaUzman>();
            this.FirmaYapilacaks = new List<FirmaYapilacak>();
            this.SahaDenetims = new List<SahaDenetim>();
        }

        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Mail { get; set; }
        public Nullable<int> EkleyenID { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public virtual ICollection<FirmaUzman> FirmaUzmen { get; set; }
        public virtual ICollection<FirmaYapilacak> FirmaYapilacaks { get; set; }
        public virtual ICollection<SahaDenetim> SahaDenetims { get; set; }
        public virtual Yonetici Yonetici { get; set; }
    }
}
