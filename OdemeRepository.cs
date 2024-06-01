// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class OdemeRepository
    {
        private string _connectionString;
        private static OdemeRepository _instance;

        public OdemeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static OdemeRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new OdemeRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=db_OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }
        public Odeme OdemeGetirByRezervasyonID(int rezervasyonID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "select * from odemeler " +
                "Inner join rezervasyonlar on odemeler.id=rezervasyonlar.odeme_id " +
                 "Inner join kullanicilar on odemeler.kullanici_id=kullanicilar.id "+
                "where rezervasyonlar.id=@rezervasyonID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@rezervasyonID", rezervasyonID);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Odeme odeme = new Odeme();
                        Kullanici kullanici= new Kullanici();
                        odeme.id = Convert.ToInt32(reader["id"]);
                        odeme.ucret = (double)Convert.ToDecimal(reader["ucret"]);
                        odeme.odemeTamamlandimi = Convert.ToBoolean(reader["odemeTamamlandimi"]);

                        kullanici.id = Convert.ToInt32(reader["kullanici_id"]);
                        odeme.kullanici = kullanici;
                        return odeme;
                    }
                    else
                    {
                        return null;
                    }

                }

            }
        }
        public Odeme Ekle(Odeme odeme)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query= "insert into odemeler(kullanici_id,ucret,odemeTamamlandimi) values(@kullanici_id,@ucret,@odemeTamamlandimi);select CONVERT(int,scope_identity())";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kullanici_id",odeme.kullanici.id);
                    command.Parameters.AddWithValue("@ucret",odeme.ucret);
                    command.Parameters.AddWithValue("@odemeTamamlandimi",odeme.odemeTamamlandimi);
                    var id = command.ExecuteScalar();
                    odeme.id = (int)id;
                    return odeme;
                }
            }
        }
        public List<Odeme> Listele()
        {
            List<Odeme> odemeler = new List<Odeme>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "select * from odemeler " +
                "Inner join kullanicilar on odemeler.kullanici_id=kullanicilar.id ";                
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Odeme odeme = new Odeme();
                        Kullanici kullanici = new Kullanici();

                        odeme.id = Convert.ToInt32(reader["id"]);
                        odeme.ucret = (double)Convert.ToDecimal(reader["ucret"]);
                        odeme.odemeTamamlandimi = Convert.ToBoolean(reader["odemeTamamlandimi"]);
                        

                        kullanici.id = Convert.ToInt32(reader["kullanici_id"]);
                        kullanici.ad = reader["ad"].ToString();
                        kullanici.soyad = reader["soyad"].ToString();
                        kullanici.telefon = reader["telefon"].ToString();
                        kullanici.email = reader["email"].ToString();
                        kullanici.normalizedEmail = reader["normalizedEmail"].ToString();
                        kullanici.sifre = reader["sifre"].ToString();
                        kullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        odeme.kullanici = kullanici;
                        //

                        odemeler.Add(odeme);
                    }
                    return odemeler;
                }
            }
        }



        public void Guncelle(Odeme odeme)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE odemeler SET kullanici_id = @kullanici_id, ucret = @ucret, odemeTamamlandimi = @odemeTamamlandimi WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@id", odeme.id);
                    command.Parameters.AddWithValue("@kullanici_id", odeme.kullanici.id);
                    command.Parameters.AddWithValue("@ucret", odeme.ucret);
                    command.Parameters.AddWithValue("@odemeTamamlandimi", odeme.odemeTamamlandimi);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Sil(Odeme odeme)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "delete from odemeler where id=@id";
                using (var command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@id",odeme.id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
