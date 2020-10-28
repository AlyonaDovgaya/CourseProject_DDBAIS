using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Storage
    {
        public int StorageId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата поступления")]
        public DateTime ReceiptDate { get; set; }

        [Display(Name = "Объём")]
        public int Volume { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }

        [Display(Name = "Сотрудник")]
        public string Employee { get; set; }
        public int? ProductId { get; set; }
        public int? DialerId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Dialer Dialer { get; set; }
    }
}
