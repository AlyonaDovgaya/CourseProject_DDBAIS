using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Dialer
    {
        public int DialerId { get; set; }
        public string DialerName { get; set; }
        public string DialerAddress { get; set; }
        public int TelNumber { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}
