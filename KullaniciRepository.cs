using OtelYonetimi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class KullaniciRepository
    {
        private string _connectionString;
        private static KullaniciRepository _instance;

        public KullaniciRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static KullaniciRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KullaniciRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }

        public void Ekle(Kullanici kullanici)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO kullanicilar (email, sifre, kullaniciTuru, ad, soyad, telefon) VALUES (@email, @sifre, @kullaniciTuru, @ad, @soyad, @telefon)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", kullanici.email);
                    command.Parameters.AddWithValue("@sifre", kullanici.sifre);
                    command.Parameters.AddWithValue("@kullaniciTuru", kullanici.kullaniciTuru);
                    command.Parameters.AddWithValue("@ad", kullanici.ad);
                    command.Parameters.AddWithValue("@soyad", kullanici.soyad);
                    command.Parameters.AddWithValue("@telefon", kullanici.telefon);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Kullanici> Listele()
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kullanicilar";
                using (var command = new SqlCommand(query, connection))
                {
                   var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Kullanici kullanici = new Kullanici();
                        kullanici.id = Convert.ToInt32(reader["id"]);
                        kullanici.email = reader["email"].ToString();
                        kullanici.normalizedEmail = reader["normalizedEmail"].ToString();
                        kullanici.sifre = reader["sifre"].ToString();
                        kullanici.ad = reader["ad"].ToString();
                        kullanici.soyad = reader["soyad"].ToString();
                        kullanici.telefon = reader["telefon"].ToString();
                        kullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        kullanicilar.Add(kullanici);
                    }
                    return kullanicilar;
                }
            }
        }

        public void Guncelle(Kullanici kullanici)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE kullanicilar SET email = @email,  sifre = @sifre, kullaniciTuru = @kullaniciTuru, ad = @ad, soyad = @soyad, telefon = @telefon WHERE id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", kullanici.email);
                    command.Parameters.AddWithValue("@sifre", kullanici.sifre);
                    command.Parameters.AddWithValue("@kullaniciTuru", kullanici.kullaniciTuru);
                    command.Parameters.AddWithValue("@ad", kullanici.ad);
                    command.Parameters.AddWithValue("@soyad", kullanici.soyad);
                    command.Parameters.AddWithValue("@telefon", kullanici.telefon);
                    command.ExecuteNonQuery();
                }

            }
        }

        public void Sil(Kullanici kullanici)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM kullanicilar WHERE id=@id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", kullanici.id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
