using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? TypeId { get; set; }
        public string ProductName { get; set; }
        public string Storage { get; set; }
        public string Packaging { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
