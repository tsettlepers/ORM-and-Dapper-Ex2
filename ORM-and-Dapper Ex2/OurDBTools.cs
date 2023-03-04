using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Ex2
{
    public abstract class OurDBTools
    {
        public static IDbConnection Conn { get; set; }

        public abstract OurDBObjects Get(int id);
        public abstract IEnumerable<OurDBObjects> GetAll();
        public abstract bool Create(OurDBObjects newRec);
        public abstract bool Update(OurDBObjects thisRec);
        public abstract bool Delete(int id);
    }
}
