using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace OtobusBiletSistemi
{
    public partial class GiderFormu : Form
    {
        // MySQL bağlantı dizesi
        string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8mb4;";

        public GiderFormu()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Formdan alınan veriler
            string giderAd = txtGiderAd.Text;
            decimal giderTutar = numGiderTutar.Value;
            DateTime giderTarihi = dtpGiderTarihi.Value;
            string aciklama = txtAciklama.Text;

            // Veri kontrolü (Gider adı ve tutarının boş olmaması)
            if (string.IsNullOrEmpty(giderAd) || giderTutar <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir gider adı ve tutarı giriniz.");
                return;
            }

            // MySQL'e bağlanma ve veri kaydetme
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Giderler (GiderAd, GiderTutar, GiderTarihi, Aciklama) " +
                                   "VALUES (@GiderAd, @GiderTutar, @GiderTarihi, @Aciklama)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Parametreleri ekle
                        cmd.Parameters.AddWithValue("@GiderAd", giderAd);
                        cmd.Parameters.AddWithValue("@GiderTutar", giderTutar);
                        cmd.Parameters.AddWithValue("@GiderTarihi", giderTarihi);
                        cmd.Parameters.AddWithValue("@Aciklama", aciklama);

                        // Sorguyu çalıştır ve kaydet
                        cmd.ExecuteNonQuery();
                    }
                }

                // Başarı mesajı ve form temizleme
                MessageBox.Show("Gider başarıyla kaydedildi.");
                ClearFields();
            }
            catch (MySqlException ex)
            {
                // MySQL hatası durumunda
                MessageBox.Show("Veritabanına bağlanırken bir hata oluştu: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Genel hata durumu
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        // Formdaki alanları temizleme
        private void ClearFields()
        {
            txtGiderAd.Clear();
            txtAciklama.Clear();
            numGiderTutar.Value = 0;
            dtpGiderTarihi.Value = DateTime.Now;
        }
    }
}
