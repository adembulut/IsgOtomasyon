using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class CalismaPlani
    {
        public CalismaPlani()
        {
            this.Calismas = new List<Calisma>();
        }

        public int Id { get; set; }
        public string Faliyet { get; set; }
        public int FirmaID { get; set; }
        public string PeriyotTipi { get; set; }
        public int PeriyotAraligi { get; set; }
        public string EkleyenKisi { get; set; }
        public string EkleyenAdi { get; set; }
        public string SorumluKisi { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public string Aciklama { get; set; }
        public virtual ICollection<Calisma> Calismas { get; set; }
        public virtual Firma Firma { get; set; }
    }
}
