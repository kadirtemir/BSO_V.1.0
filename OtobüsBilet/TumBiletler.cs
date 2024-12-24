using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobüsBilet
{
    public partial class TumBiletler : Form
    {
        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";

        public TumBiletler()
        {
            InitializeComponent();
            LoadTumBiletler();
        }

        private void LoadTumBiletler()
        
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                otobus_plakasi AS 'Otobüs Plakası', 
                                kalkis_noktasi AS 'Kalkış Noktası', 
                                varis_noktasi AS 'Varış Noktası', 
                                hareket_saati AS 'Hareket Saati', 
                                varis_saati AS 'Varış Saati', 
                                bilet_fiyati AS 'Bilet Fiyatı',
                                isim AS 'İsim',
                                soyisim AS 'Soyisim'
                             FROM 
                                biletler b
                             JOIN 
                                kullanicilar k ON tc = tc";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable biletTablosu = new DataTable();
                    adapter.Fill(biletTablosu);
                    dataGridView1.DataSource = biletTablosu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void TumBiletler_Load(object sender, EventArgs e)
        {
        }
    }
}
