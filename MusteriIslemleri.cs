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
    public partial class MusteriIslemleri : Form
    {
        public MusteriIslemleri()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MusteriIslemleri_Load(object sender, EventArgs e)
        {
            txtOdaNo.Enabled = false;
            txtOdaAd.Enabled = false;
            txtId.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "11";
            txtOdaAd.Text = "Oda 11";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "1"; 
            txtOdaAd.Text = "Oda 1"; 
        }

        private void txtOdaNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "2";
            txtOdaAd.Text = "Oda 2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "3";
            txtOdaAd.Text = "Oda 3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "4";
            txtOdaAd.Text = "Oda 4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "5";
            txtOdaAd.Text = "Oda 5";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "6";
            txtOdaAd.Text = "Oda 6";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "7";
            txtOdaAd.Text = "Oda 7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "8";
            txtOdaAd.Text = "Oda 8";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "9";
            txtOdaAd.Text = "Oda 9";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "10";
            txtOdaAd.Text = "Oda 10";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "12";
            txtOdaAd.Text = "Oda 12";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "13";
            txtOdaAd.Text = "Oda 13";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "14";
            txtOdaAd.Text = "Oda 14";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "15";
            txtOdaAd.Text = "Oda 15";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "16";
            txtOdaAd.Text = "Oda 16";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "17";
            txtOdaAd.Text = "Oda 17";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "18";
            txtOdaAd.Text = "Oda 18";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "19";
            txtOdaAd.Text = "Oda 19";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "20";
            txtOdaAd.Text = "Oda 20";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "21";
            txtOdaAd.Text = "Oda 21";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "22";
            txtOdaAd.Text = "Oda 22";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "23";
            txtOdaAd.Text = "Oda 23";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "24";
            txtOdaAd.Text = "Oda 24";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            txtOdaNo.Text = "25";
            txtOdaAd.Text = "Oda 25";
        }
    }
}
