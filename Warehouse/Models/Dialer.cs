using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Dialer
    {
        public int DialerId { get; set; }

        [Display(Name = "Поставщик")]
        public string DialerName { get; set; }

        [Display(Name = "Адрес")]
        public string DialerAddress { get; set; }

        [Display(Name = "Телефон")]
        public int TelNumber { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
    }
}
