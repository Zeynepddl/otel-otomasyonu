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
    public partial class PersonelOdaIslem : Form
    {
        private OdaRepository odaRepository;
        public PersonelOdaIslem()
        {
            InitializeComponent();
            odaRepository=OdaRepository.GetInstance();
        }
        void OdaListele()
        {
            List<Oda> odalar = odaRepository.Listele();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID",typeof(int));
            dt.Columns.Add("kapiNumarasi",typeof(string));
            dt.Columns.Add("odaAdi",typeof(string));
            dt.Columns.Add("odaTuru",typeof(string));
            dt.Columns.Add("enFazlaMusteriSayisi",typeof(int));
            dt.Columns.Add("enFazlaCocukSayisi",typeof(int));
            dt.Columns.Add("doluMu",typeof(bool));

           
            foreach (Oda oda in odalar)
            {
                DataRow row = dt.NewRow();
                row["ID"] =oda.id;
                row["kapiNumarasi"] = oda.kapiNumarasi;
                row["odaAdi"] = oda.odaAdi;
                row["odaTuru"] = oda.odaTuru;
                row["enFazlaMusteriSayisi"] = oda.enFazlaMusteriSayisi;
                row["enFazlaCocukSayisi"] = oda.enFazlaCocukSayisi;
                row["doluMu"] = oda.doluMu;
              
                dt.Rows.Add(row);
            }

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    Oda oda = new Oda();
                    oda.id = Convert.ToInt16(row.Cells["id"].Value);
                    oda.kapiNumarasi = Convert.ToString(row.Cells["kapiNumarasi"].Value);
                    oda.odaAdi = Convert.ToString(row.Cells["odaAdi"].Value);
                    oda.odaTuru = Convert.ToString(row.Cells["odaTuru"].Value);
                    oda.enFazlaMusteriSayisi = Convert.ToInt16(row.Cells["enFazlaMusteriSayisi"].Value);
                    oda.enFazlaCocukSayisi = Convert.ToInt16(row.Cells["enFazlaCocukSayisi"].Value);
                    oda.doluMu = Convert.ToBoolean(row.Cells["doluMU"].Value);
                    odaRepository.Guncelle(oda);
                    OdaListele();//hata verirse sil
                }
                MessageBox.Show("Oda boşaltıldı");
            }
            else
            {
                MessageBox.Show("Lütfen boşaltılacak bir oda seçin.");
            }
         }

        private void PersonelOdaIslem_Load(object sender, EventArgs e)
        {
            OdaListele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
