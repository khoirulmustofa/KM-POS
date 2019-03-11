using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toko.Data;
using Toko.Entity;

namespace Toko
{
    public partial class FrmPOS : Form
    {
        private Users userLogin;
        public Users UserLogin { get => userLogin; set => userLogin = value; }

        public FrmPOS()
        {
            InitializeComponent();

            string pathExe = AppDomain.CurrentDomain.BaseDirectory.ToString();
            InitFile oInitFile = new InitFile(pathExe + "Setting.ini");
            VarGlobal.NamaToko = oInitFile.InitReadValue("config", "NamaToko");
            VarGlobal.AlamatToko = oInitFile.InitReadValue("config", "AlamatToko");
            VarGlobal.NoHp = oInitFile.InitReadValue("config", "NoHp");
            VarGlobal.Lokasi = oInitFile.InitReadValue("config", "Lokasi");
            VarGlobal.Footer1 = oInitFile.InitReadValue("config", "Footer1");
            VarGlobal.Footer2 = oInitFile.InitReadValue("config", "Footer2");
            VarGlobal.TerimaKasih = oInitFile.InitReadValue("config", "TerimaKasih");
            VarGlobal.NamaPrinter = oInitFile.InitReadValue("config", "NamaPrinter");

        }
        #region BARANG

        private void btnRefress_Barang_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new BarangCRUD().GetAllBarangAsTable();
                gv_Barang.DataSource = dt;
                GridStyle_Barang();
                FistLoadComponent_Barang();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GridStyle_Barang()
        {

            // Header
            gv_Barang.Columns[0].HeaderText = "KODE BARANG";
            gv_Barang.Columns[1].HeaderText = "NAMA BARANG";
            gv_Barang.Columns[2].HeaderText = "HARGA BELI";
            gv_Barang.Columns[3].HeaderText = "HARGA JUAL";
            gv_Barang.Columns[4].HeaderText = "STOK";
            // Width
            gv_Barang.Columns[0].Width = 200;
            gv_Barang.Columns[1].Width = 400;
            gv_Barang.Columns[2].Width = 150;
            gv_Barang.Columns[3].Width = 150;
            gv_Barang.Columns[4].Width = 100;
            //Format

            gv_Barang.Columns[2].DefaultCellStyle.Format = "#,###";
            gv_Barang.Columns[3].DefaultCellStyle.Format = "#,###";

            gv_Barang.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gv_Barang.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gv_Barang.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Hide
            if (UserLogin.Level.Equals("Kasir"))
            {
                gv_Barang.Columns[2].Visible = false;
            }
        }

