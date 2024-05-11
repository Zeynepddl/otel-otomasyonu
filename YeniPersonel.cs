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
        public YeniPersonel()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=OtelOtomasyonu;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Personel values(@a,@s,@tc,@tel,@cins,@dt,@mail,@sfr)", con);
            cmd.Parameters.AddWithValue("@a", txtad.Text);
            cmd.Parameters.AddWithValue("@s", txtsad.Text);
            cmd.Parameters.AddWithValue("@tc", txttc.Text);
            cmd.Parameters.AddWithValue("@tel", txttel.Text);
            cmd.Parameters.AddWithValue("@cins", (rbK.Checked ? "Kadın" : "Erkek"));
            cmd.Parameters.AddWithValue("@dt", txtdt.Text);
            cmd.Parameters.AddWithValue("@mail", txtmail.Text);
            cmd.Parameters.AddWithValue("@sfr", txtsfr.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            PersonelGiris personelgiris = new PersonelGiris();
            personelgiris.ShowDialog();
        }
    }
}
