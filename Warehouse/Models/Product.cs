using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? ProductTypeId { get; set; }

        [Display(Name = "Название товара")]
        public string ProductName { get; set; }

        [Display(Name = "Условия хранения")]
        public string Storage { get; set; }

        [Display(Name = "Упаковка")]
        public string Packaging { get; set; }

        [Display(Name = "Срок годности")]
        public int ExpirationDate { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
