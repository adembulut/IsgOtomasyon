using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class DoktorZiyaret
    {
        public DoktorZiyaret()
        {
            this.DoktorNots = new List<DoktorNot>();
            this.SaglikRaporus = new List<SaglikRaporu>();
        }

        public int Id { get; set; }
        public Nullable<int> DoktorID { get; set; }
        public Nullable<int> FirmaID { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public virtual Doktor Doktor { get; set; }
        public virtual ICollection<DoktorNot> DoktorNots { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual ICollection<SaglikRaporu> SaglikRaporus { get; set; }
    }
}
