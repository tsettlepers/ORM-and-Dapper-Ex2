using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Ex2
{
    public abstract class OurDBObjects
    {
        public OurDBObjects()
        {
            IsDirty = false;
        }

        // These are just examples of the kinds of housekeeping properties I'd likely want.
        [Write(false)]
        public string? EntityType { get; set; }
        [Write(false)]
        public DateTime? RetrievedAt { get; set; }
        [Write(false)]
        public bool IsDirty { get; set; }

        public virtual string Summary()
        {
            return $"{EntityType} ({(IsDirty ? "Dirty" : "Clean")})";            
        }

    }
}
