using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class Kullanici
    {
        public int id { get; set; }
        public string email { get; set; }
        public string normalizedEmail { get; set; }
        public string sifre { get; set; }
        public KullaniciTuru kullaniciTuru { get; set; }
    }
}
