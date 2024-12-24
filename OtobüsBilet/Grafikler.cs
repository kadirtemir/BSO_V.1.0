using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OxyPlot;
using OxyPlot.Series;

namespace OtobüsBilet
{
    public partial class Grafikler : Form
    {
        public Grafikler()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
        }

        private void Grafikler_Load(object sender, EventArgs e)
        {
            DrawPieChart();
        }

        private void DrawPieChart()
        {
            string connectionString = "Server=46.1.54.221;Database=otomasyon;User ID=otomasyon;Password=test;Charset=utf8;";
            string query = "SELECT b.kalkis_noktasi, b.varis_noktasi, SUM(b.bilet_fiyati) AS ToplamTutar " +
                           "FROM alinan_biletler ab " +
                           "JOIN biletler b ON ab.bilet_id = b.id " +
                           "GROUP BY b.kalkis_noktasi, b.varis_noktasi";

            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı bağlantısında bir hata oluştu: " + ex.Message);
                return;
            }

            var plotModel = new PlotModel { Title = "Bilet Fiyatı Dağılımı", TextColor = OxyColors.Black };
            var pieSeries = new PieSeries
            {
                AngleSpan = 360,
                StartAngle = 0,
                StrokeThickness = 0.5
            };

            Random rand = new Random();

            foreach (DataRow row in dataTable.Rows)
            {
                string kalkisNoktasi = row["kalkis_noktasi"].ToString().Split(' ')[0];
                string varisNoktasi = row["varis_noktasi"].ToString().Split(' ')[0];
                string rota = kalkisNoktasi + " - " + varisNoktasi;
                decimal toplamTutar = Convert.ToDecimal(row["ToplamTutar"]);

                var color = OxyColor.FromRgb((byte)rand.Next(0, 128), (byte)rand.Next(0, 128), (byte)rand.Next(0, 128));

                pieSeries.Slices.Add(new PieSlice(rota, (double)toplamTutar)
                {
                    IsExploded = false,
                    Fill = color
                });
            }

            plotModel.Series.Add(pieSeries);

            var plotView = new OxyPlot.WindowsForms.PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel
            };
            this.Controls.Add(plotView);
        }
    }
}
