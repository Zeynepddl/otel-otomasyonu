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
    public partial class Uyeler : Form
    {
        public Uyeler()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=OtelOtomasyonu;Integrated Security=True;");
                void personelListele()
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from Personel", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
        private void Uyeler_Load(object sender, EventArgs e)
        {
            personelListele();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
