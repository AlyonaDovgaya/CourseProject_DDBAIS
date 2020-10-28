using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Display(Name = "Заказчик")]
        public string CustomerName { get; set; }

        [Display(Name = "Адрес")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Телефон")]
        public int TelNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
