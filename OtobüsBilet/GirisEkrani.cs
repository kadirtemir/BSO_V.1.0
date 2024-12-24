using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobüsBilet
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            BaglantiTest();
            InitializeComponent();
        }

        private void BaglantiTest()
        {
            string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKadi.Text;
            string sifre = txtPass.Text;

            string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;";

            string query = "SELECT h_turu, tc FROM kullanicilar WHERE kadi = @kadi AND sifre = @sifre";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@kadi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string hTuru = reader["h_turu"].ToString();

                            if (hTuru == "muhasebe")
                            {
                                MuhasebeEkran muhasebeForm = new MuhasebeEkran(kullaniciAdi);
                                muhasebeForm.Show();
                                this.Hide();
                            }
                            else if (hTuru == "biletci")
                            {
                                AdminEkran adminForm = new AdminEkran(kullaniciAdi);
                                adminForm.Show();
                                this.Hide();
                            }
                            else if (hTuru == "musteri")
                            {
                                string tc = reader["tc"].ToString();  
                                MusteriEkran musteriForm = new MusteriEkran(tc);  
                                musteriForm.Show();
                                this.Hide();
                            }

              
                            else
                            {
                                MessageBox.Show("Bilinmeyen kullanıcı türü.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Malumatcı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {

        }

        private void kaydolBtn_Click(object sender, EventArgs e)
        {
            musteriKayitEkran musteriKayitForm = new musteriKayitEkran();
            musteriKayitForm.Show();
        }
    }
}
