using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Egitim
    {
        public int Id { get; set; }
        public string Konusu { get; set; }
        public Nullable<int> Suresi { get; set; }
    }
}
