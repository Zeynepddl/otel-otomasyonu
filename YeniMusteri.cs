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
    public partial class YeniMusteri : Form
    {
        private KullaniciRepository kullaniciRepository;
        public YeniMusteri()
        {
            InitializeComponent();
            kullaniciRepository = KullaniciRepository.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void YeniMusteri_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((txtad.Text == "") || (txtsad.Text == "") || (txttel.Text == "") || txttel.MaskFull == false || (txtmail.Text == "") || (txtsfr.Text == ""))
            {
                MessageBox.Show("Boş alan bırakılamaz");
                return;
            }

            string email = txtmail.Text;
            string telefon = txttel.Text;

            if (kullaniciRepository.TelefonVarMi(telefon))
            {
                MessageBox.Show("Bu telefon numarası zaten kayıtlı. ");
                return;
            }

            if (kullaniciRepository.EmailVarMi(email))
            {
                MessageBox.Show("Bu e-posta adresi zaten kayıtlı.");
                return;// Eğer e-posta adresi zaten kayıtlıysa işlemi sonlandır
            }

            Kullanici musteri = new Kullanici();
            musteri.ad = txtad.Text;
            musteri.soyad = txtsad.Text;
            musteri.telefon = txttel.Text;
            musteri.email = txtmail.Text;
            musteri.sifre = txtsfr.Text;
            musteri.kullaniciTuru = KullaniciTuru.musteri;

            kullaniciRepository.Ekle(musteri);

            MessageBox.Show("Kayıt Başarılı");


            MusteriGiris musteriGiris = new MusteriGiris();
            musteriGiris.Show();
        }
    }
}
