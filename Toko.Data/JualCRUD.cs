using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Entity;

namespace Toko.Data
{
    public class JualCRUD : BaseCRUD
    {
        public bool InsertJual(Jual oJual)
        {
            bool result = false;
            string sql = string.Format(@"INSERT INTO `jual`
                                        (`KodeJual`,
                                        `Waktu`,
                                        `NoKustomer`,
                                        `Kasir`,
                                        `Lokasi`,
                                        `TotalBayar`,
                                        `UangBayar`,
                                        `Keterangan`)
                                        VALUES (@0,
                                                @1,
                                                @2,
                                                @3,
                                                @4,
                                                @5,
                                                @6,
                                                @7)");
            result = db.Execute(sql, oJual.KodeJual,
                                    oJual.Waktu,
                                    oJual.NoKustomer,
                                    oJual.Kasir,
                                    oJual.Lokasi,
                                    oJual.TotalBayar,
                                    oJual.UangBayar,
                                    oJual.Keterangan) == 1;
            return result;
        }

        public Jual GetJualBy(string KodeJual)
        {
            string sql = string.Format(@"SELECT
                                        `KodeJual`,
                                        `Waktu`,
                                        `NoKustomer`,
                                        `Kasir`,
                                        `Lokasi`,
                                        `TotalBayar`,
                                        `UangBayar`,
                                        `Keterangan`
                                        FROM `jual`
                                        WHERE `KodeJual` = @0");
            return db.SingleOrDefault<Jual>(sql, KodeJual); 
        }

        public DataTable GetLapPenjulanBy(DateTime dtStart, DateTime dtEnd)
        {
            string sql = string.Format(@"SELECT JD.`KodeJual`,JD.`KodeBarang`,B.`NamaBarang`,JD.`Jumlah`,JD.`HargaBeli`,JD.`HargaJual`
                                        FROM `jual_detail` AS JD
                                        JOIN `jual` AS J
                                        ON J.`KodeJual`=JD.`KodeJual`
                                        JOIN `Barang` AS B
                                        ON B.`KodeBarang`= JD.`KodeBarang`
                                        WHERE J.`Waktu` BETWEEN @0 AND @1");
            return db.ExecuteReader(sql, dtStart, dtEnd);
        }
    }
}
