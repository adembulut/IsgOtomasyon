using System;
using System.Collections.Generic;

namespace DKXISG.Models
{
    public partial class Il
    {
        public Il()
        {
            this.Ilces = new List<Ilce>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public virtual ICollection<Ilce> Ilces { get; set; }
    }
}
