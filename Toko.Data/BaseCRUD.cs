using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toko.Data
{

    public class BaseCRUD
    {
        public PetaPoco.Database db = null;
        public BaseCRUD()
        {
            db = new PetaPoco.Database("TokoConstring");
        }
    }
}
