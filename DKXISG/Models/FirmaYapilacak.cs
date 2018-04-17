using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class FirmaYapilacak
    {
        public int Id { get; set; }
        public string Tipi { get; set; }
        public string Aciklama { get; set; }
        public bool isTamam { get; set; }
        public System.DateTime Tarih { get; set; }
        public int FirmaID { get; set; }
        public int UzmanID { get; set; }
        public Nullable<System.DateTime> okTarih { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual Uzman Uzman { get; set; }
    }
}
