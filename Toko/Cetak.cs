using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Data;
using Toko.Entity;

namespace Toko
{
    public class Cetak
    {
        public static void CetakStruk(string KodeJual)
        {

            //Cetak Header
            string s = " " + Convert.ToString((char)10);
            s += Convert.ToString((char)27) + "a" + Convert.ToString((char)1); // rata tengah
            s += Convert.ToString((char)27) + "!" + Convert.ToString((char)16); // double High            
            s += VarGlobal.NamaToko + Convert.ToString((char)10);
            s += VarGlobal.AlamatToko + Convert.ToString((char)10);
            s += VarGlobal.NoHp + Convert.ToString((char)10);
            s += Convert.ToString((char)27) + "!" + Convert.ToString((char)0); // normal High
            s += Convert.ToString((char)27) + "a" + Convert.ToString((char)0);  // rata kiri
            s += "------------------------------------------------" + Convert.ToString((char)10);
            
                Jual oJual = new JualCRUD().GetJualBy(KodeJual);

                s += "Kode Jual  : " + oJual.KodeJual.Trim() + Convert.ToString((char)10);
                s += "Tanggal    : " + oJual.Waktu.ToString("dd-MMM-yyyy hh:mm:ss") + Convert.ToString((char)10);
                s += "Kasir      : " + oJual.Kasir.Trim() + Convert.ToString((char)10);
                s += "------------------------------------------------" + Convert.ToString((char)10);
                s += "Nama                        Qty  Harga  Total" + Convert.ToString((char)10);
                s += "------------------------------------------------" + Convert.ToString((char)10);
                //    123456789 123456789 123456789 12  999  9,999,999
                //s += "123456789012345678901234567890123456789012345678" + Convert.ToString((char)10);


                List<JualDetailVIewModel> listJualDetail = new JualDetailCRUD().GetJualDetailViewModelAsListBy(KodeJual);
                foreach (var jualDetail in listJualDetail)
                {
                    String cetak = String.Format("{0,0} {1,19} {2,7} {3,6}", jualDetail.NamaBarang, jualDetail.Jumlah.ToString(), jualDetail.HargaJual.ToString("#,###"), (jualDetail.HargaJual * jualDetail.Jumlah).ToString("#,###"));
                    s += (cetak + Convert.ToString((char)10));
                }
                s += "------------------------------------------------" + Convert.ToString((char)10);
                s += "                         Total   :   " + String.Format("{0, 10} ", oJual.TotalBayar.ToString("#,###")) + Convert.ToString((char)10);
                s += "                         Bayar   :   " + String.Format("{0, 10} ", oJual.UangBayar.ToString("#,###")) + Convert.ToString((char)10);
                s += "                         Kembali :   " + String.Format("{0, 10} ", (oJual.UangBayar - oJual.TotalBayar).ToString("#,###")) + Convert.ToString((char)10);
                s += "------------------------------------------------" + Convert.ToString((char)10);
           

            // Cetak Footer
            s += Convert.ToString((char)27) + "a" + Convert.ToString((char)1); // rata tengah
            s += Convert.ToString((char)27) + "!" + Convert.ToString((char)8); // double High                                                       
            s += VarGlobal.Footer1 + Convert.ToString((char)10);
            s += VarGlobal.Footer2 + Convert.ToString((char)10);
            s += VarGlobal.TerimaKasih + Convert.ToString((char)10);
            s += Convert.ToString((char)27) + "!" + Convert.ToString((char)0); // normal High
            s += Convert.ToString((char)27) + "a" + Convert.ToString((char)0);  // rata kiri            
            s += " " + Convert.ToString((char)10);
            s += Convert.ToString((char)29) + Convert.ToString((char)86) + Convert.ToString((char)66) + Convert.ToString((char)0);

            // Cetakkkkkkkkkkkkkkkk
            //RawPrinterHelper.SendStringToPrinter("EPSON TM-T81 Receipt", s);  
            RawPrinterHelper.SendStringToPrinter(VarGlobal.NamaPrinter, s);

            //s += "(0XH1D)" + "V" + Convert.ToString((char)66) + Convert.ToString((char)0);

        }
    }
}
