// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    public class Odeme
    {
        public int id { get; set; }
        public Kullanici kullanici { get; set; } = new Kullanici();
        public double ucret { get; set; }
        public bool odemeTamamlandimi { get; set; }

    }
}
