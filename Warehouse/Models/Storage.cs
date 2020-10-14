using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Storage
    {
        public int StorageId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Volume { get; set; }
        public decimal Cost { get; set; }
        public string Employee { get; set; }
        public int? ProductId { get; set; }
        public int? DialerId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Dialer Dialer { get; set; }
    }
}
