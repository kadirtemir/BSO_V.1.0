namespace OtobusBiletSistemi
{
    partial class GiderFormu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtGiderAd = new System.Windows.Forms.TextBox();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.numGiderTutar = new System.Windows.Forms.NumericUpDown();
            this.dtpGiderTarihi = new System.Windows.Forms.DateTimePicker();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.lblGiderAd = new System.Windows.Forms.Label();
            this.lblTutar = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.lblAciklama = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGiderTutar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtGiderAd
            // 
            this.txtGiderAd.Location = new System.Drawing.Point(120, 30);
            this.txtGiderAd.Name = "txtGiderAd";
            this.txtGiderAd.Size = new System.Drawing.Size(200, 20);
            this.txtGiderAd.TabIndex = 0;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(120, 90);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(200, 20);
            this.txtAciklama.TabIndex = 3;
            // 
            // numGiderTutar
            // 
            this.numGiderTutar.DecimalPlaces = 2;
            this.numGiderTutar.Location = new System.Drawing.Point(120, 60);
            this.numGiderTutar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numGiderTutar.Name = "numGiderTutar";
            this.numGiderTutar.Size = new System.Drawing.Size(200, 20);
            this.numGiderTutar.TabIndex = 1;
            // 
            // dtpGiderTarihi
            // 
            this.dtpGiderTarihi.Location = new System.Drawing.Point(120, 120);
            this.dtpGiderTarihi.Name = "dtpGiderTarihi";
            this.dtpGiderTarihi.Size = new System.Drawing.Size(200, 20);
            this.dtpGiderTarihi.TabIndex = 2;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(120, 160);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(200, 40);
            this.btnKaydet.TabIndex = 4;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // lblGiderAd
            // 
            this.lblGiderAd.AutoSize = true;
            this.lblGiderAd.Location = new System.Drawing.Point(30, 30);
            this.lblGiderAd.Name = "lblGiderAd";
            this.lblGiderAd.Size = new System.Drawing.Size(58, 13);
            this.lblGiderAd.TabIndex = 5;
            this.lblGiderAd.Text = "Gider Adı:";
            // 
            // lblTutar
            // 
            this.lblTutar.AutoSize = true;
            this.lblTutar.Location = new System.Drawing.Point(30, 60);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(37, 13);
            this.lblTutar.TabIndex = 6;
            this.lblTutar.Text = "Tutar: ";
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.Location = new System.Drawing.Point(30, 120);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(37, 13);
            this.lblTarih.TabIndex = 7;
            this.lblTarih.Text = "Tarih: ";
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.Location = new System.Drawing.Point(30, 90);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(57, 13);
            this.lblAciklama.TabIndex = 8;
            this.lblAciklama.Text = "Açıklama: ";
            // 
            // GiderFormu
            // 
            this.ClientSize = new System.Drawing.Size(350, 220);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.lblTutar);
            this.Controls.Add(this.lblGiderAd);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.dtpGiderTarihi);
            this.Controls.Add(this.numGiderTutar);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.txtGiderAd);
            this.Name = "GiderFormu";
            this.Text = "Gider Girişi";
            ((System.ComponentModel.ISupportInitialize)(this.numGiderTutar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtGiderAd;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.NumericUpDown numGiderTutar;
        private System.Windows.Forms.DateTimePicker dtpGiderTarihi;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label lblGiderAd;
        private System.Windows.Forms.Label lblTutar;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label lblAciklama;
    }
}
