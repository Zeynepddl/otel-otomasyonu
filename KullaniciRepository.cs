// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                _instance = new KullaniciRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=db_OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }

        //Tabloda güncelleme yaparken o numaranın olduğu başka kayıt var mı yok mu kontrol eder.
        public bool TelefonVarMi(string telefon ,int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE telefon = @Telefon AND id != @KullaniciId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@KullaniciId", id);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        // Yeni kullanıcı eklerken kullanılan metodlar
        //Bu,yeni bir kullanıcı eklenirken aynı numaraya sahip başka bir kullanıcının olup olmadığını kontrol eder.
        public bool TelefonVarMi(string telefon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE telefon = @Telefon";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool EmailVarMi(string email, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE email = @Email AND id != @KullaniciId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@KullaniciId", id);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public bool EmailVarMi(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM kullanicilar WHERE email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void KullaniciSil(int kullaniciId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Rezervasyonlar tablosundaki ilişkili kayıtları sil
                        SqlCommand cmd3 = new SqlCommand("DELETE FROM rezervasyonlar WHERE odeme_id IN (SELECT id FROM odemeler WHERE kullanici_id = @KullaniciId)", connection, transaction);
                        cmd3.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        cmd3.ExecuteNonQuery();

                        // Odemeler tablosundaki ilişkili kayıtları sil
                        SqlCommand cmd0 = new SqlCommand("DELETE FROM odemeler WHERE kullanici_id = @KullaniciId", connection, transaction);
                        cmd0.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        cmd0.ExecuteNonQuery();
                        

                        // Şifre kurtarma kayıtlarını sil
                        SqlCommand cmd = new SqlCommand("DELETE FROM sifreKurtarmalar WHERE kullanici_id = @KullaniciId", connection, transaction);
                        cmd.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        cmd.ExecuteNonQuery();

                        // Kullanıcıyı sil
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM kullanicilar WHERE id = @KullaniciId", connection, transaction);
                        cmd2.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        cmd2.ExecuteNonQuery();

                        // İşlemleri onayla
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda işlemleri geri al
                        transaction.Rollback();
                        MessageBox.Show("Kullanıcı silinirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }
    
        public Kullanici KullaniciGetir(Kullanici kullanici)
        {
            using (var connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "select * from kullanicilar where email=@email and sifre=@sifre and kullaniciTuru=@kullaniciTuru";
                using (var command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@email", kullanici.email);
                    command.Parameters.AddWithValue("@sifre", kullanici.sifre);
                    command.Parameters.AddWithValue("@kullaniciTuru", kullanici.kullaniciTuru);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Kullanici okunanKullanici = new Kullanici();
                        okunanKullanici.id = Convert.ToInt32(reader["id"]);
                        okunanKullanici.email = reader["email"].ToString();
                        okunanKullanici.normalizedEmail = reader["normalizedEmail"].ToString();
                        okunanKullanici.sifre = reader["sifre"].ToString();
                        okunanKullanici.ad = reader["ad"].ToString();
                        okunanKullanici.soyad = reader["soyad"].ToString();
                        okunanKullanici.telefon = reader["telefon"].ToString();
                        okunanKullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        return okunanKullanici;
                    } else
                    {
                        return null;
                    }
                }
            }
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

        //listele metoduna giriş parametresi olarak kullanıcı turu enumını eklicez 
        public List<Kullanici> Listele(KullaniciTuru kullaniciTuru)
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kullanicilar where kullaniciTuru=@kullaniciTuru";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kullaniciTuru", kullaniciTuru);
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
                string query = "UPDATE kullanicilar SET  ad = @ad, soyad = @soyad, telefon = @telefon,email = @email, normalizedEmail=@normalizedEmail, sifre = @sifre, kullaniciTuru = @kullaniciTuru WHERE id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", kullanici.id);
                    command.Parameters.AddWithValue("@ad", kullanici.ad);
                    command.Parameters.AddWithValue("@soyad", kullanici.soyad);
                    command.Parameters.AddWithValue("@telefon", kullanici.telefon);
                    command.Parameters.AddWithValue("@email", kullanici.email);
                    command.Parameters.AddWithValue("@normalizedEmail", kullanici.normalizedEmail);
                    command.Parameters.AddWithValue("@sifre", kullanici.sifre);
                    command.Parameters.AddWithValue("@kullaniciTuru", kullanici.kullaniciTuru);
                    
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

        internal Kullanici EmaildenKullaniciGetir(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM kullanicilar WHERE email=@email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email",email);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Kullanici okunanKullanici = new Kullanici();
                        okunanKullanici.id = Convert.ToInt32(reader["id"]);
                        okunanKullanici.email = reader["email"].ToString();
                        okunanKullanici.normalizedEmail = reader["normalizedEmail"].ToString();
                        okunanKullanici.sifre = reader["sifre"].ToString();
                        okunanKullanici.ad = reader["ad"].ToString();
                        okunanKullanici.soyad = reader["soyad"].ToString();
                        okunanKullanici.telefon = reader["telefon"].ToString();
                        okunanKullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        return okunanKullanici;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
