using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class SifreKurtarmaRepository
    {
        private string _connectionString;
        private static SifreKurtarmaRepository _instance;//Bu, SifreKurtarmaRepository sınıfının tek bir örneğini tutmak için kullanılan bir statik alan
        public SifreKurtarmaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public static SifreKurtarmaRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SifreKurtarmaRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=db_OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }
        public void Ekle(SifreKurtarma sifreKurtarma)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "insert into sifreKurtarmalar(kullanici_id,talepZamani,token,tokenGecerlilikSuresi) values(@kullanici_id,@talepZamani,@token,@tokenGecerlilikSuresi) ";
                using (var command=new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kullanici_id",sifreKurtarma.kullanici.id);
                    command.Parameters.AddWithValue("@talepZamani",sifreKurtarma.talepZamani);
                    command.Parameters.AddWithValue("@token",sifreKurtarma.token);
                    command.Parameters.AddWithValue("@tokenGecerlilikSuresi",sifreKurtarma.tokenGecerlilikSuresi);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<SifreKurtarma> Listele()
        {
            List<SifreKurtarma> sifreKurtarmalar = new List<SifreKurtarma>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "select * from sifreKurtarmalar Inner join kullanicilar on sifrekurtarma.kullanici_id=kullanicilar.id";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        SifreKurtarma sifreKurtarma = new SifreKurtarma();
                        Kullanici kullanici = new Kullanici();

                        sifreKurtarma.id = Convert.ToInt32(reader["id"]);
                        sifreKurtarma.talepZamani = Convert.ToDateTime(reader["talepZamani"]);
                        sifreKurtarma.token = reader["token"].ToString();
                        sifreKurtarma.tokenGecerlilikSuresi = Convert.ToDateTime(reader["tokenGecerlilikSuresi"]);


                        kullanici.id= Convert.ToInt32(reader["id"]);
                        kullanici.ad = reader["ad"].ToString();
                        kullanici.soyad = reader["soyad"].ToString();
                        kullanici.telefon = reader["telefon"].ToString();
                        kullanici.email = reader["email"].ToString();
                        kullanici.normalizedEmail = reader["normalizedemail"].ToString();
                        kullanici.sifre = reader["sifre"].ToString() ;
                        kullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        sifreKurtarma.kullanici= kullanici;

                        sifreKurtarmalar.Add(sifreKurtarma);
                    }
                    return sifreKurtarmalar;
                   
                } 
                
            }
        }
        public void Guncelle(SifreKurtarma sifreKurtarma)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "update sifreKurtarmalar set kullanici_id=@kullanici_id,talepZamani=@talepZamani,token=@token,tokenGecerlilikSuresi=@tokenGecerlilikSuresi where id=@id";
                using (var command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@id",sifreKurtarma.id);
                    command.Parameters.AddWithValue("@kullanici_id", sifreKurtarma.kullanici.id);
                    command.Parameters.AddWithValue("@talepZamani",sifreKurtarma.talepZamani);
                    command.Parameters.AddWithValue("@token",sifreKurtarma.token);
                    command.Parameters.AddWithValue("@tokenGecerlilikSuresi",sifreKurtarma.tokenGecerlilikSuresi);
                    command.ExecuteNonQuery();
                }
            }

        }
        public void Sil(SifreKurtarma sifreKurtarma)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query="delete from sifreKurtarmalar where id=@id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", sifreKurtarma.id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
