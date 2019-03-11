using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Entity;

namespace Toko.Data
{
    public class HutangBayarCRUD : BaseCRUD
    {
        public void InsertHutangBayar(HutangBayar oHutangBayar)
        {
            string sql = string.Format(@"INSERT INTO `hutang_bayar`
                                        (`Nominal`,
                                        `Kode`,
                                        `NoKustomer`,
                                        `Waktu`,
                                        `Keterangan`,
                                        `Kasir`)
                                        VALUES (@0,
                                        @1,
                                        @2,
                                        @3,
                                        @4,
                                        @5)");
            db.Execute(sql, oHutangBayar.Nominal,
                            oHutangBayar.Kode,
                            oHutangBayar.NoKustomer,
                            oHutangBayar.Waktu,
                            oHutangBayar.Keterangan,
                            oHutangBayar.Kasir);
        }

        public DataTable GetHutangBayarKustomerByKustomerAsTable(string NoKustomer)
        {
            string sql = string.Format(@"SELECT 
                                     `Waktu`,
                                     @@Hutang:=IF(`Kode`='HUTANG',`Nominal`,0) AS HUTANG,
                                     @@bayar:=IF(`Kode`='BAYAR',`Nominal`,0) AS BAYAR,
                                     @@Saldo:=@@Saldo+@@bayar-@@Hutang AS SALDO,
                                     `Keterangan`
                                     FROM `hutang_bayar`,(SELECT @@Saldo:=0) SAL
                                     WHERE `NoKustomer` = @0
                                     ORDER BY `Waktu`");
            return db.ExecuteReader(sql, NoKustomer);
        }
    }
}
