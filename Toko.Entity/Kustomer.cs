using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("kustomer")]
    [PetaPoco.PrimaryKey("KodeKustomer")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class Kustomer
    {

        [PetaPoco.Column]
        public string NoKustomer { get; set; }
        [PetaPoco.Column]
        public string Nama { get; set; }
        [PetaPoco.Column]
        public string Alamat { get; set; }
        [PetaPoco.Column]
        public string Email { get; set; }
        [PetaPoco.Column]
        public string Hanphone { get; set; }
        [PetaPoco.Column]
        public int Point { get; set; }
    }
}
