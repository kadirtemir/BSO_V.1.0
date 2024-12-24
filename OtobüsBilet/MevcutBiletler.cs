using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace OtobüsBilet
{
    public partial class MevcutBiletler : Form
    {
        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";

        private string tc;
        public MevcutBiletler(string tc)
        {

            InitializeComponent();
            this.tc = tc; 
            LoadBiletler();
        }


        private void LoadBiletler()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                id AS 'Bilet ID',
                                otobus_plakasi AS 'Otobüs Plakası', 
                                kalkis_noktasi AS 'Kalkış Noktası', 
                                varis_noktasi AS 'Varış Noktası', 
                                hareket_saati AS 'Hareket Saati', 
                                varis_saati AS 'Varış Saati', 
                                bilet_fiyati AS 'Bilet Fiyatı' 
                             FROM biletler";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int biletId = Convert.ToInt32(row.Cells["Bilet ID"].Value);
                string otobusPlakasi = row.Cells["Otobüs Plakası"].Value.ToString();
                string kalkisNoktasi = row.Cells["Kalkış Noktası"].Value.ToString();
                string varisNoktasi = row.Cells["Varış Noktası"].Value.ToString();
                string hareketSaati = row.Cells["Hareket Saati"].Value.ToString();
                string varisSaati = row.Cells["Varış Saati"].Value.ToString();
                string biletFiyati = row.Cells["Bilet Fiyatı"].Value.ToString();

                GosterSeciliBilet(otobusPlakasi, kalkisNoktasi, varisNoktasi, hareketSaati, varisSaati, biletFiyati);
                string selectedSeat = comboBox1.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedSeat))
                {
                    if (!IsSeatAvailable(biletId, selectedSeat))
                    {
                        MessageBox.Show("Seçilen koltuk dolu. Lütfen başka bir koltuk seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    AlinanBiletKaydet(biletId);
                    UpdateBoşKoltuklar(biletId, selectedSeat);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir bilet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsSeatAvailable(int biletId, string selectedSeat)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT bos_koltuklar FROM biletler WHERE id = @biletId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@biletId", biletId);

                    string bosKoltuklar = cmd.ExecuteScalar()?.ToString();
                    if (string.IsNullOrEmpty(bosKoltuklar))
                    {
                        MessageBox.Show("Koltuk bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    List<int> bosKoltukListesi = bosKoltuklar.Split(',').Select(int.Parse).ToList();
                    int selectedSeatInt = Convert.ToInt32(selectedSeat);

                    return bosKoltukListesi.Contains(selectedSeatInt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UpdateBoşKoltuklar(int biletId, string selectedSeat)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT bos_koltuklar FROM biletler WHERE id = @biletId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@biletId", biletId);

                    string bosKoltuklar = cmd.ExecuteScalar()?.ToString();
                    if (string.IsNullOrEmpty(bosKoltuklar))
                    {
                        MessageBox.Show("Koltuk bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    List<int> bosKoltukListesi = bosKoltuklar.Split(',').Select(int.Parse).ToList();
                    int selectedSeatInt = Convert.ToInt32(selectedSeat);

                    if (bosKoltukListesi.Contains(selectedSeatInt))
                    {
                        bosKoltukListesi.Remove(selectedSeatInt);
                        string updatedBosKoltuklar = string.Join(",", bosKoltukListesi);

                        string updateQuery = @"UPDATE biletler SET bos_koltuklar = @bosKoltuklar WHERE id = @biletId";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@bosKoltuklar", updatedBosKoltuklar);
                        updateCmd.Parameters.AddWithValue("@biletId", biletId);

                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Seçilen koltuk zaten dolu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AlinanBiletKaydet(int biletId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = @"SELECT COUNT(*) FROM alinan_biletler 
                                  WHERE tc = @tc AND bilet_id = @bilet_id";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@tc", tc);
                    checkCmd.Parameters.AddWithValue("@bilet_id", biletId);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Bu bileti zaten almışsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = @"INSERT INTO alinan_biletler (tc, bilet_id, alis_tarihi) 
                             VALUES (@tc, @bilet_id, NOW())";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tc", tc);
                    cmd.Parameters.AddWithValue("@bilet_id", biletId);

                    MySqlTransaction transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    MessageBox.Show("Bilet başarıyla alındı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GosterSeciliBilet(string otobusPlakasi, string kalkisNoktasi, string varisNoktasi, string hareketSaati, string varisSaati, string biletFiyati)
        {
            MessageBox.Show($"Otobüs Plakası: {otobusPlakasi}\nKalkış Noktası: {kalkisNoktasi}\nVarış Noktası: {varisNoktasi}\nHareket Saati: {hareketSaati}\nVarış Saati: {varisSaati}\nBilet Fiyatı: {biletFiyati}");
        }
        private void MevcutBiletler_Load(object sender, EventArgs e)
        {
            this.Width = 1200;
            this.Height = 600;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int biletId = Convert.ToInt32(row.Cells["Bilet ID"].Value);
                UpdateSeatButtons(biletId);

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void UpdateSeatButtons(int biletId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT bos_koltuklar FROM biletler WHERE id = @biletId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@biletId", biletId);

                    string bosKoltuklar = cmd.ExecuteScalar()?.ToString();
                    if (string.IsNullOrEmpty(bosKoltuklar))
                    {
                        MessageBox.Show("Koltuk bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    List<int> bosKoltukListesi = bosKoltuklar.Split(',').Select(int.Parse).ToList();

                    for (int i = 1; i <= 46; i++)
                    {
                        
                        Button btnKoltuk = (Button)this.Controls.Find($"btnKoltuk{i}", true).FirstOrDefault();
                        if (btnKoltuk != null)
                        {
                            if (bosKoltukListesi.Contains(i))
                            {
                                btnKoltuk.BackColor = Color.Green;
                                btnKoltuk.Enabled = true;
                            }
                            else
                            {
                                btnKoltuk.BackColor = Color.Red;
                                btnKoltuk.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
