using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Firma
    {
        public Firma()
        {
            this.DoktorZiyarets = new List<DoktorZiyaret>();
            this.FirmaDoktors = new List<FirmaDoktor>();
            this.FirmaUzmen = new List<FirmaUzman>();
            this.FirmaYapilacaks = new List<FirmaYapilacak>();
            this.IsyeriBolumus = new List<IsyeriBolumu>();
            this.SahaDenetims = new List<SahaDenetim>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public Nullable<int> IlceID { get; set; }
        public string Adresi { get; set; }
        public string Telefonu { get; set; }
        public string Sinifi { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNumarasi { get; set; }
        public string LatLng { get; set; }
        public string Web { get; set; }
        public string Mail { get; set; }
        public int CalisanSayisi { get; set; }
        public int MusteriID { get; set; }
        public int EkleyenID { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public bool TamamlandiMi { get; set; }
        public int SektorId { get; set; }
        public string ResimYolu { get; set; }
        public virtual ICollection<DoktorZiyaret> DoktorZiyarets { get; set; }
        public virtual Ilce Ilce { get; set; }
        public virtual Musteri Musteri { get; set; }
        public virtual Sektor Sektor { get; set; }
        public virtual Yonetici Yonetici { get; set; }
        public virtual ICollection<FirmaDoktor> FirmaDoktors { get; set; }
        public virtual ICollection<FirmaUzman> FirmaUzmen { get; set; }
        public virtual ICollection<FirmaYapilacak> FirmaYapilacaks { get; set; }
        public virtual ICollection<IsyeriBolumu> IsyeriBolumus { get; set; }
        public virtual ICollection<SahaDenetim> SahaDenetims { get; set; }
    }
}
