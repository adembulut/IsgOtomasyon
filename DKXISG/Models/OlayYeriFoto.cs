using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class OlayYeriFoto
    {
        public int Id { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ResimYolu { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public virtual Item Item { get; set; }
    }
}
