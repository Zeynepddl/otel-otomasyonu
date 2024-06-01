// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    public class Oda
    {
        public int id { get; set; }
        public string kapiNumarasi { get; set; }
        public string odaAdi { get; set; }
        public string odaTuru { get; set; }
        public int enFazlaMusteriSayisi { get; set; }
        public int enFazlaCocukSayisi { get; set; }

        public bool doluMu { get; set; }

    }
}
