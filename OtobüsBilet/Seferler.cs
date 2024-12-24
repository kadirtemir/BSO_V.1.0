using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OtobüsBilet
{
    public partial class Seferler : Form
    {
        public Seferler()
        {
            InitializeComponent();
        }

        private string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;";

        private void Seferler_Load(object sender, EventArgs e)
        {
            LoadTickets();
            LoadAracPlaka();
        }

        private void LoadTickets()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT * FROM biletler", connection);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "biletler");
                    dataGridView.DataSource = dataSet.Tables["biletler"];
                    dataGridView.Columns[0].Visible = false;

                    MySqlCommand countCommand = new MySqlCommand("SELECT COUNT(*) FROM biletler", connection);
                    int ticketCount = Convert.ToInt32(countCommand.ExecuteScalar());
                    lblYolcu.Text = $"Toplam Bilet Sayısı: {ticketCount}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void LoadAracPlaka()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT arac_plakasi FROM otobüs_ekle", connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        aracPlaka.Items.Add(reader["arac_plakasi"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading vehicle plates: " + ex.Message);
                }
            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(aracPlaka.Text) || string.IsNullOrEmpty(fiyat.Text) || string.IsNullOrEmpty(cmbNereden.Text) || string.IsNullOrEmpty(cmbNereye.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                DateTime kalkisTarihi = dtTarih.Value;
                string[] saatDakika = textBox1.Text.Split(':');
                if (saatDakika.Length == 2)
                {
                    kalkisTarihi = new DateTime(kalkisTarihi.Year, kalkisTarihi.Month, kalkisTarihi.Day, int.Parse(saatDakika[0]), int.Parse(saatDakika[1]), 0);
                }

                string varisSaati = GetVarisSaati(cmbNereden.Text, cmbNereye.Text, kalkisTarihi, connection);
                string bosKoltuklar = string.Join(",", Enumerable.Range(1, 46));

                MySqlCommand command = new MySqlCommand("INSERT INTO biletler (otobus_plakasi, kalkis_noktasi, varis_noktasi, hareket_saati, varis_saati, bilet_fiyati, bos_koltuklar) VALUES (@plaka, @kalkis, @varis, @haraket, @varisSaati, @fiyat, @bosKoltuklar)", connection);
                command.Parameters.AddWithValue("@plaka", aracPlaka.Text);
                command.Parameters.AddWithValue("@kalkis", cmbNereden.Text);
                command.Parameters.AddWithValue("@varis", cmbNereye.Text);
                command.Parameters.AddWithValue("@haraket", kalkisTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@varisSaati", varisSaati);
                command.Parameters.AddWithValue("@fiyat", fiyat.Text);
                command.Parameters.AddWithValue("@bosKoltuklar", bosKoltuklar);
                command.ExecuteNonQuery();

                MessageBox.Show("Yeni bilet eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTickets();
            }
        }

        private string GetVarisSaati(string kalkis, string varis, DateTime hareketSaati, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("SELECT otobus_sure FROM il_mesafeleri WHERE kalkis_noktasi = @kalkis AND varis_noktasi = @varis", connection);
            command.Parameters.AddWithValue("@kalkis", kalkis);
            command.Parameters.AddWithValue("@varis", varis);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                int otobusSure = Convert.ToInt32(result);
                DateTime varisSaati = hareketSaati.AddHours(otobusSure);
                return varisSaati.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return string.Empty;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                int ticketId = Convert.ToInt32(row.Cells[0].Value);
                MessageBox.Show($"Seçilen Bilet ID: {ticketId}", "Bilet ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];
                int ticketId = Convert.ToInt32(selectedRow.Cells[0].Value);

                DialogResult result = MessageBox.Show($"Bilet ID {ticketId} silinsin mi?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("DELETE FROM biletler WHERE id = @ticketId", connection);
                        command.Parameters.AddWithValue("@ticketId", ticketId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Bilet başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTickets();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir bilet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
