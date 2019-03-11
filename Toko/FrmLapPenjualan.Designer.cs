namespace Toko
{
    partial class FrmLapPenjualan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rvLapPenjualan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnSimpan_Barang = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTelpToko = new System.Windows.Forms.Label();
            this.lblAlamatToko = new System.Windows.Forms.Label();
            this.lblNamaToko = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rvLapPenjualan
            // 
            this.rvLapPenjualan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rvLapPenjualan.LocalReport.ReportEmbeddedResource = "Toko.Report.ReportLaporanPenjualan.rdlc";
            this.rvLapPenjualan.Location = new System.Drawing.Point(0, 110);
            this.rvLapPenjualan.Name = "rvLapPenjualan";
            this.rvLapPenjualan.ServerReport.BearerToken = null;
            this.rvLapPenjualan.ServerReport.ReportServerUrl = new System.Uri("", System.UriKind.Relative);
            this.rvLapPenjualan.Size = new System.Drawing.Size(958, 571);
            this.rvLapPenjualan.TabIndex = 0;
            // 
            // btnSimpan_Barang
            // 
            this.btnSimpan_Barang.BackColor = System.Drawing.Color.Crimson;
            this.btnSimpan_Barang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpan_Barang.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSimpan_Barang.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSimpan_Barang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnSimpan_Barang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.btnSimpan_Barang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan_Barang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpan_Barang.ForeColor = System.Drawing.Color.White;
            this.btnSimpan_Barang.Image = global::Toko.Properties.Resources.previous;
            this.btnSimpan_Barang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSimpan_Barang.Location = new System.Drawing.Point(844, 58);
            this.btnSimpan_Barang.Name = "btnSimpan_Barang";
            this.btnSimpan_Barang.Size = new System.Drawing.Size(102, 40);
            this.btnSimpan_Barang.TabIndex = 25;
            this.btnSimpan_Barang.Text = "TUTUP";
            this.btnSimpan_Barang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSimpan_Barang.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.lblTelpToko);
            this.groupBox1.Controls.Add(this.btnSimpan_Barang);
            this.groupBox1.Controls.Add(this.lblAlamatToko);
            this.groupBox1.Controls.Add(this.lblNamaToko);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(958, 104);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // lblTelpToko
            // 
            this.lblTelpToko.AutoSize = true;
            this.lblTelpToko.Location = new System.Drawing.Point(12, 75);
            this.lblTelpToko.Name = "lblTelpToko";
            this.lblTelpToko.Size = new System.Drawing.Size(188, 21);
            this.lblTelpToko.TabIndex = 2;
            this.lblTelpToko.Text = "No.HP (081 230 4000 86)";
            // 
            // lblAlamatToko
            // 
            this.lblAlamatToko.AutoSize = true;
            this.lblAlamatToko.Location = new System.Drawing.Point(12, 54);
            this.lblAlamatToko.Name = "lblAlamatToko";
            this.lblAlamatToko.Size = new System.Drawing.Size(353, 21);
            this.lblAlamatToko.TabIndex = 1;
            this.lblAlamatToko.Text = "Jln Trembes Km 01, Ds Gunem Kec Gunem 52936";
            // 
            // lblNamaToko
            // 
            this.lblNamaToko.AutoSize = true;
            this.lblNamaToko.BackColor = System.Drawing.Color.Transparent;
            this.lblNamaToko.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamaToko.Location = new System.Drawing.Point(6, 9);
            this.lblNamaToko.Name = "lblNamaToko";
            this.lblNamaToko.Size = new System.Drawing.Size(324, 45);
            this.lblNamaToko.TabIndex = 0;
            this.lblNamaToko.Text = "Toko Khoirul Mustofa";
            // 
            // FrmLapPenjualan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 681);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rvLapPenjualan);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmLapPenjualan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLapPenjualan";
            this.Load += new System.EventHandler(this.FrmLapPenjualan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvLapPenjualan;
        private System.Windows.Forms.Button btnSimpan_Barang;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTelpToko;
        private System.Windows.Forms.Label lblAlamatToko;
        private System.Windows.Forms.Label lblNamaToko;
    }
}