using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("barang")]
    [PetaPoco.PrimaryKey("KodeBarang")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class Barang
    {

        [PetaPoco.Column]
        public string KodeBarang { get; set; }
        [PetaPoco.Column]
        public string NamaBarang { get; set; }
        [PetaPoco.Column]
        public int HargaBeli { get; set; }
        [PetaPoco.Column]
        public int HargaJual { get; set; }
        [PetaPoco.Column]
        public int Stock { get; set; }
    }
}
