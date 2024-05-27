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
    public partial class SifremiKurtarmaFormu : Form
    {
        private EmailSender emailSender;
        private static Random random = new Random();
        private KullaniciRepository kullaniciRepository;
        private SifreKurtarmaRepository sifreKurtarmaRepository;

        public SifremiKurtarmaFormu()
        {
            InitializeComponent();
            emailSender = new EmailSender("zeynepodev1@gmail.com", "dlkt qgjh gbtu utby");
            kullaniciRepository = KullaniciRepository.GetInstance();
            sifreKurtarmaRepository = SifreKurtarmaRepository.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e) { 
            Kullanici kullanici = kullaniciRepository.EmaildenKullanıcıGetir(textBox1.Text);
            if (kullanici != null) { 
                string yeniSifre = RastgeleSifreUret(12);
                emailSender.SifreKurtarmaMailiGonder(textBox1.Text, yeniSifre);
                kullanici.sifre = yeniSifre;
                kullaniciRepository.Guncelle(kullanici);
                SifreKurtarma sifreKurtarma = new SifreKurtarma();
                sifreKurtarma.talepZamani = DateTime.Now;
                sifreKurtarma.yeniSifre = yeniSifre;
                sifreKurtarma.kullanici = kullanici;

                sifreKurtarmaRepository.Ekle(sifreKurtarma);
                MessageBox.Show("Yeni şifre mailinize yollanmıştır.");
            }
            else
            {
                MessageBox.Show("Girilen e-posta adresi sistemde kayıtlı değil.");
            }
        }

        public static string RastgeleSifreUret(int karakterSayisi)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, karakterSayisi)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
