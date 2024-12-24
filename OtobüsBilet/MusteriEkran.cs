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
    public partial class MusteriEkran : Form
    {
        private string tc;
        public MusteriEkran(string tc)
        {
            InitializeComponent();
            this.tc = tc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MevcutBiletler bilet = new OtobüsBilet.MevcutBiletler(tc);
            bilet.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlinmisBiletler alinmisBilet = new OtobüsBilet.AlinmisBiletler(tc);
            alinmisBilet.ShowDialog();
        }

        private void MusteriEkran_Load(object sender, EventArgs e)
        {

        }
    }
}
