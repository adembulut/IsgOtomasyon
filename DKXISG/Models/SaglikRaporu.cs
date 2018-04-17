using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class SaglikRaporu
    {
        public SaglikRaporu()
        {
            this.SaglikRaporuTetkiks = new List<SaglikRaporuTetkik>();
        }

        public int Id { get; set; }
        public int ZiyaretID { get; set; }
        public string HastaAdiSoyadi { get; set; }
        public string HastaTCNo { get; set; }
        public string HastaTelefon { get; set; }
        public string HastaAdres { get; set; }
        public string Aciklama { get; set; }
        public System.DateTime Tarih { get; set; }
        public virtual DoktorZiyaret DoktorZiyaret { get; set; }
        public virtual ICollection<SaglikRaporuTetkik> SaglikRaporuTetkiks { get; set; }
    }
}
