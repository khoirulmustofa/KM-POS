using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Entity
{
    [PetaPoco.TableName("users")]
    [PetaPoco.PrimaryKey("UserId")]
    [PetaPoco.ExplicitColumns]
    [Serializable]
    public class Users
    {

        [PetaPoco.Column]
        public int UserId { get; set; }
        [PetaPoco.Column]
        public string UserName { get; set; }
        [PetaPoco.Column]
        public string Passwords { get; set; }
        [PetaPoco.Column]
        public string NamaLengkap { get; set; }
        [PetaPoco.Column]
        public string Alamat { get; set; }
        [PetaPoco.Column]
        public string NoKTP { get; set; }
        [PetaPoco.Column]
        public string Level { get; set; }
    }
}
