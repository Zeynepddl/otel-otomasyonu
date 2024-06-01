// ZEYNEP DAYAL - 262284037
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
    public partial class PersonelGiris : Form
    {
        KullaniciRepository kullaniciRepository;
        public PersonelGiris()
        {
            InitializeComponent();
            kullaniciRepository = KullaniciRepository.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void PersonelGiris_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Kullanici kullanici = new Kullanici();
            kullanici.email = txtmail.Text;
            kullanici.sifre = txtsifre.Text;
            kullanici.kullaniciTuru = KullaniciTuru.personel;

            Kullanici musteriKullanici = kullaniciRepository.KullaniciGetir(kullanici);
            if (musteriKullanici != null)
            {
                PersonelIslemleri personelIslemleri = new PersonelIslemleri();
                personelIslemleri.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış Bilgi");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            YeniPersonel yeniPersonel = new YeniPersonel();
            yeniPersonel.ShowDialog();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifremiKurtarmaFormu sifremiKurtarmaFormu = new SifremiKurtarmaFormu();
            sifremiKurtarmaFormu.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
