using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата отправки")]
        public DateTime DispatchDate { get; set; }

        [Display(Name = "Доставка")]
        public string Delivery { get; set; }

        [Display(Name = "Объём")]
        public int Volume { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }

        [Display(Name = "Сотрудник")]
        public string Employee { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
