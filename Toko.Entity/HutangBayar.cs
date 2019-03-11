using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("hutang_bayar")]
    [PetaPoco.PrimaryKey("IdHutangBayar")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class HutangBayar
    {

        [PetaPoco.Column]
        public int IdHutangBayar { get; set; }
        [PetaPoco.Column]
        public int Nominal { get; set; }
        [PetaPoco.Column]
        public string Kode { get; set; }
        [PetaPoco.Column]
        public string NoKustomer { get; set; }
        [PetaPoco.Column]
        public DateTime Waktu { get; set; }
        [PetaPoco.Column]
        public string Keterangan { get; set; }
        [PetaPoco.Column]
        public string Kasir { get; set; }
    }
}
