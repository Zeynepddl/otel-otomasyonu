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
    public partial class OdemeOnayla : Form
    {
        RezervasyonRepository rezervasyonRepository;
        OdaRepository odaRepository;
        OdemeRepository odemeRepository;
        public Rezervasyon rezervasyon;
        public OdemeOnayla()
        {
            InitializeComponent();
            rezervasyonRepository = RezervasyonRepository.GetInstance();
            odemeRepository=OdemeRepository.GetInstance();
            odaRepository = OdaRepository.GetInstance();
            
        }
        public void setRezervasyon(Rezervasyon rezervasyon)
        {
           this.rezervasyon = rezervasyon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            rezervasyon.odeme = odemeRepository.Ekle(rezervasyon.odeme);
            rezervasyonRepository.Ekle(rezervasyon);
            

            MessageBox.Show("Rezervasyon oluşturulmuştur");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void OdemeOnayla_Load(object sender, EventArgs e)
        {
            label2.Text = rezervasyon.odeme.ucret.ToString();
        }
    }
}
