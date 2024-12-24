
using OtobusBilet;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace OtobüsBilet
{
    public partial class AdminEkran : Form
    {
        private string kullaniciAdi;

        public AdminEkran(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            label1.Text = $"Hoşgeldin {kullaniciAdi}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Seferler bilet = new OtobüsBilet.Seferler();
            bilet.ShowDialog();
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            MusteriEkleEkran musteri = new OtobüsBilet.MusteriEkleEkran();
            musteri.Show();
        }

        private void btnBiletler_Click(object sender, EventArgs e)
        {
            MevcutBiletler biletekran = new OtobüsBilet.MevcutBiletler(kullaniciAdi);
            biletekran.Show();
        }

 

        private void button2_Click(object sender, EventArgs e)
        {
            GirisEkrani ge = new GirisEkrani();
            ge.Show();
            this.Close();
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            AlinmisBiletler biletlerim = new AlinmisBiletler(kullaniciAdi);
            biletlerim.Show();
            this.Close();
        }

        

        private void button2_Click_2(object sender, EventArgs e)
        {
            TumBiletler tumBiletlerForm = new TumBiletler();
            tumBiletlerForm.ShowDialog();
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            OtobusEkle otobusEkleForm = new OtobusEkle();
            otobusEkleForm.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
         
            OtobusListesi otobusListesiForm = new OtobusListesi();
            otobusListesiForm.Show();
        }

    }
}




