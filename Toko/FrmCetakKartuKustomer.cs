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
    public partial class FrmCetakKartuKustomer : Form
    {
        public FrmCetakKartuKustomer()
        {
            InitializeComponent();
            string pathExe = AppDomain.CurrentDomain.BaseDirectory.ToString();
            InitFile oInitFile = new InitFile(pathExe + "Setting.ini");
            VarGlobal.NamaToko = oInitFile.InitReadValue("config", "NamaToko");
            VarGlobal.AlamatToko = oInitFile.InitReadValue("config", "AlamatToko");
            VarGlobal.NoHp = oInitFile.InitReadValue("config", "NoHp");

            this.Text = "KM-POS | " + VarGlobal.NamaToko+" | Cetak Kartu Kustomer";
            lblNamaToko.Text = VarGlobal.NamaToko;
            lblAlamatToko.Text = VarGlobal.AlamatToko;
            lblTelpToko.Text = VarGlobal.NoHp;
        }

        private DataTable tableBarang;

        public DataTable TableKustomer { get => tableBarang; set => tableBarang = value; }

        private void FrmCetakKartuKustomer_Load(object sender, EventArgs e)
        {
            //DataTable dsCustomers = new BarangCRUD().GetAllBarangAsTable();
              
            ReportParameter[] rptParam = new ReportParameter[]
            {
                new ReportParameter("NamaToko", VarGlobal.NamaToko),
                new ReportParameter("AlamatToko", VarGlobal.AlamatToko)
            };
            this.rvKartuKustomer.LocalReport.SetParameters(rptParam);
            ReportDataSource datasource = new ReportDataSource("DataSet1", tableBarang);
            this.rvKartuKustomer.LocalReport.DataSources.Clear();
            this.rvKartuKustomer.LocalReport.DataSources.Add(datasource);

            this.rvKartuKustomer.RefreshReport();
        }
    }
}
