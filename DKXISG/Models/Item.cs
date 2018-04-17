using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Item
    {
        public Item()
        {
            this.OlayYeriFotoes = new List<OlayYeriFoto>();
        }

        public int Id { get; set; }
        public string TehlikeSinifi { get; set; }
        public string TEDurum { get; set; }
        public string Riskler { get; set; }
        public Nullable<int> Olasilik { get; set; }
        public Nullable<int> Siddet { get; set; }
        public Nullable<int> Risk { get; set; }
        public Nullable<int> OncelikSirasi { get; set; }
        public string DOFaliyetler { get; set; }
        public System.DateTime TerminTarihi { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public Nullable<int> SDID { get; set; }
        public virtual SahaDenetim SahaDenetim { get; set; }
        public virtual ICollection<OlayYeriFoto> OlayYeriFotoes { get; set; }
    }
}
