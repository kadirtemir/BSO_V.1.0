using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace OtobüsBilet
{
    public partial class BiletEkleme : Form
    {
        public BiletEkleme()
        {
            InitializeComponent();
            LoadOtogarlar();
            LoadOtobusPlakalari();
        }

        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";

        private void LoadOtogarlar()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmdSetNames = new MySqlCommand("SET NAMES utf8", conn);
                    cmdSetNames.ExecuteNonQuery();

                    string query = "SELECT otogar_adi FROM istasyonlar";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string otogarAdi = reader.GetString("otogar_adi");
                        cmbKalkisOtogari.Items.Add(otogarAdi);
                        cmbVarisOtogari.Items.Add(otogarAdi);
                    }
                    reader.Close();
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void LoadOtobusPlakalari()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Arac_plakasi FROM otobüs_ekle";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            otobusPlakaCombx.Items.Add(reader["Arac_plakasi"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Otobüs_Plakası = otobusPlakaCombx.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(Otobüs_Plakası))
            {
                MessageBox.Show("Lütfen bir otobüs plakası seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string KalkisNoktasi = cmbKalkisOtogari.SelectedItem?.ToString();
            string VarisNoktasi = cmbVarisOtogari.SelectedItem?.ToString();
            string Otobüs_Hareket_Saati = haraketSaati.Text;
            string Otobüs_Varıs_Saati = varisSaati.Text;
            string Bilet_Fiyatı = biletFiyati.Text;

            if (string.IsNullOrWhiteSpace(Otobüs_Plakası))
            {
                MessageBox.Show("Otobüs plakasını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(KalkisNoktasi))
            {
                MessageBox.Show("Lütfen kalkış otogarını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(VarisNoktasi))
            {
                MessageBox.Show("Lütfen varış otogarını seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Otobüs_Hareket_Saati))
            {
                MessageBox.Show("Lütfen otobüs hareket saatini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Otobüs_Varıs_Saati))
            {
                MessageBox.Show("Lütfen otobüs varış saatini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Bilet_Fiyatı))
            {
                MessageBox.Show("Bilet fiyatını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bosKoltuklar = string.Join(",", Enumerable.Range(1, 46));

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT aktif FROM biletler WHERE otobus_plakasi = @otobusPlakasi";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@otobusPlakasi", Otobüs_Plakası);

                        object result = checkCmd.ExecuteScalar();
                        if (result != null && Convert.ToInt32(result) == 1)
                        {
                            MessageBox.Show("Bu otobüs plakası aktif durumdadır. Yeni bir bilet eklenemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO biletler (otobus_plakasi, kalkis_noktasi, varis_noktasi, hareket_saati, varis_saati, bilet_fiyati, aktif, bos_koltuklar) " +
                                         "VALUES (@otobusPlakasi, @kalkisNoktasi, @varisNoktasi, @hareketSaati, @varisSaati, @biletFiyati, @aktif, @bosKoltuklar)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@otobusPlakasi", Otobüs_Plakası);
                        cmd.Parameters.AddWithValue("@kalkisNoktasi", KalkisNoktasi);
                        cmd.Parameters.AddWithValue("@varisNoktasi", VarisNoktasi);
                        cmd.Parameters.AddWithValue("@hareketSaati", Otobüs_Hareket_Saati);
                        cmd.Parameters.AddWithValue("@varisSaati", Otobüs_Varıs_Saati);
                        cmd.Parameters.AddWithValue("@biletFiyati", Bilet_Fiyatı);
                        cmd.Parameters.AddWithValue("@aktif", 1);
                        cmd.Parameters.AddWithValue("@bosKoltuklar", bosKoltuklar);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} satır eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.ToString()}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

          
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void BiletEkleme_Load(object sender, EventArgs e)
        {

        }

        private void otobusPlakaCombx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
