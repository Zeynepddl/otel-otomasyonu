// ZEYNEP DAYAL - 262284037
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

        }

        private void YeniPersonel_Load(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

            Kullanici personel = new Kullanici();
            personel.ad = txtad.Text;
            personel.soyad = txtsad.Text;
            personel.telefon = txttel.Text;
            personel.email = txtmail.Text;
            personel.sifre = txtsfr.Text;
            personel.kullaniciTuru = KullaniciTuru.personel;

            kullaniciRepository.Ekle(personel);

            MessageBox.Show("Kayıt Başarılı");
            this.Close();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
