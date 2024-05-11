using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class Rezervasyon
    {
        public int id { get; set; }
        public Oda oda { get; set; }
        public Kullanici kullanici { get; set; }
        public DateTime girisZamani { get; set; }
        public DateTime cikisZamani { get; set; }
        public int musteriSayisi { get; set; }
        public int cocukSayisi { get; set; }
        public Odeme odeme { get; set; }

    }
}
