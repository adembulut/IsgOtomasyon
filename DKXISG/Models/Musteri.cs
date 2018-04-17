using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Musteri
    {
        public Musteri()
        {
            this.Firmas = new List<Firma>();
        }

        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string AdiSoyadi { get; set; }
        public string Unvani { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Adres { get; set; }
        public int EkleyenID { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public virtual ICollection<Firma> Firmas { get; set; }
        public virtual Yonetici Yonetici { get; set; }
    }
}
