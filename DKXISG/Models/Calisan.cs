using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Calisan
    {
        public Calisan()
        {
            this.CalisanAsis = new List<CalisanAsi>();
        }

        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Gorevi { get; set; }
        public Nullable<System.DateTime> DogumTarihi { get; set; }
        public string TcKimlikNo { get; set; }
        public string Cinsiyet { get; set; }
        public string MedeniHali { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string EgitimDurumu { get; set; }
        public string KanGrubu { get; set; }
        public Nullable<int> IsyeriBolumID { get; set; }
        public Nullable<System.DateTime> IseGirisTarihi { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public Nullable<bool> HalaCalisiyorMu { get; set; }
        public virtual IsyeriBolumu IsyeriBolumu { get; set; }
        public virtual ICollection<CalisanAsi> CalisanAsis { get; set; }
    }
}
