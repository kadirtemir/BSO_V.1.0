using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobusBilet
{
    public partial class OtobusListesi : Form
    {
        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";

        public OtobusListesi()
        {
            InitializeComponent();
        }

        private void OtobusListesi_Load(object sender, EventArgs e)
        {
            OtobusleriYukle();
        }

        private void OtobusleriYukle()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                     Arac_plakasi AS 'Otobüs Plakası',
                                     Arac_markasi AS 'Marka', 
                                     Arac_modeli AS 'Model', 
                                     Yolcu_kapasitesi AS 'Kapasite'
                                     FROM otobüs_ekle";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable otobusTablosu = new DataTable();
                    adapter.Fill(otobusTablosu);

                    dataGridView1.DataSource = otobusTablosu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata Detayı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            OtobusleriYukle();
        }
    }
}
