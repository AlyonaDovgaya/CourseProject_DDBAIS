using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ProductType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
