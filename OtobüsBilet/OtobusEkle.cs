using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobusBilet
{
    public partial class OtobusEkle : Form
    {
        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";

        public OtobusEkle()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaka.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtDonanım.Text) ||
                string.IsNullOrWhiteSpace(txtKapasite.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO otobüs_ekle (Arac_plakasi,Arac_markasi , Arac_modeli, Arac_donanimi, Yolcu_kapasitesi) 
                                     VALUES (@plaka,@marka, @model, @donanım, @kapasite)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@plaka", txtPlaka.Text.Trim());
                    cmd.Parameters.AddWithValue("@marka", txtMarka.Text.Trim());
                    cmd.Parameters.AddWithValue("@model", txtModel.Text.Trim());
                    cmd.Parameters.AddWithValue("@donanım", txtDonanım.Text.Trim());
                    cmd.Parameters.AddWithValue("@kapasite", int.Parse(txtKapasite.Text.Trim()));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Otobüs başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Form Temizleme
                    txtPlaka.Clear();
                    txtModel.Clear();
                    txtDonanım.Clear();
                    txtKapasite.Clear();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblModel_Click(object sender, EventArgs e)
        {

        }

        private void OtobusEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