        private void FistLoadComponent_Barang()
        {
            txtKodeBarang_Barang.Enabled = true;

            txtKodeBarang_Barang.Text = "";
            txtNamaBarang_Barang.Text = "";
            txtHargaJual_Barang.Text = "";
            txtHargaBeli_Barang.Text = "";
            txtStock_Barang.Text = "";
            txtNilai_Barang.Text = "";

            txtKodeBarang_Barang.Focus();
        }
        private bool Isnew_Barang = true;
        private void btnSimpan_Barang_Click(object sender, EventArgs e)
        {
            try
            {
                if (CekIsNullOrEmpty_Barang())
                {
                    if (CekIsNumber_Barang())
                    {
                        Barang oBarang = new Barang();
                        oBarang.KodeBarang = txtKodeBarang_Barang.Text;
                        oBarang.NamaBarang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNamaBarang_Barang.Text);
                        oBarang.HargaBeli = Convert.ToInt32(txtHargaBeli_Barang.Text);
                        oBarang.HargaJual = Convert.ToInt32(txtHargaJual_Barang.Text);
                        oBarang.Stock = Convert.ToInt32(txtStock_Barang.Text);
                        if (Isnew_Barang == false)
                        {
                            //update
                            if (new BarangCRUD().UpdateBarang(oBarang))
                            {
                                Isnew_Barang = true;
                                MetroFramework.MetroMessageBox.Show(this, "Barang " + oBarang.NamaBarang + " Berhasil Di Update.", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            //Insert
                            if (new BarangCRUD().InsertBarang(oBarang))
                            {
                                Isnew_Barang = true;
                                MetroFramework.MetroMessageBox.Show(this, "Barang " + oBarang.NamaBarang + " Berhasil Di Simpan.", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        btnRefress_Barang_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CekIsNumber_Barang()
        {
            bool result = false;
            if (CekHarga(txtHargaJual_Barang.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Harga Jual Barang Bukan Angka Atau Nol !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHargaJual_Barang.Focus();
                result = false;
            }
            else if (CekHarga(txtHargaBeli_Barang.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Harga Beli Barang Bukan Angka Atau Nol !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHargaBeli_Barang.Focus();
                result = false;
            }
            else if (CekHarga(txtStock_Barang.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Stok Barang Bukan Angka Atau Nol !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStock_Barang.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private int CekHarga(string param)
        {
            int result = 0;
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (regex.IsMatch(param))
                {
                    result = Convert.ToInt32(param);
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private bool CekIsNullOrEmpty_Barang()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtKodeBarang_Barang.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Kode Barang Kosong !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKodeBarang_Barang.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtNamaBarang_Barang.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Nama Barang Kosong !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNamaBarang_Barang.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtHargaJual_Barang.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Harga Jual Barang Kosong !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHargaJual_Barang.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtHargaBeli_Barang.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Harga Beli Barang Kosong !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHargaBeli_Barang.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtStock_Barang.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Stok Barang Kosong !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStock_Barang.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void gv_Barang_DoubleClick(object sender, EventArgs e)
        {
            txtKodeBarang_Barang.Text = gv_Barang.SelectedRows[0].Cells[0].Value.ToString();
            txtNamaBarang_Barang.Text = gv_Barang.SelectedRows[0].Cells[1].Value.ToString();
            txtHargaBeli_Barang.Text = gv_Barang.SelectedRows[0].Cells[2].Value.ToString();
            txtHargaJual_Barang.Text = gv_Barang.SelectedRows[0].Cells[3].Value.ToString();
            txtStock_Barang.Text = gv_Barang.SelectedRows[0].Cells[4].Value.ToString();
            txtKodeBarang_Barang.Enabled = false;
            Isnew_Barang = false;
        }

        private void btnHapus_Barang_Click(object sender, EventArgs e)
        {
            try
            {
                string kodeBarang = gv_Barang.SelectedRows[0].Cells[0].Value.ToString();
                bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Akan Menghapus Kode " + gv_Barang.SelectedRows[0].Cells[1].Value.ToString() + " ?", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
                if (tanya)
                {
                    if (new BarangCRUD().DeleteBarangBy(kodeBarang))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Barang " + gv_Barang.SelectedRows[0].Cells[1].Value.ToString() + " Berhasil Di Hapus.", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRefress_Barang_Click(sender, e);
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : Maaf Anda Belum Memilih Data Yang Ingin Di Hapus !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCari_Barang_Click(object sender, EventArgs e)
        {
            try
            {
                string nilai = txtNilai_Barang.Text;
                if (!string.IsNullOrEmpty(nilai))
                {
                    if (cmbKriteria_Barang.SelectedItem == null)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Kriteria !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cmbKriteria_Barang.Focus();
                    }
                    else
                    {
                        string kriteria = cmbKriteria_Barang.SelectedItem.ToString().Replace(" ", "");
                        DataTable dt = new BarangCRUD().GetBarangByAsTable(nilai, kriteria);
                        gv_Barang.DataSource = dt;
                        GridStyle_Barang();
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Silahkan Isi Nilai !!!", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNilai_Barang.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCetakLabel_Barang_Click(object sender, EventArgs e)
        {
            if (gv_Barang.SelectedRows.Count != 0)
            {

                DataTable dtBarang = new DataTable();
                dtBarang.Columns.Add("KodeBarang");
                dtBarang.Columns.Add("NamaBarang");
                dtBarang.Columns.Add("HargaBeli");
                dtBarang.Columns.Add("HargaJual");
                dtBarang.Columns.Add("Stock");

                var selectedRows = gv_Barang.SelectedRows.OfType<DataGridViewRow>().Where(row => !row.IsNewRow).ToArray();

                foreach (var row in selectedRows)
                {
                    DataRow oDataRow = dtBarang.NewRow();
                    oDataRow["KodeBarang"] = row.Cells[0].Value.ToString();
                    oDataRow["NamaBarang"] = row.Cells[1].Value.ToString();
                    oDataRow["HargaBeli"] = row.Cells[2].Value.ToString();
                    oDataRow["HargaJual"] = row.Cells[3].Value.ToString();
                    oDataRow["Stock"] = row.Cells[4].Value.ToString();
                    dtBarang.Rows.Add(oDataRow);
                }

                FrmCetakLabel oFrmCetakLabel = new FrmCetakLabel();
                dtBarang.DefaultView.Sort = "KodeBarang asc";
                dtBarang = dtBarang.DefaultView.ToTable();
                oFrmCetakLabel.TableBarang = dtBarang;
                DialogResult oDialogResult = oFrmCetakLabel.ShowDialog(this);
                if (oDialogResult == DialogResult.Cancel)
                {
                    oFrmCetakLabel.Close();
                }
                else if (oDialogResult == DialogResult.OK)
                {
                    oFrmCetakLabel.Close();
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Barang Yang Ingin Di Cetak !", "BARANG | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        #endregion BARANG

        #region KUSTOMER
        private void btnRefress_Kustomer_Click(object sender, EventArgs e)
        {
            DataTable dt = new KustomerCRUD().GetAllKustomerAsTable();
            gv_Kustomer.DataSource = dt;
            GridStyle_Kustomer();
            FistLoadComponent_Kustomer();
        }


        private void btnCetakKartu_kustomer_Click(object sender, EventArgs e)
        {
            if (gv_Kustomer.SelectedRows.Count != 0)
            {

                DataTable dtKustomer = new DataTable();
                DataColumn intNoKustomer = new DataColumn("NoKustomer");
                intNoKustomer.DataType = System.Type.GetType("System.Int32");
                dtKustomer.Columns.Add(intNoKustomer);
                dtKustomer.Columns.Add("Nama");
                dtKustomer.Columns.Add("Alamat");
                dtKustomer.Columns.Add("Email");
                dtKustomer.Columns.Add("Hanphone");

                var selectedRows = gv_Kustomer.SelectedRows.OfType<DataGridViewRow>().Where(row => !row.IsNewRow).ToArray();

                foreach (var row in selectedRows)
                {
                    DataRow oDataRow = dtKustomer.NewRow();
                    oDataRow["NoKustomer"] = row.Cells[0].Value.ToString();
                    oDataRow["Nama"] = row.Cells[1].Value.ToString();
                    oDataRow["Alamat"] = row.Cells[2].Value.ToString();
                    oDataRow["Email"] = row.Cells[3].Value.ToString();
                    oDataRow["Hanphone"] = row.Cells[4].Value.ToString();
                    dtKustomer.Rows.Add(oDataRow);
                }

                FrmCetakKartuKustomer oFrmCetakKartuKustomer = new FrmCetakKartuKustomer();
                dtKustomer.DefaultView.Sort = "NoKustomer asc";
                dtKustomer = dtKustomer.DefaultView.ToTable();
                oFrmCetakKartuKustomer.TableKustomer = dtKustomer;
                DialogResult oDialogResult = oFrmCetakKartuKustomer.ShowDialog(this);
                if (oDialogResult == DialogResult.Cancel)
                {
                    oFrmCetakKartuKustomer.Close();
                }
                else if (oDialogResult == DialogResult.OK)
                {
                    oFrmCetakKartuKustomer.Close();
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Barang Yang Ingin Di Cetak !", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void GridStyle_Kustomer()
        {
            // Header
            gv_Kustomer.Columns[0].HeaderText = "NO KUSTOMER";
            gv_Kustomer.Columns[1].HeaderText = "NAMA KUSTOMER";
            gv_Kustomer.Columns[2].HeaderText = "ALAMAT";
            gv_Kustomer.Columns[3].HeaderText = "EMAIL";
            gv_Kustomer.Columns[4].HeaderText = "HANPHONE";
            gv_Kustomer.Columns[5].HeaderText = "POINT";
            // Width
            gv_Kustomer.Columns[0].Width = 180;
            gv_Kustomer.Columns[1].Width = 200;
            gv_Kustomer.Columns[2].Width = 400;
            gv_Kustomer.Columns[3].Width = 250;
            gv_Kustomer.Columns[4].Width = 130;
            gv_Kustomer.Columns[5].Width = 80;
            //Aligment
            gv_Kustomer.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void FistLoadComponent_Kustomer()
        {

            txtNoKustomer_Kustomer.Text = "";
            txtNamaKustomer_Kustomer.Text = "";
            txtEmail_Kustomer.Text = "";
            txtAlamat_Kustomer.Text = "";
            txtHanphone_Kustomer.Text = "";
            txtPoint_Kustomer.Text = "";
            txtNilai_Kustomer.Text = "";

            txtNamaKustomer_Kustomer.Focus();
        }

        private void btnCari_Kustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string nilai = txtNilai_Kustomer.Text;
                if (!string.IsNullOrEmpty(nilai))
                {
                    if (cmbKriteria_Kustomer.SelectedItem == null)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Kriteria !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cmbKriteria_Kustomer.Focus();
                    }
                    else
                    {
                        string kriteria = cmbKriteria_Kustomer.SelectedItem.ToString().Replace(" ", "");
                        DataTable dt = new KustomerCRUD().GetKustomerByAsTable(nilai, kriteria);
                        gv_Kustomer.DataSource = dt;
                        GridStyle_Kustomer();
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Silahkan Isi Nilai !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNilai_Kustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Isnew_kustomer = true;

        private void btnSimpan_Kustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (CekIsNullOrEmpty_Kustomer())
                {
                    if (CekIsNumber_Kustomer())
                    {
                        Kustomer oKustomer = new Kustomer();
                        oKustomer.Nama = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNamaKustomer_Kustomer.Text);
                        oKustomer.Alamat = txtAlamat_Kustomer.Text;
                        oKustomer.Email = txtEmail_Kustomer.Text;
                        oKustomer.Hanphone = txtHanphone_Kustomer.Text;
                        oKustomer.Point = Convert.ToInt32(txtPoint_Kustomer.Text);
                        if (Isnew_kustomer == false)
                        {
                            //update
                            if (new KustomerCRUD().UpdateKustomer(oKustomer))
                            {
                                Isnew_kustomer = true;
                                MetroFramework.MetroMessageBox.Show(this, "Kustomer " + oKustomer.Nama + " Berhasil Di Update.", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            //Insert
                            if (new KustomerCRUD().InsertKustomer(oKustomer))
                            {
                                Isnew_kustomer = true;
                                MetroFramework.MetroMessageBox.Show(this, "Kustomer " + oKustomer.Nama + " Berhasil Di Simpan.", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        btnRefress_Kustomer_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CekIsNumber_Kustomer()
        {
            bool result = false;
            if (CekHarga_Kustomer(txtHanphone_Kustomer.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Hanphone Kustomer Bukan Angka Atau Nol !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHanphone_Kustomer.Focus();
                result = false;
            }
            else if (CekHarga_Kustomer(txtPoint_Kustomer.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Point Kustomer Bukan Angka Atau Nol !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPoint_Kustomer.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private int CekHarga_Kustomer(string param)
        {
            int result = 0;
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (regex.IsMatch(param))
                {
                    result = Convert.ToInt32(param);
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private bool CekIsNullOrEmpty_Kustomer()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtNoKustomer_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Kode Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNoKustomer_Kustomer.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtNamaKustomer_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Nama Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNamaKustomer_Kustomer.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtAlamat_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Alamat Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAlamat_Kustomer.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtEmail_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Email Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail_Kustomer.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtHanphone_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Hanphone Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHanphone_Kustomer.Focus();
                result = false;
            }
            if (string.IsNullOrEmpty(txtPoint_Kustomer.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Point Kustomer Kosong !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPoint_Kustomer.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void gv_Kustomer_DoubleClick(object sender, EventArgs e)
        {
            txtNoKustomer_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[0].Value.ToString();
            txtNamaKustomer_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[1].Value.ToString();
            txtAlamat_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[3].Value.ToString();
            txtHanphone_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[4].Value.ToString();
            txtPoint_Kustomer.Text = gv_Kustomer.SelectedRows[0].Cells[5].Value.ToString();
            txtNoKustomer_Kustomer.Enabled = false;
            Isnew_kustomer = false;
        }

        private void btnHapus_Kustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string NoKustomer = gv_Kustomer.SelectedRows[0].Cells[0].Value.ToString();
                bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Akan Menghapus Kode " + gv_Kustomer.SelectedRows[0].Cells[1].Value.ToString() + " ?", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
                if (tanya)
                {
                    if (new KustomerCRUD().DeleteKustomerBy(NoKustomer))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Kustomer " + gv_Kustomer.SelectedRows[0].Cells[1].Value.ToString() + " Berhasil Di Hapus.", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRefress_Kustomer_Click(sender, e);
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : Maaf Anda Belum Memilih Data Yang Ingin Di Hapus !!!", "KUSTOMER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion KUSTOMER

        #region PENJUALAN
        private void txtKodeBarcode_Penjualan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string kodeBarcode = txtKodeBarcode_Penjualan.Text;
                    Barang oBarang = new BarangCRUD().GetBarangByKodeBarang(kodeBarcode);
                    if (oBarang == null)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Kode " + kodeBarcode + " Yang Anda Masukan Tidak Ada Di Sistem", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        // Jika Baru  pertama kali belanja
                        if (gv_Penjualan.Rows.Count <= 0)
                        {
                            int n = gv_Penjualan.Rows.Add();
                            gv_Penjualan.Rows[n].Cells[0].Value = oBarang.KodeBarang;
                            gv_Penjualan.Rows[n].Cells[1].Value = oBarang.NamaBarang;
                            gv_Penjualan.Rows[n].Cells[2].Value = "1";
                            gv_Penjualan.Rows[n].Cells[3].Value = oBarang.HargaJual;
                            gv_Penjualan.Rows[n].Cells[4].Value = (Int32.Parse(gv_Penjualan.Rows[n].Cells[2].Value.ToString()) * Int32.Parse(gv_Penjualan.Rows[n].Cells[3].Value.ToString()));
                            gv_Penjualan.Rows[n].Cells[5].Value = oBarang.HargaBeli;
                            lblPesan_Penjualan.Text = oBarang.NamaBarang + " Berhasil Di Tambah.";
                        }
                        else
                        {
                            // Jika sudah ada kode barang yang sama
                            bool isNew = true;
                            int j = gv_Penjualan.Rows.Count;
                            for (int i = 0; i < j; i++)
                            {
                                string _kodeBarang = gv_Penjualan.Rows[i].Cells[0].Value.ToString();
                                // Jika ada barang sama yang di input
                                if (kodeBarcode.Equals(_kodeBarang))
                                {
                                    int _jumlah = Convert.ToInt32(gv_Penjualan.Rows[i].Cells[2].Value.ToString()) + 1;
                                    gv_Penjualan.Rows[i].Cells[2].Value = _jumlah.ToString();
                                    gv_Penjualan.Rows[i].Cells[4].Value = (Int32.Parse(gv_Penjualan.Rows[i].Cells[2].Value.ToString()) * Int32.Parse(gv_Penjualan.Rows[i].Cells[3].Value.ToString()));
                                    isNew = false;
                                    break;
                                }
                            }
                            // Jika Baru
                            if (isNew)
                            {
                                // baru tidak double
                                int n = gv_Penjualan.Rows.Add();
                                gv_Penjualan.Rows[n].Cells[0].Value = oBarang.KodeBarang;
                                gv_Penjualan.Rows[n].Cells[1].Value = oBarang.NamaBarang;
                                gv_Penjualan.Rows[n].Cells[2].Value = "1";
                                gv_Penjualan.Rows[n].Cells[3].Value = oBarang.HargaJual;
                                gv_Penjualan.Rows[n].Cells[4].Value = (Int32.Parse(gv_Penjualan.Rows[n].Cells[2].Value.ToString()) * Int32.Parse(gv_Penjualan.Rows[n].Cells[3].Value.ToString()));
                                gv_Penjualan.Rows[n].Cells[5].Value = oBarang.HargaBeli;
                                lblPesan_Penjualan.Text = oBarang.NamaBarang + " Berhasil Di Tambah.";
                            }
                            lblPesan_Penjualan.Text = oBarang.NamaBarang + " Berhasil Di Tambah.";
                        }
                        string totalJual = SumTotalBayar_Penjualan();
                        txtTotalBayar_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", Int32.Parse(totalJual));
                        gv_Penjualan.Columns[3].DefaultCellStyle.Format = "#,###";
                        gv_Penjualan.Columns[4].DefaultCellStyle.Format = "#,###";

                        FistLoadComponent_Penjualan();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FistLoadComponent_Penjualan()
        {
            txtKodeBarcode_Penjualan.Enabled = true;

            txtNamaBarang_Penjualan.Visible = false;
            txtHarga_Penjualan.Visible = false;
            txtJumlah_Penjualan.Visible = false;
            txtTotal_Penjualan.Visible = false;
            txtKodeBarcode_Penjualan.Text = "";
            lblPesan_Penjualan.Text = "-";
            lblNamaKustomer_Penjualan.Text = "-";
            txtKodeBarcode_Penjualan.Focus();
        }

        private string SumTotalBayar_Penjualan()
        {
            double totalPenjualan = 0;
            int total = gv_Penjualan.Rows.Count;
            for (int i = 0; i < total; ++i)
            {
                totalPenjualan += Convert.ToDouble(gv_Penjualan.Rows[i].Cells[4].Value);
            }
            return totalPenjualan.ToString();
        }

        private void gv_Penjualan_DoubleClick(object sender, EventArgs e)
        {
            txtKodeBarcode_Penjualan.Text = gv_Penjualan.SelectedRows[0].Cells[0].Value.ToString();
            txtNamaBarang_Penjualan.Text = gv_Penjualan.SelectedRows[0].Cells[1].Value.ToString();
            txtJumlah_Penjualan.Text = gv_Penjualan.SelectedRows[0].Cells[2].Value.ToString();
            txtHarga_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", gv_Penjualan.SelectedRows[0].Cells[3].Value);
            txtTotal_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", gv_Penjualan.SelectedRows[0].Cells[4].Value);

            txtKodeBarcode_Penjualan.Enabled = false;

            txtNamaBarang_Penjualan.Visible = true;
            txtHarga_Penjualan.Visible = true;
            txtJumlah_Penjualan.Visible = true;
            txtTotal_Penjualan.Visible = true;

            txtJumlah_Penjualan.Focus();
        }

        private void btnHapus_Penjualan_Click(object sender, EventArgs e)
        {
            try
            {
                string kodeBarcode = gv_Penjualan.SelectedRows[0].Cells[0].Value.ToString();

                bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Akan Menghapus Kode " + kodeBarcode + " ?", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
                if (tanya)
                {
                    foreach (DataGridViewRow item in this.gv_Penjualan.SelectedRows)
                    {
                        gv_Penjualan.Rows.RemoveAt(item.Index);
                    }
                    FistLoadComponent_Penjualan();
                    string totalJual = SumTotalBayar_Penjualan();
                    txtTotalBayar_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", Int32.Parse(totalJual));

                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Anda Belum Memilih Item Yang Akan Di Hapus !!!", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUangBayar_Penjualan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"^[0-9]+$");
                if (regex.IsMatch(txtUangBayar_Penjualan.Text))
                {
                    int uangBayar = Int32.Parse(txtUangBayar_Penjualan.Text);
                    int totalBayar = Int32.Parse(SumTotalBayar_Penjualan());
                    if (uangBayar < totalBayar)
                    {
                        txtUangKembali_Penjualan.Text = "Kurang Bayar";
                        txtUangKembali_Penjualan.ForeColor = Color.Red;
                        txtUangKembali_Penjualan.TextAlign = HorizontalAlignment.Center;
                    }
                    else
                    {
                        int uangKembali = uangBayar - totalBayar;
                        txtUangKembali_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", uangKembali);
                        txtUangKembali_Penjualan.ForeColor = Color.Black;
                        txtUangKembali_Penjualan.TextAlign = HorizontalAlignment.Right;
                    }
                }
                else if (string.IsNullOrEmpty(txtUangBayar_Penjualan.Text))
                {
                    txtUangBayar_Penjualan.Clear();
                    txtUangKembali_Penjualan.Text = "Kurang Bayar";
                    txtUangKembali_Penjualan.ForeColor = Color.Red;
                    txtUangKembali_Penjualan.TextAlign = HorizontalAlignment.Center;
                    txtUangBayar_Penjualan.Focus();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Maaf Uang Bayar Yang Anda Masukan Bukan Angka !!!", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error :\n" + ex.ToString(), "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string NoKustomer_Penjualan = string.Empty;



        private void btnBayar_Penjualan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUangBayar_Penjualan.Text))
                {
                    MetroFramework.MetroMessageBox.Show(this, "Maaf Uang Bayar Belum Di Input !!!", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUangBayar_Penjualan.Focus();
                }
                else
                {
                    int uangBayar = Int32.Parse(txtUangBayar_Penjualan.Text);
                    int totalBayar = Int32.Parse(SumTotalBayar_Penjualan());
                    if (uangBayar < totalBayar)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Maaf Uang Bayar Masih Kurang !!!", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        SavePenjualan();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error :\n" + ex.ToString(), "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePenjualan()
        {
            string kodeJual = "PJL-" + DateTime.Now.ToString("ddMMyyhhmmss");
            if (SaveJual(kodeJual))
            {

                int jmlItem = gv_Penjualan.Rows.Count;
                for (int i = 0; i < jmlItem; i++)
                {
                    JualDetail oJualDetail = new JualDetail();
                    oJualDetail.KodeJual = kodeJual;
                    oJualDetail.KodeBarang = gv_Penjualan.Rows[i].Cells[0].Value.ToString();
                    oJualDetail.Jumlah = Int32.Parse(gv_Penjualan.Rows[i].Cells[2].Value.ToString());
                    oJualDetail.HargaJual = Int32.Parse(gv_Penjualan.Rows[i].Cells[3].Value.ToString());
                    oJualDetail.HargaBeli = Int32.Parse(gv_Penjualan.Rows[i].Cells[5].Value.ToString());
                    new JualDetailCRUD().InsertJualDetail(oJualDetail);
                }
                // Cetak struk
                Cetak.CetakStruk(kodeJual);
                MetroFramework.MetroMessageBox.Show(this, "Terima Kasih Telah Melakukan Belanja", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);


                //Clear Component
                gv_Penjualan.Rows.Clear();
                gv_Penjualan.Refresh();
                txtTotalBayar_Penjualan.Text = "";
                txtUangBayar_Penjualan.Text = "";
                txtUangKembali_Penjualan.Text = "";
                txtKeterangan_Penjualan.Text = "";
                FistLoadComponent_Penjualan();
            }
        }

        private bool SaveJual(string kodeJual)
        {
            bool result = false;
            Jual oJual = new Jual();
            oJual.KodeJual = kodeJual;
            oJual.Waktu = DateTime.Now;
            oJual.NoKustomer = NoKustomer_Penjualan;
            oJual.Kasir = userLogin.NamaLengkap;
            oJual.Lokasi = VarGlobal.Lokasi;
            oJual.UangBayar = Int32.Parse(txtUangBayar_Penjualan.Text);
            oJual.TotalBayar = Int32.Parse(SumTotalBayar_Penjualan());
            oJual.Keterangan = txtKeterangan_Penjualan.Text;
            if (new JualCRUD().InsertJual(oJual))
            {
                result = true;
            }
            return result;
        }

        private int CekNumberJumlahJual(string strNumber)
        {
            int result = 0;
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (regex.IsMatch(strNumber))
                {
                    result = Convert.ToInt32(strNumber);
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private void txtJumlah_Penjualan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (CekNumberJumlahJual(txtJumlah_Penjualan.Text) == 0)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Jumlah Yang Anda Masukan Bukan Angka Atau Nol", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtJumlah_Penjualan.Focus();
                    }
                    else
                    {
                        string kodeBarang = txtKodeBarcode_Penjualan.Text.Trim();
                        int j = gv_Penjualan.Rows.Count;
                        string namaBarang = string.Empty;
                        for (int i = 0; i < j; i++)
                        {
                            string _kodeBarang = gv_Penjualan.Rows[i].Cells[0].Value.ToString();
                            if (kodeBarang.Equals(_kodeBarang))
                            {
                                int _jumlah = Int32.Parse(txtJumlah_Penjualan.Text);
                                gv_Penjualan.Rows[i].Cells[2].Value = _jumlah.ToString();
                                gv_Penjualan.Rows[i].Cells[4].Value = (Int32.Parse(gv_Penjualan.Rows[i].Cells[2].Value.ToString()) * Int32.Parse(gv_Penjualan.Rows[i].Cells[3].Value.ToString()));
                                namaBarang = gv_Penjualan.Rows[i].Cells[1].Value.ToString();
                                break;
                            }
                        }
                        string totalJual = SumTotalBayar_Penjualan();
                        txtTotalBayar_Penjualan.Text = String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:C}", Int32.Parse(totalJual));
                        gv_Penjualan.Columns[3].DefaultCellStyle.Format = "#,###";
                        gv_Penjualan.Columns[4].DefaultCellStyle.Format = "#,###";
                        lblPesan_Penjualan.Text = namaBarang + " Berhasil Di Update.";

                        FistLoadComponent_Penjualan();
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Jumlah Yang Anda Masukan Bukan Angka Atau 0", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNoKustomer_Penjualan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string NoKustomer = txtNoKustomer_Penjualan.Text.Trim();

                    Kustomer oKustomer = new KustomerCRUD().GetKustomerBy(NoKustomer);
                    if (oKustomer != null)
                    {
                        lblNamaKustomer_Penjualan.Text = "Selamat Datang " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oKustomer.Nama).ToString() + " ( " + oKustomer.NoKustomer + " )";
                        NoKustomer_Penjualan = oKustomer.NoKustomer;
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Kode Kustomer " + NoKustomer + " Tidak Ada Dalam Sistem !!!", "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    txtNoKustomer_Penjualan.Text = "";
                    txtKodeBarcode_Penjualan.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "Penjualan | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion PENJUALAN

        #region KREDIT

        private void GridStyle_HutangBayar()
        {
            // Header
            gv_HutangBayar.Columns[0].HeaderText = "WAKTU";
            gv_HutangBayar.Columns[1].HeaderText = "HUTANG";
            gv_HutangBayar.Columns[2].HeaderText = "BAYAR";
            gv_HutangBayar.Columns[3].HeaderText = "SALDO";
            gv_HutangBayar.Columns[4].HeaderText = "KETERANGAN";
            // Width
            gv_HutangBayar.Columns[0].Width = 200;
            gv_HutangBayar.Columns[1].Width = 100;
            gv_HutangBayar.Columns[2].Width = 100;
            gv_HutangBayar.Columns[3].Width = 100;
            gv_HutangBayar.Columns[4].Width = 300;

            //Format
            gv_HutangBayar.Columns[0].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";
            gv_HutangBayar.Columns[1].DefaultCellStyle.Format = "#,###";
            gv_HutangBayar.Columns[2].DefaultCellStyle.Format = "#,###";
            gv_HutangBayar.Columns[3].DefaultCellStyle.Format = "#,###";
            //Aligment
            gv_HutangBayar.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gv_HutangBayar.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gv_HutangBayar.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        private void txtNoKustomer_HutangBayar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string NoKustomer = txtNoKustomer_HutangBayar.Text.Trim();

                    Kustomer oKustomer = new KustomerCRUD().GetKustomerBy(NoKustomer);
                    if (oKustomer != null)
                    {
                        // Populate Kustomer
                        txtNoKustomerLabel_HutangBayar.Text = oKustomer.NoKustomer;
                        txtNamaLabel_HutangBayar.Text = oKustomer.Nama;
                        txtAlamatLabel_HutangBayar.Text = oKustomer.Alamat;
                        txtEmailLabel_HutangBayar.Text = oKustomer.Email;
                        txtHanphoneLabel_HutangBayar.Text = oKustomer.Hanphone;
                        txtPointLabel_HutangBayar.Text = oKustomer.Point.ToString();
                        // Tampil di Grid
                        ShowHutangBayarBy(NoKustomer);
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Kode Kustomer " + NoKustomer + " Tidak Ada Dalam Sistem !!!", "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    txtNoKustomer_HutangBayar.Text = "";
                    txtNoKustomer_HutangBayar.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ShowHutangBayarBy(string NoKustomer)
        {
            DataTable dt = new DataTable();
            dt = new HutangBayarCRUD().GetHutangBayarKustomerByKustomerAsTable(NoKustomer);

            gv_HutangBayar.DataSource = dt;
            GridStyle_HutangBayar();
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexTab = tabControl1.SelectedIndex;
            if (indexTab == 0)
            {
                this.Text = "KM-POS " + VarGlobal.NamaToko + " | BARANG";
                txtKodeBarang_Barang.Focus();
            }
            else if (indexTab == 1)
            {
                this.Text = "KM-POS " + VarGlobal.NamaToko + " | KUSTOMER";
                txtNamaKustomer_Kustomer.Focus();
            }
            else if (indexTab == 2)
            {
                this.Text = "KM-POS " + VarGlobal.NamaToko + " | PENJUALAN";
                txtKodeBarcode_Penjualan.Focus();
            }
            else if (indexTab == 3)
            {
                this.Text = "KM-POS " + VarGlobal.NamaToko + " | HUTANG BAYAR";
                txtNoKustomer_HutangBayar.Focus();
            }
            else if (indexTab == 4)
            {
                this.Text = "KM-POS " + VarGlobal.NamaToko + " | USERS";
                txtUserName_Users.Focus();
            }
        }

        private void btnRefresh_BayarHutang_Click(object sender, EventArgs e)
        {
            try
            {
                FistLoadComponent_HutangBarar();
                string NoKustomer = txtNoKustomer_HutangBayar.Text.Trim();
                ShowHutangBayarBy(NoKustomer);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnSimpan_HutangBayar_Click(object sender, EventArgs e)
        {
            try
            {
                string nominal = txtNominal_HutangBayar.Text;
                if (!string.IsNullOrEmpty(nominal))
                {
                    if (CekIsNumber_HutangBayar())
                    {
                        if (cmbKriteria_HutangBayar.SelectedItem == null)
                        {
                            MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Kriteria Hutang Bayar !!!", "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            cmbKriteria_HutangBayar.Focus();
                        }
                        else
                        {
                            string kriteria = cmbKriteria_HutangBayar.SelectedItem.ToString().Replace(" ", "");
                            HutangBayar oHutangBayar = new HutangBayar();

                            oHutangBayar.Nominal = Convert.ToInt32(txtNominal_HutangBayar.Text);
                            oHutangBayar.Kode = cmbKriteria_HutangBayar.SelectedItem.ToString();
                            oHutangBayar.NoKustomer = txtNoKustomerLabel_HutangBayar.Text;
                            oHutangBayar.Waktu = DateTime.Now;
                            oHutangBayar.Keterangan = txtKeterangan_HutangBayar.Text;
                            oHutangBayar.Kasir = userLogin.NamaLengkap;
                            new HutangBayarCRUD().InsertHutangBayar(oHutangBayar);
                            ShowHutangBayarBy(txtNoKustomerLabel_HutangBayar.Text);
                            cmbKriteria_HutangBayar.Items.Clear();
                            cmbKriteria_HutangBayar.Items.AddRange(new object[] { "HUTANG", "BAYAR" });
                            MetroFramework.MetroMessageBox.Show(this, "Hutang Bayar kustomer No " + oHutangBayar.NoKustomer + " Berhasil Di Update.", "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Silahkan Isi Nominal !!!", "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNominal_HutangBayar.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FistLoadComponent_HutangBarar()
        {
            txtNoKustomerLabel_HutangBayar.Text = "";
            txtNamaLabel_HutangBayar.Text = "";
            txtAlamatLabel_HutangBayar.Text = "";
            txtEmailLabel_HutangBayar.Text = "";
            txtHanphoneLabel_HutangBayar.Text = "";
            txtPointLabel_HutangBayar.Text = "";
            cmbKriteria_HutangBayar.Items.AddRange(new object[] {
            "HUTANG",
            "BAYAR"});
            txtNominal_HutangBayar.Text = "";
            txtKeterangan_HutangBayar.Text = "";
        }
        private bool CekIsNumber_HutangBayar()
        {
            bool result = false;
            if (CekHarga(txtNominal_HutangBayar.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Nominal Hutang Bayar Bukan Angka Atau Nol !!!", "HUTANG BAYAR | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNominal_HutangBayar.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        #endregion KREDIT

        #region USERS

        private void btnRefress_Users_Click(object sender, EventArgs e)
        {
            DataTable dt = new UsersCRUD().GetAllUserAsTable();
            gv_Users.DataSource = dt;
            GridStyle_Users();
            FistLoadComponent_Users();
        }

        private void FistLoadComponent_Users()
        {
            txtUserName_Users.Enabled = true;
            txtUserName_Users.Text = "";
            txtPassword_Users.Text = "";
            txtNamaLengkap_Users.Text = "";
            txtAlamat_Users.Text = "";
            txtNoKTP_Users.Text = "";
            cmbLevel_Users.Items.Clear();
            cmbLevel_Users.Items.AddRange(new object[] {
            "Owner",
            "Kasir"});

            txtUserName_Users.Focus();
        }

        private void GridStyle_Users()
        {
            // Header
            gv_Users.Columns[0].HeaderText = "USER ID";
            gv_Users.Columns[1].HeaderText = "USER NAME";
            gv_Users.Columns[3].HeaderText = "NAMA LENGKAP";
            gv_Users.Columns[4].HeaderText = "ALAMAT";
            gv_Users.Columns[5].HeaderText = "NO KTP";
            gv_Users.Columns[6].HeaderText = "LEVEL";
            // Width
            gv_Users.Columns[0].Width = 100;
            gv_Users.Columns[1].Width = 170;
            gv_Users.Columns[3].Width = 250;
            gv_Users.Columns[4].Width = 200;
            gv_Users.Columns[5].Width = 200;
            gv_Users.Columns[6].Width = 100;
            // Hide
            gv_Users.Columns[2].Visible = false;
            //Aligment
            gv_Users.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void btnCari_Users_Click(object sender, EventArgs e)
        {
            try
            {
                string nilai = txtNilai_Users.Text;
                if (!string.IsNullOrEmpty(nilai))
                {
                    if (cmbKriteria_Users.SelectedItem == null)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Silahkan Pilih Kriteria !!!", "USER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cmbKriteria_Users.Focus();
                    }
                    else
                    {
                        string kriteria = cmbKriteria_Users.SelectedItem.ToString().Replace(" ", "");
                        DataTable dt = new UsersCRUD().GetUsersByAsTable(nilai, kriteria);
                        gv_Users.DataSource = dt;
                        GridStyle_Users();
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Silahkan Isi Nilai !!!", "USER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNilai_Kustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "USER | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool Isnew_User = true;
        private void btnSimpan_Users_Click(object sender, EventArgs e)
        {
            try
            {
                if (CekIsNullOrEmpty_Users())
                {
                    if (CekIsNumber_Users())
                    {
                        Users oUsers = new Users();
                        oUsers.UserId = userId;
                        oUsers.UserName = txtUserName_Users.Text;
                        oUsers.Passwords = txtPassword_Users.Text;
                        oUsers.NamaLengkap = txtNamaLengkap_Users.Text;
                        oUsers.Alamat = txtAlamat_Users.Text;
                        oUsers.NoKTP = txtNoKTP_Users.Text;
                        oUsers.Level = cmbLevel_Users.SelectedItem.ToString();
                        if (Isnew_User == false)
                        {
                            //update
                            if (cbGantiPassword.Checked == true)
                            {
                                if (new UsersCRUD().UpdateUserWithPassword(oUsers))
                                {
                                    Isnew_User = true;
                                    MetroFramework.MetroMessageBox.Show(this, "User " + oUsers.UserName + " Berhasil Di Update.", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                if (new UsersCRUD().UpdateUserWithoutPassword(oUsers))
                                {
                                    Isnew_User = true;
                                    MetroFramework.MetroMessageBox.Show(this, "User " + oUsers.UserName + " Berhasil Di Update.", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                        else
                        {
                            // Make Sure Password not Emty
                            if (string.IsNullOrEmpty(txtPassword_Users.Text))
                            {
                                MetroFramework.MetroMessageBox.Show(this, "Maaf Password Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtPassword_Users.Focus();
                            }
                            else
                            {
                                //Insert
                                if (new UsersCRUD().InsertUser(oUsers))
                                {
                                    Isnew_User = true;
                                    MetroFramework.MetroMessageBox.Show(this, "User " + oUsers.UserName + " Berhasil Di Simpan.", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        btnRefress_Users_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : \n" + ex.ToString(), "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CekIsNumber_Users()
        {
            bool result = false;
            if (CekAngka_Users(txtNoKTP_Users.Text) == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf No KTP Bukan Angka !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHanphone_Kustomer.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private int CekAngka_Users(string param)
        {
            int result = 0;
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (regex.IsMatch(param))
                {
                    result = Convert.ToInt32(param);
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private bool CekIsNullOrEmpty_Users()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtUserName_Users.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf User Name Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName_Users.Focus();
                result = false;
            }

            else if (string.IsNullOrEmpty(txtNamaLengkap_Users.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Nama Lengkap Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNamaLengkap_Users.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(txtAlamat_Users.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Alamat Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAlamat_Users.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(txtNoKTP_Users.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf No KTP Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNoKTP_Users.Focus();
                result = false;
            }
            else if (cmbLevel_Users.SelectedItem == null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Level Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbLevel_Users.Focus();
                result = false;
            }
            else if (cbGantiPassword.Checked == true)
            {
                if (string.IsNullOrEmpty(txtPassword_Users.Text))
                {
                    MetroFramework.MetroMessageBox.Show(this, "Maaf Password Kosong !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPassword_Users.Focus();
                    result = false;
                }
                else { result = true; }
            }
            else
            {
                result = true;
            }
            return result;
        }

        private int userId;
        private void gv_Users_DoubleClick(object sender, EventArgs e)
        {
            userId = Convert.ToInt32(gv_Users.SelectedRows[0].Cells[0].Value.ToString());
            txtUserName_Users.Text = gv_Users.SelectedRows[0].Cells[1].Value.ToString();
            txtNamaLengkap_Users.Text = gv_Users.SelectedRows[0].Cells[3].Value.ToString();
            txtAlamat_Users.Text = gv_Users.SelectedRows[0].Cells[4].Value.ToString();
            txtNoKTP_Users.Text = gv_Users.SelectedRows[0].Cells[5].Value.ToString();
            cmbLevel_Users.Text = gv_Users.SelectedRows[0].Cells[6].Value.ToString();

            Isnew_User = false;
        }

        private void btnHapus_Users_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = gv_Users.SelectedRows[0].Cells[0].Value.ToString();
                bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Akan Menghapus User Name " + gv_Users.SelectedRows[0].Cells[1].Value.ToString() + " ?", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
                if (tanya)
                {
                    if (new UsersCRUD().DeleteUsersBy(userId))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "User " + gv_Users.SelectedRows[0].Cells[1].Value.ToString() + " Berhasil Di Hapus.", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRefress_Users_Click(sender, e);
                    }
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error : Maaf Anda Belum Memilih Data Yang Ingin Di Hapus !!!", "USERS | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion USERS

        private void FrmPOS_Load(object sender, EventArgs e)
        {
            this.Text = "KM-POS " + VarGlobal.NamaToko;
            lblNamaToko.Text = VarGlobal.NamaToko;
            lblAlamatToko.Text = VarGlobal.AlamatToko;
            lblTelpToko.Text = VarGlobal.NoHp;

            lblUserLogin.Text = userLogin.NamaLengkap + " - ( " + userLogin.Level + " )";
            dtStart_Laporan.Value = DateTime.Now.Date;
            dtEnd_Laporan.Value = DateTime.Now.Date;

            gv_Penjualan.Columns[5].Visible = false;

            //Autorisation
            if (userLogin.Level.Equals("Kasir"))
            {
                btnSimpan_Barang.Visible = false;
                btnSimpan_Kustomer.Visible = false;
                btnSimpan_Users.Visible = false;

                btnHapus_Barang.Visible = false;
                btnHapus_Kustomer.Visible = false;
                btnHapus_Users.Visible = false;

                btnCetakLabel_Barang.Visible = false;

                label8.Visible = false;
                txtHargaBeli_Barang.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Ingin Keluar Dari Aplikasi ?", "KM | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            if (tanya)
            {
                Application.Exit();
            }
        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void btnCetak_Laporan_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtStart = dtStart_Laporan.Value;
                DateTime dtEnd = dtEnd_Laporan.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                DataTable dtLapPenjulan = new JualCRUD().GetLapPenjulanBy(dtStart, dtEnd);
                if (dtStart > dtEnd)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Maaf Tanggal Dari Tidak Boleh Melebihi Tangal Dari !", "LAPORAN | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtStart_Laporan.Focus();
                }
                else
                {
                    FrmLapPenjualan oFrmLapPenjualan = new FrmLapPenjualan();
                    oFrmLapPenjualan.TableLapPenjualan = dtLapPenjulan;
                    oFrmLapPenjualan.StarDate = dtStart;
                    oFrmLapPenjualan.EndDate = dtEnd;
                    DialogResult oDialogResult = oFrmLapPenjualan.ShowDialog(this);
                    if (oDialogResult == DialogResult.Cancel)
                    {
                        oFrmLapPenjualan.Close();
                    }
                    else if (oDialogResult == DialogResult.OK)
                    {
                        oFrmLapPenjualan.Close();
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
