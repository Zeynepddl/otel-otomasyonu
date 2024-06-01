// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelYonetimi
{
    public partial class MusteriIslemleri : Form
    {
        OdaRepository odaRepository;
        RezervasyonRepository rezervasyonRepository;
        Kullanici kullanici;
        public MusteriIslemleri(Kullanici kullanici)
        {
            InitializeComponent();
            odaRepository = OdaRepository.GetInstance();
            rezervasyonRepository = RezervasyonRepository.GetInstance();
            this.kullanici = kullanici;
        }

        void odaListele()
        {
            List<Oda> odalar = odaRepository.Listele();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Kapı Numarası", typeof(string));
            dt.Columns.Add("Oda Adı", typeof(string));
            dt.Columns.Add("Oda Türü", typeof(string));
            dt.Columns.Add("Yetişkin Sayısı", typeof(int));
            dt.Columns.Add("Çocuk Sayısı", typeof(int));
            dt.Columns.Add("Doluluk Durumu", typeof(bool));

            foreach (Oda oda in odalar)
            {
                DataRow row = dt.NewRow();
                row["ID"] = oda.id;
                row["Kapı Numarası"] = oda.kapiNumarasi;
                row["Oda Adı"] = oda.odaAdi;
                row["Oda Türü"] = oda.odaTuru;
                row["Yetişkin Sayısı"] = oda.enFazlaMusteriSayisi;
                row["Çocuk Sayısı"] = oda.enFazlaCocukSayisi;
                row["Doluluk Durumu"] = oda.doluMu;

                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ID"].Visible = false;
        }

        void rezervasyonListele()
        {
            List<Rezervasyon> rezervasyonlar = rezervasyonRepository.Listele();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("OdaID", typeof(int));
            dt.Columns.Add("KullaniciID", typeof(int));
            dt.Columns.Add("GirisZamani", typeof(DateTime));
            dt.Columns.Add("CikisZamani", typeof(DateTime));
            dt.Columns.Add("MusteriSayisi", typeof(int));
            dt.Columns.Add("CocukSayisi", typeof(int));
            dt.Columns.Add("OdemeID", typeof(int));

            foreach (Rezervasyon rezervasyon in rezervasyonlar)
            {
                DataRow row = dt.NewRow();
                row["ID"] = rezervasyon.id;
                row["OdaID"] = rezervasyon.oda.id;
                row["KullaniciID"] = rezervasyon.kullanici.id;
                row["GirisZamani"] = rezervasyon.girisZamani;
                row["CikisZamani"] = rezervasyon.cikisZamani;
                row["MusteriSayisi"] = rezervasyon.musteriSayisi;
                row["CocukSayisi"] = rezervasyon.cocukSayisi;
                row["OdemeID"] = rezervasyon.odeme.id;

                dt.Rows.Add(row);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MusteriIslemleri_Load(object sender, EventArgs e)
        {
            txtYetiskin.Enabled = false;
            txtCocuk.Enabled = false;
            odaListele();
            dataGridView1.ForeColor = Color.Black;
            hesaplananUcretiGuncelle();

            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker2.MinDate = DateTime.Today;

        }

        private void button15_Click(object sender, EventArgs e)
        {
        }

        private void button26_Click(object sender, EventArgs e)
        {

            
            if (dataGridView1.SelectedRows.Count == 1 )
            {


                DataGridViewRow row = dataGridView1.SelectedRows[0];
                bool doluMu = Convert.ToBoolean(row.Cells["Doluluk Durumu"].Value);

                if (doluMu)
                {
                    MessageBox.Show("Seçtiğiniz oda dolu. Lütfen başka bir oda seçin.");
                    return;
                }

                
                    Oda oda = new Oda();
                    oda.id = Convert.ToInt16(row.Cells["id"].Value);
                    oda.kapiNumarasi = Convert.ToString(row.Cells["Kapı Numarası"].Value);
                    oda.odaAdi = Convert.ToString(row.Cells["Oda Adı"].Value);
                    oda.odaTuru = Convert.ToString(row.Cells["Oda Türü"].Value);
                    oda.enFazlaMusteriSayisi = Convert.ToInt16(row.Cells["Yetişkin Sayısı"].Value);
                    oda.enFazlaCocukSayisi = Convert.ToInt16(row.Cells["Çocuk Sayısı"].Value);
                    oda.doluMu = Convert.ToBoolean(row.Cells["Doluluk Durumu"].Value);
                    


                    Rezervasyon rezervasyon = new Rezervasyon();
                    rezervasyon.oda = oda;
                    rezervasyon.kullanici = this.kullanici;
                    rezervasyon.girisZamani = Convert.ToDateTime(dateTimePicker1.Text);
                    rezervasyon.cikisZamani = Convert.ToDateTime(dateTimePicker2.Text);
                    rezervasyon.musteriSayisi = Convert.ToInt16(txtYetiskin.Text);
                    rezervasyon.cocukSayisi = Convert.ToInt16(txtCocuk.Text);
                    rezervasyon.odeme = new Odeme(); // Ödeme nesnesini oluşturun ve gerekli özellikleri doldurun
                    rezervasyon.odeme.ucret = Convert.ToInt16(label10.Text);
                    rezervasyon.odeme.kullanici = this.kullanici;


                    

                    OdemeOnayla odemeOnayla = new OdemeOnayla();
                    odemeOnayla.setRezervasyon(rezervasyon);
                    odemeOnayla.ShowDialog();
                    
                    odaListele();
                
            }
            else
            {
                MessageBox.Show("Lütfen bir oda seçin.");
            }








        }




        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void txtOdaNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button12_Click(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void button20_Click(object sender, EventArgs e)
        {
        }

        private void button19_Click(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e)
        {
        }

        private void button17_Click(object sender, EventArgs e)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
        }

        private void button25_Click(object sender, EventArgs e)
        {
        }

        private void button24_Click(object sender, EventArgs e)
        {
        }

        private void button23_Click(object sender, EventArgs e)
        {
        }

        private void button22_Click(object sender, EventArgs e)
        {
        }

        private void button21_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // txtOdaNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //txtOdaAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        void hesaplananUcretiGuncelle()
        {
            int odaninFiyati = 1000; 
            int gunSayisi = (dateTimePicker2.Value - dateTimePicker1.Value).Days;
            if (gunSayisi < 1)
            {
                gunSayisi = 1;
            }
            int hesaplananUcret = odaninFiyati * gunSayisi; 
            label10.Text = hesaplananUcret.ToString();
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Giriş tarihi çıkış tarihinden büyükse veya eşitse çıkış tarihini giriş tarihinden bir gün sonrası yap
            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date)
            {
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
            }
          
            
            hesaplananUcretiGuncelle();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Çıkış tarihi giriş tarihinden küçükse veya eşitse giriş tarihini çıkış tarihinden bir gün öncesi yap
            if (dateTimePicker2.Value.Date <= dateTimePicker1.Value.Date)
            {
                dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-1);//-1" ifadesi, dateTimePicker2'nin değerini dateTimePicker1'den bir gün sonrası olarak belirlemek için kullanılır.
            }
           
            hesaplananUcretiGuncelle();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1) 
            {
                txtYetiskin.Text = dataGridView1.CurrentRow.Cells["Yetişkin Sayısı"].Value.ToString();
                txtCocuk.Text = dataGridView1.CurrentRow.Cells["Çocuk Sayısı"].Value.ToString();
            }
        }
    }
}
