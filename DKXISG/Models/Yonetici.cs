using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Yonetici
    {
        public Yonetici()
        {
            this.Doktors = new List<Doktor>();
            this.Firmas = new List<Firma>();
            this.FirmaDoktors = new List<FirmaDoktor>();
            this.FirmaUzmen = new List<FirmaUzman>();
            this.Musteris = new List<Musteri>();
            this.Uzmen = new List<Uzman>();
        }

        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Adres { get; set; }
        public bool AktifMi { get; set; }
        public virtual ICollection<Doktor> Doktors { get; set; }
        public virtual ICollection<Firma> Firmas { get; set; }
        public virtual ICollection<FirmaDoktor> FirmaDoktors { get; set; }
        public virtual ICollection<FirmaUzman> FirmaUzmen { get; set; }
        public virtual ICollection<Musteri> Musteris { get; set; }
        public virtual ICollection<Uzman> Uzmen { get; set; }
    }
}
