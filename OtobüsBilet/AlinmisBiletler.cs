using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Otobüs_Bilet_Otomasyonu;

namespace OtobüsBilet
{
    public partial class AlinmisBiletler : Form
    {
        private string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";
        private string kullaniciAdi;

        public AlinmisBiletler(string tc)
        {
            InitializeComponent();
            this.kullaniciAdi = GetKullaniciAdiFromTc(tc);
            LoadBiletlerim();
        }

        private string GetKullaniciAdiFromTc(string tc)
        {
            string kAdi = "";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT kadi FROM kullanicilar WHERE tc = @tc";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tc", tc);
                    kAdi = cmd.ExecuteScalar()?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return kAdi;
        }

        private void LoadBiletlerim()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    b.id AS 'Bilet ID',
                    b.otobus_plakasi AS 'Otobüs Plakası', 
                    b.kalkis_noktasi AS 'Kalkış Noktası', 
                    b.varis_noktasi AS 'Varış Noktası', 
                    b.hareket_saati AS 'Hareket Saati', 
                    b.varis_saati AS 'Varış Saati', 
                    b.bilet_fiyati AS 'Bilet Fiyatı' 
                FROM alinan_biletler ab
                INNER JOIN biletler b ON ab.bilet_id = b.id
                INNER JOIN kullanicilar k ON ab.tc = k.tc
                WHERE k.kadi = @kullaniciAdi";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable biletTablosu = new DataTable();
                    adapter.Fill(biletTablosu);

                    dataGridView1.DataSource = biletTablosu;
                    dataGridView1.Columns["Bilet ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBiletIptal_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var result = MessageBox.Show("Seçili bileti iptal etmek istediğinizden emin misiniz?", "Bilet İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int biletId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Bilet ID"].Value);

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();

                            string queryGetTc = "SELECT tc FROM kullanicilar WHERE kadi = @kullaniciAdi";
                            MySqlCommand cmdGetTc = new MySqlCommand(queryGetTc, conn);
                            cmdGetTc.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);

                            string tc = cmdGetTc.ExecuteScalar()?.ToString();

                            if (!string.IsNullOrEmpty(tc))
                            {
                                string queryDelete = "DELETE FROM alinan_biletler WHERE bilet_id = @biletId AND tc = @tc";
                                MySqlCommand cmdDelete = new MySqlCommand(queryDelete, conn);
                                cmdDelete.Parameters.AddWithValue("@biletId", biletId);
                                cmdDelete.Parameters.AddWithValue("@tc", tc);

                                int rowsAffected = cmdDelete.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Bilet başarıyla iptal edildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadBiletlerim();
                                }
                                else
                                {
                                    MessageBox.Show("Bilet iptali başarısız oldu. Bilgileri kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı adı için TC bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen iptal etmek istediğiniz bileti seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Biletlerim_Load_1(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
 
    }
}
