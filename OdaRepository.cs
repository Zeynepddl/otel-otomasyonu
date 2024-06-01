// ZEYNEP DAYAL - 262284037
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelYonetimi
{
    internal class OdaRepository
    {
        private string _connectionString;
        private static OdaRepository _instance;
        private RezervasyonRepository _rezervasyonRepository;
        public OdaRepository(string connectionString)
        {
            _connectionString = connectionString;
            _rezervasyonRepository = RezervasyonRepository.GetInstance();

        }

        public static OdaRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new OdaRepository("Data Source=DESKTOP-0K6KJK2\\SQLEXPRESS;Initial Catalog=db_OtelOtomasyonu;Integrated Security=True;");
            }
            return _instance;
        }
        public void Ekle(Oda oda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "insert into odalar(kapiNumarasi,odaAdi,odaTuru,enFazlaMusteriSayisi,enFazlaCocukSayisi,doluMu) values (@kapiNumarasi,@odaAdi,@odaTuru,@enFazlaMusteriSayisi,@enFazlaCocukSayisi,@doluMu) ";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kapiNumarasi", oda.kapiNumarasi);
                    command.Parameters.AddWithValue("@odaAdi", oda.odaAdi);
                    command.Parameters.AddWithValue("@odaTuru", oda.odaTuru);
                    command.Parameters.AddWithValue("@enFazlaMusteriSayisi", oda.enFazlaMusteriSayisi);
                    command.Parameters.AddWithValue("@enFazlaCocukSayisi", oda.enFazlaCocukSayisi);
                    command.Parameters.AddWithValue("@doluMu", oda.doluMu?1:0);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Oda> Listele()
        {
            List<Oda> odalar = new List<Oda>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "select * from odalar";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader=command.ExecuteReader();
                    while (reader.Read())
                    {
                        Oda oda = new Oda();
                        oda.id = Convert.ToInt32(reader["id"]);
                        oda.kapiNumarasi = reader["kapiNumarasi"].ToString();
                        oda.odaAdi = reader["odaAdi"].ToString();
                        oda.odaTuru = reader["odaTuru"].ToString();
                        oda.enFazlaMusteriSayisi = Convert.ToInt32(reader["enFazlaMusteriSayisi"]);
                        oda.enFazlaCocukSayisi= Convert.ToInt32(reader["enFazlaCocukSayisi"]);
                        oda.doluMu= Convert.ToBoolean(reader["doluMu"]);
                        odalar.Add(oda);
                    }
                    return odalar;
                }
            }
        }
        public void Guncelle(Oda oda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "Update odalar set kapiNumarasi=@kapiNumarasi,odaAdi=@odaAdi,odaTuru=@odaTuru,enFazlaMusteriSayisi=@enFazlaMusteriSayisi,enFazlaCocukSayisi=@enFazlaCocukSayisi,doluMu=@doluMu where id=@id" ;
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id",oda.id);
                    command.Parameters.AddWithValue("@kapiNumarasi",oda.kapiNumarasi);
                    command.Parameters.AddWithValue("@odaAdi",oda.odaAdi);
                    command.Parameters.AddWithValue("@odaTuru",oda.odaTuru);
                    command.Parameters.AddWithValue("@enFazlaMusteriSayisi",oda.enFazlaMusteriSayisi);
                    command.Parameters.AddWithValue("@enFazlaCocukSayisi",oda.enFazlaCocukSayisi);
                    command.Parameters.AddWithValue("@doluMu",oda.doluMu);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Sil(Oda oda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Öncelikle, oda ile ilişkili rezervasyonları sil
                _rezervasyonRepository.SilOdayaGore(oda.id);
                string query = "delete from odalar where id=@id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id",oda.id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
