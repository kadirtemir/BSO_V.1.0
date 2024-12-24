using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobüsBilet
{
    public partial class musteriKayitEkran : Form
    {
        public musteriKayitEkran()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;";



        private void kaydolBtn_Click(object sender, EventArgs e)
        {
            string kAdi = kAdi_box.Text;
            string isim = isimBox.Text;
            string soyisim = soyİsimBox.Text;
            string sifre = sifreBox.Text;
            string tc = tcBox.Text;
            string telefon = telBox.Text;
            string eposta = ePostaBox.Text;
            string cinsiyet = cinsiyetBox.Text;

            if (string.IsNullOrWhiteSpace(kAdi) || string.IsNullOrWhiteSpace(isim) || string.IsNullOrWhiteSpace(soyisim) ||
                string.IsNullOrWhiteSpace(sifre) || string.IsNullOrWhiteSpace(tc))
            {
                MessageBox.Show("Lütfen tüm zorunlu alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tc.Length != 11 || !long.TryParse(tc, out _))
            {
                MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır ve yalnızca rakam içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO kullanicilar (kadi, sifre, isim, soyisim, h_turu, tc, eposta, telefon, cinsiyet) " +
                           "VALUES (@kadi, @sifre, @isim, @soyisim, 'musteri', @tc, @eposta, @telefon, @cinsiyet)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@kadi", kAdi);
                        command.Parameters.AddWithValue("@sifre", sifre);
                        command.Parameters.AddWithValue("@isim", isim);
                        command.Parameters.AddWithValue("@soyisim", soyisim);
                        command.Parameters.AddWithValue("@tc", tc);
                        command.Parameters.AddWithValue("@eposta", eposta);
                        command.Parameters.AddWithValue("@telefon", telefon);
                        command.Parameters.AddWithValue("@cinsiyet", cinsiyet);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kullanıcı başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearTextBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı kaydedilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearTextBoxes()
        {
            kAdi_box.Text = string.Empty;
            isimBox.Text = string.Empty;
            soyİsimBox.Text = string.Empty;
            sifreBox.Text = string.Empty;
            tcBox.Text = string.Empty;
            telBox.Text = string.Empty;
            ePostaBox.Text = string.Empty;
            cinsiyetBox.Text = string.Empty;
        }
    }



}


