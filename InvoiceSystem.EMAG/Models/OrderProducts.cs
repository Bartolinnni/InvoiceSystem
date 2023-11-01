using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Models
{
    public class OrderProducts
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? Status { get; set; }
        public int? Quantity { get; set; }
        public decimal? SalePrice { get; set; }
        
    }
}
