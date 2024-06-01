﻿// ZEYNEP DAYAL - 262284037
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
        private KullaniciRepository kullaniciRepository;
        public Uyeler()
        {
            InitializeComponent();
            kullaniciRepository = KullaniciRepository.GetInstance();
        }
        void kullaniciListele(KullaniciTuru kullaniciTuru)
        {
            List<Kullanici> kullanicilar = kullaniciRepository.Listele(kullaniciTuru);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Ad", typeof(string));
            dt.Columns.Add("Soyad",typeof(string));
            dt.Columns.Add("Telefon", typeof(string));
            dt.Columns.Add("Email",typeof(string));
            dt.Columns.Add("NormalizedEmail",typeof(string));
            dt.Columns.Add("Sifre",typeof(string));
            dt.Columns.Add("KullaniciTuru",typeof(bool));

            foreach(Kullanici kullanici in kullanicilar) {
                DataRow row = dt.NewRow();
                row["ID"] = kullanici.id;
                row["Ad"] = kullanici.ad;
                row["Soyad"] = kullanici.soyad;
                row["Telefon"] = kullanici.telefon;
                row["Email"] = kullanici.email;
                row["Sifre"] = kullanici.sifre;
                row["NormalizedEmail"] = kullanici.normalizedEmail;
                row["KullaniciTuru"] = kullanici.kullaniciTuru;

                dt.Rows.Add(row);
            }
            
            dataGridView1.DataSource = dt;
        }
        private void Uyeler_Load(object sender, EventArgs e)
        {
            kullaniciListele(KullaniciTuru.personel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    try
                    {
                        int kullaniciId = Convert.ToInt32(row.Cells["id"].Value);
                        kullaniciRepository.KullaniciSil(kullaniciId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
                kullaniciListele(KullaniciTuru.personel);
                MessageBox.Show("Kullanıcı silindi.");
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir kullanıcı seçin.");
            }



        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Kullanici kullanici = new Kullanici();
                kullanici.id = Convert.ToInt32(selectedRow.Cells["id"].Value);
                kullanici.ad = Convert.ToString(selectedRow.Cells["ad"].Value);
                kullanici.soyad = Convert.ToString(selectedRow.Cells["soyad"].Value);
                kullanici.telefon = Convert.ToString(selectedRow.Cells["telefon"].Value);
                kullanici.email = Convert.ToString(selectedRow.Cells["email"].Value);
                kullanici.normalizedEmail = Convert.ToString(selectedRow.Cells["normalizedEmail"].Value);
                kullanici.sifre = Convert.ToString(selectedRow.Cells["sifre"].Value);
                kullanici.kullaniciTuru = KullaniciTuru.personel;


                bool telefonVar = kullaniciRepository.TelefonVarMi(kullanici.telefon, kullanici.id);
                bool emailVar = kullaniciRepository.EmailVarMi(kullanici.email, kullanici.id);

                if (telefonVar)
                {
                    MessageBox.Show("Bu telefon numarası zaten kayıtlı.");
                    return;
                }

                if (emailVar)
                {
                    MessageBox.Show("Bu e-posta adresi zaten kayıtlı.");
                    return;
                }

                kullaniciRepository.Guncelle(kullanici);


                MessageBox.Show("Kullanıcı bilgileri güncellendi.");
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir kullanıcı seçin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YeniPersonel yeniPersonel = new YeniPersonel();
            yeniPersonel.FormClosed += YeniPersonel_FormClosed; // Form kapandığında çalışacak olay
            yeniPersonel.ShowDialog();
        }
        private void YeniPersonel_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Form kapandığında DataGridView'i güncelle
            kullaniciListele(KullaniciTuru.personel);
        }
    }
    
}
