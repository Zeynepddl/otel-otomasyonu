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
    public partial class PersonelSatisIslem : Form
    {
        OdemeRepository odemeRepository;
        RezervasyonRepository rezervasyonRepository;
        public PersonelSatisIslem()
        {
            InitializeComponent();
            odemeRepository=OdemeRepository.GetInstance();
            rezervasyonRepository = RezervasyonRepository.GetInstance();
        }
        

        void rezervasyonListele()
        {
            List<Rezervasyon> rezervasyonlar = rezervasyonRepository.Listele();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID",typeof(int));
            dt.Columns.Add("OdaID",typeof(int));
            dt.Columns.Add("KullaniciID",typeof(int));
            dt.Columns.Add("GirisZamani",typeof(DateTime));
            dt.Columns.Add("CikisZamani",typeof(DateTime));
            dt.Columns.Add("MusteriSayisi",typeof(int));
            dt.Columns.Add("CocukSayisi",typeof(int));
            dt.Columns.Add("OdemeID",typeof(int));

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
            dataGridView2.DataSource = dt;
        }

        private void PersonelSatisIslem_Load(object sender, EventArgs e)
        {
            
            rezervasyonListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {/*
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                Odeme odeme = odemeRepository.OdemeGetirByRezervasyonID(Convert.ToInt32(selectedRow.Cells["id"].Value));
                odeme.odemeTamamlandimi = true;
                odemeRepository.Guncelle(odeme);
                rezervasyonListele();



                MessageBox.Show("Rezervasyon ödemesi tamamlandı");
            }
            else
            {
                MessageBox.Show("Lütfen onaylanıcak bir kayıt seçin.");
            }*/

            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                int rezervasyonId = Convert.ToInt32(selectedRow.Cells["id"].Value);
                Odeme odeme = odemeRepository.OdemeGetirByRezervasyonID(rezervasyonId);

                if (odeme.odemeTamamlandimi)
                {
                    MessageBox.Show("Bu ödeme zaten onaylanmıştır.");
                }
                else
                {
                    odeme.odemeTamamlandimi = true;
                    odemeRepository.Guncelle(odeme);
                    rezervasyonListele();

                    MessageBox.Show("Rezervasyon ödemesi tamamlandı");
                }
            }
            else
            {
                MessageBox.Show("Lütfen onaylanacak bir kayıt seçin.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                Rezervasyon rezervasyon = new Rezervasyon();
                rezervasyon.id = Convert.ToInt32(selectedRow.Cells["id"].Value);

                rezervasyonRepository.Sil(rezervasyon);
                rezervasyonListele();

                MessageBox.Show("Rezervasyon kaydı silindi");
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
