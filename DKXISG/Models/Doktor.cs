using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Doktor
    {
        public Doktor()
        {
            this.CalisanAsis = new List<CalisanAsi>();
            this.DoktorZiyarets = new List<DoktorZiyaret>();
            this.FirmaDoktors = new List<FirmaDoktor>();
        }

        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Adres { get; set; }
        public string DiplomaNo { get; set; }
        public Nullable<int> EkleyenID { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public virtual ICollection<CalisanAsi> CalisanAsis { get; set; }
        public virtual Yonetici Yonetici { get; set; }
        public virtual ICollection<DoktorZiyaret> DoktorZiyarets { get; set; }
        public virtual ICollection<FirmaDoktor> FirmaDoktors { get; set; }
    }
}
