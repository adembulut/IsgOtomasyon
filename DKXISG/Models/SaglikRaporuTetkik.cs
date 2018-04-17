using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class SaglikRaporuTetkik
    {
        public int Id { get; set; }
        public int SaglikRaporuID { get; set; }
        public string Tetkik { get; set; }
        public string Sonuc { get; set; }
        public System.DateTime Tarih { get; set; }
        public virtual SaglikRaporu SaglikRaporu { get; set; }
    }
}
