using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class RezervasyonRepository
    {
        private string _connectionString;
        private static RezervasyonRepository _instance;

        public RezervasyonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static RezervasyonRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RezervasyonRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=db_OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }

        public void Ekle(Rezervasyon rezervasyon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO rezervasyonlar (oda_id, kullanici_id, girisZamani, cikisZamani, musteriSayisi, cocukSayisi, odeme_id) VALUES (@oda_id, @kullanici_id, @girisZamani, @cikisZamani, @musteriSayisi, @cocukSayisi, @odeme_id)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oda_id", rezervasyon.oda.id);
                    command.Parameters.AddWithValue("@kullanici_id", rezervasyon.kullanici.id);
                    command.Parameters.AddWithValue("@girisZamani", rezervasyon.girisZamani);
                    command.Parameters.AddWithValue("@cikisZamani", rezervasyon.cikisZamani);
                    command.Parameters.AddWithValue("@musteriSayisi", rezervasyon.musteriSayisi);
                    command.Parameters.AddWithValue("@cocukSayisi", rezervasyon.cocukSayisi);
                    command.Parameters.AddWithValue("@odeme_id", rezervasyon.odeme.id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Rezervasyon> Listele()
        {
            List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM rezervasyonlar " +
                    "INNER JOIN odalar ON rezervasyonlar.oda_id = odalar.id " +
                    "INNER JOIN kullanicilar ON rezervasyonlar.kullanici_id = kullanicilar.id " +
                    "INNER JOIN odemeler ON rezervasyonlar.odeme_id = odemeler.id";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Rezervasyon rezervasyon = new Rezervasyon();
                        Oda oda = new Oda();
                        Kullanici kullanici = new Kullanici();
                        Odeme odeme = new Odeme();

                        Console.WriteLine(reader);

                        rezervasyon.id = Convert.ToInt32(reader["id"]);
                        rezervasyon.girisZamani = Convert.ToDateTime(reader["girisZamani"]);
                        rezervasyon.cikisZamani = Convert.ToDateTime(reader["cikisZamani"]);
                        rezervasyon.musteriSayisi = Convert.ToInt32(reader["musteriSayisi"]);
                        rezervasyon.cocukSayisi = Convert.ToInt32(reader["cocukSayisi"]);

                        oda.id = Convert.ToInt32(reader["oda_id"]);
                        oda.kapiNumarasi = reader["kapiNumarasi"].ToString();
                        oda.odaAdi = reader["odaAdi"].ToString();
                        oda.odaTuru = reader["odaTuru"].ToString();
                        oda.enFazlaMusteriSayisi = Convert.ToInt32(reader["enFazlaMusteriSayisi"]);
                        oda.enFazlaCocukSayisi = Convert.ToInt32(reader["enFazlaCocukSayisi"]);
                        oda.doluMu = Convert.ToBoolean(reader["doluMu"]);
                        rezervasyon.oda = oda;

                        kullanici.id = Convert.ToInt32(reader["kullanici_id"]);
                        kullanici.ad = reader["ad"].ToString();
                        kullanici.soyad = reader["soyad"].ToString();
                        kullanici.telefon = reader["telefon"].ToString();
                        kullanici.email = reader["email"].ToString();
                        kullanici.normalizedEmail = reader["normalizedEmail"].ToString();
                        kullanici.sifre = reader["sifre"].ToString();
                        kullanici.kullaniciTuru = (KullaniciTuru)Enum.Parse(typeof(KullaniciTuru), reader["kullaniciTuru"].ToString());
                        rezervasyon.kullanici = kullanici;

                        odeme.id = Convert.ToInt32(reader["odeme_id"]);
                        odeme.ucret = (double)Convert.ToDecimal(reader["ucret"]);
                        odeme.odemeTamamlandimi = Convert.ToBoolean(reader["odemeTamamlandimi"]);
                        odeme.kullanici = kullanici;
                        rezervasyon.odeme = odeme;

                        rezervasyonlar.Add(rezervasyon);
                    }
                    return rezervasyonlar;
                }
            }
        }

        public void Guncelle(Rezervasyon rezervasyon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE rezervasyonlar SET oda_id = @oda_id, kullanici_id = @kullanici_id, girisZamani = @girisZamani, cikisZamani = @cikisZamani, musteriSayisi = @musteriSayisi, cocukSayisi = @cocukSayisi, odeme_id = @odeme_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oda_id", rezervasyon.oda.id);
                    command.Parameters.AddWithValue("@kullanici_id", rezervasyon.kullanici.id);
                    command.Parameters.AddWithValue("@girisZamani", rezervasyon.girisZamani);
                    command.Parameters.AddWithValue("@cikisZamani", rezervasyon.cikisZamani);
                    command.Parameters.AddWithValue("@musteriSayisi", rezervasyon.musteriSayisi);
                    command.Parameters.AddWithValue("@cocukSayisi", rezervasyon.cocukSayisi);
                    command.Parameters.AddWithValue("@odeme_id", rezervasyon.odeme.id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Sil(Rezervasyon rezervasyon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM rezervasyonlar WHERE id=@id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", rezervasyon.id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SilOdayaGore(int odaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM rezervasyonlar WHERE oda_id=@oda_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oda_id", odaId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
