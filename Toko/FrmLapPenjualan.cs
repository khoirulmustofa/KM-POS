using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toko.Data;

namespace Toko
{
    public partial class FrmLapPenjualan : Form
    {
        public FrmLapPenjualan()
        {
            InitializeComponent();
            string pathExe = AppDomain.CurrentDomain.BaseDirectory.ToString();
            InitFile oInitFile = new InitFile(pathExe + "Setting.ini");
            VarGlobal.NamaToko = oInitFile.InitReadValue("config", "NamaToko");
            VarGlobal.AlamatToko = oInitFile.InitReadValue("config", "AlamatToko");
            VarGlobal.NoHp = oInitFile.InitReadValue("config", "NoHp");

            this.Text = "KM-POS | " + VarGlobal.NamaToko + " | Cetak Label Barang";
            lblNamaToko.Text = VarGlobal.NamaToko;
            lblAlamatToko.Text = VarGlobal.AlamatToko;
            lblTelpToko.Text = VarGlobal.NoHp;
        }

        private DataTable tableLapPenjualan;
        private DateTime starDate;
        private DateTime endDate;

        public DataTable TableLapPenjualan { get => tableLapPenjualan; set => tableLapPenjualan = value; }
        public DateTime StarDate { get => starDate; set => starDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        private void FrmLapPenjualan_Load(object sender, EventArgs e)
        {
            ReportParameter[] rptParam = new ReportParameter[]
            {
               new ReportParameter("NamaToko", VarGlobal.NamaToko),
               new ReportParameter("AlamatToko", VarGlobal.AlamatToko),
               new ReportParameter("StartDate", starDate.ToString("dd-MMM-yyyy HH:mm:ss")),
               new ReportParameter("EndDate", EndDate.ToString("dd-MMM-yyyy HH:mm:ss"))
            };
            this.rvLapPenjualan.LocalReport.SetParameters(rptParam);

            ReportDataSource datasource = new ReportDataSource("DataSet1", tableLapPenjualan);
            this.rvLapPenjualan.LocalReport.DataSources.Clear();
            this.rvLapPenjualan.LocalReport.DataSources.Add(datasource);
            this.rvLapPenjualan.RefreshReport();
        }
    }
}
