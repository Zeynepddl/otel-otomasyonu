﻿// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelYonetimi
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            PersonelGiris personelgiris = new PersonelGiris();
            personelgiris.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriGiris musteriGiris = new MusteriGiris();
            musteriGiris.ShowDialog();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
