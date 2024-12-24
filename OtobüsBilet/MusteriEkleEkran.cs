using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OtobüsBilet
{
    public partial class MusteriEkleEkran : Form
    {
        private string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;";
        private MySqlCommand komut;
        private MySqlDataAdapter da;
        private DataSet ds;

        public MusteriEkleEkran()
        {
            InitializeComponent();
            doldur();
        }

        private void doldur()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                da = new MySqlDataAdapter("SELECT * FROM kullanicilar", conn);
                ds = new DataSet();
                da.Fill(ds, "kullanicilar");
                dataGridView1.DataSource = ds.Tables["kullanicilar"];
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSoyadi.Text) ||
                string.IsNullOrWhiteSpace(txtTel.Text) ||
                string.IsNullOrWhiteSpace(txtTC.Text) ||
                string.IsNullOrWhiteSpace(cmbCinsiyet.Text) ||
                string.IsNullOrWhiteSpace(hturuCombobox.Text) ||
                string.IsNullOrWhiteSpace(sifreBox.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            string kadi = txtAdi.Text + txtSoyadi.Text + txtTC.Text.Substring(0, 2);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM kullanicilar WHERE tc = @tc OR kadi = @kadi";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@tc", txtTC.Text);
                    checkCmd.Parameters.AddWithValue("@kadi", kadi);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                        return;
                }

                string query = @"INSERT INTO kullanicilar (kadi, sifre, isim, soyisim, h_turu, tc, eposta, telefon, cinsiyet) 
                                 VALUES (@kadi, @sifre, @isim, @soyisim, @h_turu, @tc, @eposta, @telefon, @cinsiyet)";
                using (MySqlCommand komut = new MySqlCommand(query, conn))
                {
                    komut.Parameters.AddWithValue("@kadi", kadi);
                    komut.Parameters.AddWithValue("@sifre", sifreBox.Text);
                    komut.Parameters.AddWithValue("@isim", txtAdi.Text);
                    komut.Parameters.AddWithValue("@soyisim", txtSoyadi.Text);
                    komut.Parameters.AddWithValue("@h_turu", hturuCombobox.Text);
                    komut.Parameters.AddWithValue("@tc", txtTC.Text);
                    komut.Parameters.AddWithValue("@eposta", textBox1.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                    komut.ExecuteNonQuery();
                }

                doldur();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string tcKimlik = dataGridView1.SelectedRows[0].Cells["tc"].Value.ToString();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM kullanicilar WHERE tc = @tc";
                    komut = new MySqlCommand(query, conn);
                    komut.Parameters.AddWithValue("@tc", tcKimlik);
                    komut.ExecuteNonQuery();
                    doldur();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE kullanicilar SET 
                                isim = @isim, soyisim = @soyisim, 
                                telefon = @telefon, cinsiyet = @cinsiyet, eposta = @eposta
                                WHERE tc = @tc";
                komut = new MySqlCommand(query, conn);
                komut.Parameters.AddWithValue("@isim", txtAdi.Text);
                komut.Parameters.AddWithValue("@soyisim", txtSoyadi.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                komut.Parameters.AddWithValue("@tc", txtTC.Text);
                komut.Parameters.AddWithValue("@eposta", textBox1.Text);
                komut.ExecuteNonQuery();
                doldur();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtTC.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdi.Text = dataGridView1.CurrentRow.Cells["isim"].Value.ToString();
            txtSoyadi.Text = dataGridView1.CurrentRow.Cells["soyisim"].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["eposta"].Value.ToString();
            cmbCinsiyet.Text = dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString();
            hturuCombobox.Text = dataGridView1.CurrentRow.Cells["h_turu"].Value.ToString();
        }

        private void MusteriEkleEkran_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void MusteriEkrani_Load(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void txtAdi_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

    }
}
