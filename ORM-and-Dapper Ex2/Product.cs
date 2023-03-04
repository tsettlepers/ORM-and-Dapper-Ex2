using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Ex2
{
    public class Product : OurDBObjects
    {
        [Key]
        public long ProductID { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryID { get; set; }
        public byte? OnSale { get; set; }
        public string? StockLevel { get; set; }
        public DateTime? Deleted { get; set; }
        [Key]
        public DateTime Updated { get; set; }               //This isn't really a key, but we need it built into the where clause of the update statement for concurrency purposes.

        public override string Summary()
        {
            return $"{ProductID}, {Name} @ {Price} [{base.Summary()}]";
        }
    }
}
