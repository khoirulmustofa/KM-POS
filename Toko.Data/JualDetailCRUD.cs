using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toko.Entity;

namespace Toko.Data
{
    public class JualDetailCRUD : BaseCRUD
    {
        public void InsertJualDetail(JualDetail oJualDetail)
        {
            string sql = string.Format(@"INSERT INTO `jual_detail`
                                        ( `KodeJual`,
                                        `KodeBarang`,
                                        `Jumlah`,
                                        `HargaBeli`,
                                        `HargaJual`)
                                        VALUES (@0,
                                        @1,
                                        @2,
                                        @3,
                                        @4)");

            db.Execute(sql, oJualDetail.KodeJual,
                            oJualDetail.KodeBarang,
                            oJualDetail.Jumlah,
                            oJualDetail.HargaBeli,
                            oJualDetail.HargaJual);

        }

        public List<JualDetailVIewModel> GetJualDetailViewModelAsListBy(string KodeJual)
        {
            string sql = string.Format(@"SELECT
                                        JD.`id`,
                                        JD.`KodeJual`,
                                        JD.`KodeBarang`,
                                        B.`NamaBarang`,
                                        JD.`Jumlah`,
                                        JD.`HargaBeli`,
                                        JD.`HargaJual`
                                        FROM `jual_detail` AS JD
                                        JOIN `Barang` AS B
                                        ON B.`KodeBarang` = JD.`KodeBarang`
                                        WHERE JD.`KodeJual` = @0");
            return db.Fetch<JualDetailVIewModel>(sql, KodeJual) as List<JualDetailVIewModel>;
        }
    }
}
