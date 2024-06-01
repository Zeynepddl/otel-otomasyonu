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
    public partial class MusteriGiris : Form
    {
        KullaniciRepository kullaniciRepository;
        public MusteriGiris()
        {
            InitializeComponent();
            kullaniciRepository=KullaniciRepository.GetInstance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

           
        }

        private void txtsifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void MusteriGiris_Load(object sender, EventArgs e)
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
            kullanici.kullaniciTuru = KullaniciTuru.musteri;

            Kullanici musteriKullanici = kullaniciRepository.KullaniciGetir(kullanici);
            if (musteriKullanici != null)
            {
                MusteriIslemleri musteriIslemleri = new MusteriIslemleri(musteriKullanici);
                musteriIslemleri.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış Bilgi");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            YeniMusteri yeniMusteri = new YeniMusteri();
            yeniMusteri.ShowDialog();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifremiKurtarmaFormu sifremiKurtarmaForm = new SifremiKurtarmaFormu();
            sifremiKurtarmaForm.ShowDialog();
        }
    }
}
