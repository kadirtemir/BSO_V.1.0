namespace OtobusBilet
{
    partial class OtobusEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OtobusEkle));
            this.lblPlaka = new System.Windows.Forms.Label();
            this.txtPlaka = new System.Windows.Forms.TextBox();
            this.lblMarka = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtDonanım = new System.Windows.Forms.TextBox();
            this.lblKapasite = new System.Windows.Forms.Label();
            this.txtKapasite = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMarka = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPlaka
            // 
            this.lblPlaka.AutoSize = true;
            this.lblPlaka.Location = new System.Drawing.Point(17, 34);
            this.lblPlaka.Name = "lblPlaka";
            this.lblPlaka.Size = new System.Drawing.Size(101, 16);
            this.lblPlaka.TabIndex = 0;
            this.lblPlaka.Text = "Otobüs Plakası:";
            // 
            // txtPlaka
            // 
            this.txtPlaka.Location = new System.Drawing.Point(137, 34);
            this.txtPlaka.Name = "txtPlaka";
            this.txtPlaka.Size = new System.Drawing.Size(150, 22);
            this.txtPlaka.TabIndex = 1;
            // 
            // lblMarka
            // 
            this.lblMarka.AutoSize = true;
            this.lblMarka.Location = new System.Drawing.Point(17, 110);
            this.lblMarka.Name = "lblMarka";
            this.lblMarka.Size = new System.Drawing.Size(48, 16);
            this.lblMarka.TabIndex = 2;
            this.lblMarka.Text = "Model:";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(137, 110);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(150, 22);
            this.txtModel.TabIndex = 3;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(17, 150);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(98, 16);
            this.lblModel.TabIndex = 4;
            this.lblModel.Text = "Araç Donanımı:";
            this.lblModel.Click += new System.EventHandler(this.lblModel_Click);
            // 
            // txtDonanım
            // 
            this.txtDonanım.Location = new System.Drawing.Point(137, 150);
            this.txtDonanım.Name = "txtDonanım";
            this.txtDonanım.Size = new System.Drawing.Size(150, 22);
            this.txtDonanım.TabIndex = 5;
            // 
            // lblKapasite
            // 
            this.lblKapasite.AutoSize = true;
            this.lblKapasite.Location = new System.Drawing.Point(17, 190);
            this.lblKapasite.Name = "lblKapasite";
            this.lblKapasite.Size = new System.Drawing.Size(62, 16);
            this.lblKapasite.TabIndex = 6;
            this.lblKapasite.Text = "kapasite:";
            // 
            // txtKapasite
            // 
            this.txtKapasite.Location = new System.Drawing.Point(137, 190);
            this.txtKapasite.Name = "txtKapasite";
            this.txtKapasite.Size = new System.Drawing.Size(150, 22);
            this.txtKapasite.TabIndex = 7;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(137, 230);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(150, 30);
            this.btnEkle.TabIndex = 8;
            this.btnEkle.Text = "Otobüs Ekle";
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Marka:";
            // 
            // txtMarka
            // 
            this.txtMarka.Location = new System.Drawing.Point(137, 72);
            this.txtMarka.Name = "txtMarka";
            this.txtMarka.Size = new System.Drawing.Size(150, 22);
            this.txtMarka.TabIndex = 10;
            // 
            // OtobusEkle
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(434, 410);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMarka);
            this.Controls.Add(this.lblPlaka);
            this.Controls.Add(this.txtPlaka);
            this.Controls.Add(this.lblMarka);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.txtDonanım);
            this.Controls.Add(this.lblKapasite);
            this.Controls.Add(this.txtKapasite);
            this.Controls.Add(this.btnEkle);
            this.DoubleBuffered = true;
            this.Name = "OtobusEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otobüs Ekle";
            this.Load += new System.EventHandler(this.OtobusEkle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblPlaka;
        private System.Windows.Forms.TextBox txtPlaka;
        private System.Windows.Forms.Label lblMarka;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtDonanım;
        private System.Windows.Forms.Label lblKapasite;
        private System.Windows.Forms.TextBox txtKapasite;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMarka;
    }
}
