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
    public partial class FrmCetakLabel : Form
    {
        public FrmCetakLabel()
        {
            InitializeComponent();
            string pathExe = AppDomain.CurrentDomain.BaseDirectory.ToString();
            InitFile oInitFile = new InitFile(pathExe + "Setting.ini");
            VarGlobal.NamaToko = oInitFile.InitReadValue("config", "NamaToko");
            VarGlobal.AlamatToko = oInitFile.InitReadValue("config", "AlamatToko");
            VarGlobal.NoHp = oInitFile.InitReadValue("config", "NoHp");

            this.Text = "KM-POS | " + VarGlobal.NamaToko+" | Cetak Label Barang";
            lblNamaToko.Text = VarGlobal.NamaToko;
            lblAlamatToko.Text = VarGlobal.AlamatToko;
            lblTelpToko.Text = VarGlobal.NoHp;
        }

        private DataTable tableBarang;

        public DataTable TableBarang { get => tableBarang; set => tableBarang = value; }

        private void FrmCetakLabel_Load(object sender, EventArgs e)
        {
            //DataTable dsCustomers = new BarangCRUD().GetAllBarangAsTable();

            ReportDataSource datasource = new ReportDataSource("DataSet1", tableBarang);
            this.rvLabelBarang.LocalReport.DataSources.Clear();
            this.rvLabelBarang.LocalReport.DataSources.Add(datasource);
            this.rvLabelBarang.RefreshReport();
        }
    }
}
