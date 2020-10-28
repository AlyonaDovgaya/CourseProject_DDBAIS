using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }

        [Display(Name = "Тип")]
        public string TypeName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Особенности")]
        public string Features { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
