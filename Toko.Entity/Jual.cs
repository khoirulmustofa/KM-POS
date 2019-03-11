using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("jual")]
    [PetaPoco.PrimaryKey("KodeJual")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class Jual
    {

        [PetaPoco.Column]
        public string KodeJual { get; set; }
        [PetaPoco.Column]
        public DateTime Waktu { get; set; }
        [PetaPoco.Column]
        public string NoKustomer { get; set; }
        [PetaPoco.Column]
        public string Kasir { get; set; }
        [PetaPoco.Column]
        public string Lokasi { get; set; }
        [PetaPoco.Column]
        public int TotalBayar { get; set; }
        [PetaPoco.Column]
        public int UangBayar { get; set; }
        [PetaPoco.Column]
        public string Keterangan { get; set; }
    }
}



