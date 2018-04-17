using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class DoktorNot
    {
        public int Id { get; set; }
        public Nullable<int> DoktorZiyaretID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public virtual DoktorZiyaret DoktorZiyaret { get; set; }
    }
}
