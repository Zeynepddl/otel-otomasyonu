using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelYonetimi
{
    public partial class YeniPersonel : Form
    {
        private KullaniciRepository kullaniciRepository;
        public YeniPersonel()
        {
            InitializeComponent();
            kullaniciRepository = KullaniciRepository.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici personel = new Kullanici();
            personel.ad = txtad.Text;
            personel.soyad = txtsad.Text;
            personel.telefon = txttel.Text;
            personel.email = txtmail.Text;
            personel.sifre = txtsfr.Text;
            personel.kullaniciTuru = KullaniciTuru.personel;

            kullaniciRepository.Ekle(personel);

            PersonelGiris personelgiris = new PersonelGiris();
            personelgiris.ShowDialog();
        }

        private void YeniPersonel_Load(object sender, EventArgs e)
        {

        }
    }
}
