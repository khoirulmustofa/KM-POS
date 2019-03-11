using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Entity;

namespace Toko.Data
{
    public class BarangCRUD : BaseCRUD
    {
        public DataTable GetAllBarangAsTable()
        {
            string sql = string.Format(@"SELECT
                                        `KodeBarang`,
                                        `NamaBarang`,
                                        `HargaBeli`,
                                        `HargaJual`,
                                        `Stock`
                                        FROM `barang`");
            return db.ExecuteReader(sql, null);
        }

        public DataTable GetBarangByAsTable(string nilai, string kriteria)
        {
            string sql = string.Format(@"SELECT
                                        `KodeBarang`,
                                        `NamaBarang`,
                                        `HargaBeli`,
                                        `HargaJual`,
                                        `Stock`
                                        FROM `barang`
                                        WHERE " + kriteria + " LIKE '%" + nilai + "%'");
            return db.ExecuteReader(sql, null);
        }

        public Barang GetBarangByKodeBarang(string kodeBarcode)
        {
            string sql = string.Format(@"SELECT
                                        `KodeBarang`,
                                        `NamaBarang`,
                                        `HargaBeli`,
                                        `HargaJual`,
                                        `Stock`
                                        FROM `barang`
                                        WHERE `KodeBarang` = @0");
            return db.SingleOrDefault<Barang>(sql, kodeBarcode);
        }

        public bool DeleteBarangBy(string kodeBarang)
        {
            bool result = false;
            string sql = string.Format(@"DELETE
                                        FROM `barang`
                                        WHERE `KodeBarang` = @0");
            result = db.Execute(sql, kodeBarang) == 1;
            return result;
        }



        public bool InsertBarang(Barang oBarang)
        {
            bool result = false;
            string sql = string.Format(@"INSERT INTO `barang`
                                        (`KodeBarang`,
                                        `NamaBarang`,
                                        `HargaBeli`,
                                        `HargaJual`,
                                        `Stock`)
                                        VALUES (@0,
                                                @1,
                                                @2,
                                                @3,
                                                @4)");
            result = db.Execute(sql, oBarang.KodeBarang,
                                      oBarang.NamaBarang,
                                      oBarang.HargaBeli,
                                      oBarang.HargaJual,
                                      oBarang.Stock) == 1;
            return result;
        }

        public bool UpdateBarang(Barang oBarang)
        {
            bool result = false;
            string sql = string.Format(@"UPDATE `barang`
                                        SET `NamaBarang` = @1,
                                        `HargaBeli` = @2,
                                        `HargaJual` = @3,
                                        `Stock` = @4
                                        WHERE `KodeBarang` = @0");
            result = db.Execute(sql, oBarang.KodeBarang,
                                     oBarang.NamaBarang,
                                     oBarang.HargaBeli,
                                     oBarang.HargaJual,
                                     oBarang.Stock) == 1;
            return result;
        }
    }
}
