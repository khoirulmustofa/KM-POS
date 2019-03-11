using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Entity;

namespace Toko.Data
{
    public class KustomerCRUD : BaseCRUD
    {
        public DataTable GetAllKustomerAsTable()
        {
            string sql = string.Format(@"SELECT
                                        `NoKustomer`,
                                        `Nama`,
                                        `Alamat`,
                                        `Email`,
                                        `Hanphone`,
                                        `Point`
                                        FROM `kustomer`");
            return db.ExecuteReader(sql, null);
        }

        public DataTable GetKustomerByAsTable(string nilai, string kriteria)
        {
            string sql = string.Format(@"SELECT
                                        `NoKustomer`,
                                        `Nama`,
                                        `Alamat`,
                                        `Email`,
                                        `Hanphone`,
                                        `Point`
                                        FROM `kustomer`
                                        WHERE " + kriteria + " LIKE '%" + nilai + "%'");
            return db.ExecuteReader(sql, null);
        }

        public bool DeleteKustomerBy(string NoKustomer)
        {
            bool result = false;
            string sql = string.Format(@"DELETE
                                        FROM `kustomer`
                                        WHERE `NoKustomer` = @0");
            result = db.Execute(sql, NoKustomer) == 1;
            return result;
        }

        public bool UpdateKustomer(Kustomer oKustomer)
        {
            bool result = false;
            string sql = string.Format(@"UPDATE `kustomer`
                                        SET `Nama` = @1,
                                        `Alamat` = @2,
                                        `Email` = @3,
                                        `Hanphone` = @4,
                                        `Point` = @5
                                        WHERE `NoKustomer` = @0");
            result = db.Execute(sql, oKustomer.NoKustomer,
                                     oKustomer.Nama,
                                     oKustomer.Alamat,
                                     oKustomer.Email,
                                     oKustomer.Hanphone,
                                     oKustomer.Point) == 1;
            return result;
        }

        public bool InsertKustomer(Kustomer oKustomer)
        {
            bool result = false;
            string sql = string.Format(@"`Nama`,
                                        `Alamat`,
                                        `Email`,
                                        `Hanphone`,
                                        `Point`)
                                        VALUES (@0,
                                                @1,
                                                @2,
                                                @3,
                                                @4)");
            result = db.Execute(sql,oKustomer.Nama,
                                     oKustomer.Alamat,
                                     oKustomer.Email,
                                     oKustomer.Hanphone,
                                     oKustomer.Point) == 1;
            return result;
        }

        public Kustomer GetKustomerBy(string NoKustomer)
        {
            string sql = string.Format(@"SELECT
                                        `NoKustomer`,
                                        `Nama`,
                                        `Alamat`,
                                        `Email`,
                                        `Hanphone`,
                                        `Point`
                                        FROM `kustomer`
                                        WHERE `NoKustomer` = @0");
            return db.SingleOrDefault<Kustomer>(sql, NoKustomer);
        }
    }
}