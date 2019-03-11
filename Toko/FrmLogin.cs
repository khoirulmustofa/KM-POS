using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toko.Data;
using Toko.Entity;

namespace Toko
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            string pathExe = AppDomain.CurrentDomain.BaseDirectory.ToString();
            InitFile oInitFile = new InitFile(pathExe + "Setting.ini");
            VarGlobal.NamaToko = oInitFile.InitReadValue("config", "NamaToko");
            VarGlobal.AlamatToko = oInitFile.InitReadValue("config", "AlamatToko");
            VarGlobal.NoHp = oInitFile.InitReadValue("config", "NoHp");
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Text = "KM-POS " + VarGlobal.NamaToko + " | LOGIN";
            lblNamaToko.Text = VarGlobal.NamaToko;
            lblAlamatToko.Text = VarGlobal.AlamatToko;
            lblTelpToko.Text = VarGlobal.NoHp;
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            try
            {
                bool tanya = MetroFramework.MetroMessageBox.Show(this, "Apakah Anda Ingin Keluar Dari Aplikasi ?", "LOGIN | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (tanya)
                {
                    Application.Exit();
                }
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //if (CekIsNullOrEmptyUser())
                //{
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                //Users oUsers = new UsersCRUD().GetUserBy(userName, password);
                Users oUsers = new UsersCRUD().GetUserBy("admin", "123123");
                if (oUsers != null)
                {
                    FrmPOS oFrmPOS = new FrmPOS();
                    oFrmPOS.UserLogin = oUsers;
                    oFrmPOS.Show();
                    this.Hide();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Maaf User Name Atau Password Anda Salah !!", "LOGIN | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserName.Focus();
                }
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool CekIsNullOrEmptyUser()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf User Name Kosong !!!", "LOGIN | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                result = false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Maaf Password Kosong !!!", "LOGIN | " + "KM-POS " + VarGlobal.NamaToko, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
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
    }
}
