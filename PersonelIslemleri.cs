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
    public partial class PersonelIslemleri : Form
    {
        public PersonelIslemleri()
        {
            InitializeComponent();
        }

        private void PersonelIslemleri_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uyeler uyeler = new Uyeler();   
            uyeler.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MusteriBilgi musteriBilgi = new MusteriBilgi(); 
            musteriBilgi.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PersonelSatisIslem personelSatisIslem = new PersonelSatisIslem();
            personelSatisIslem.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonelOdaIslem personelOdaIslem = new PersonelOdaIslem();
            personelOdaIslem.ShowDialog();
        }
    }
}
