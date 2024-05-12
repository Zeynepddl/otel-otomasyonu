using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class Odeme
    {
        public int id { get; set; }
        public Kullanici kullanici { get; set; }
        public double ucret { get; set; }
        public bool odemeTamamlandimi { get; set; }

    }
}
