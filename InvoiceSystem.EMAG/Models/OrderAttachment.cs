using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Models
{
    public class OrderAttachment
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public string Url { get; set; } 
        public int? Type { get; set; }
    }
}
