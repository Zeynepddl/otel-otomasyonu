﻿// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OtelYonetimi
{
    internal class SifreKurtarma
    {
        public int id { get; set; }
        public Kullanici kullanici { get; set; }
        public DateTime talepZamani { get; set; }
        public string yeniSifre { get; set; }
    }
}
