using OtobusBiletSistemi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobüsBilet
{
    public partial class MuhasebeEkran : Form
    {
        public MuhasebeEkran(string kullaniciAdi)
        {
            InitializeComponent();
        }

        private void MuhasebeEkran_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GiderFormu giderFormu = new GiderFormu();
            giderFormu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Grafikler Grafiklerform = new Grafikler();
            Grafiklerform.Show();
        }
    }
}
