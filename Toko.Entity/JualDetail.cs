using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("jual_detail")]
    [PetaPoco.PrimaryKey("id")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class JualDetail
    {

        [PetaPoco.Column]
        public int Id { get; set; }
        [PetaPoco.Column]
        public string KodeJual { get; set; }
        [PetaPoco.Column]
        public string KodeBarang { get; set; }
        [PetaPoco.Column]
        public int Jumlah { get; set; }
        [PetaPoco.Column]
        public int HargaBeli { get; set; }
        [PetaPoco.Column]
        public int HargaJual { get; set; }
    }
}



