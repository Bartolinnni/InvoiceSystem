using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int? IsComplete { get; set; }
        public int? Type { get; set; }
        public int PaymentModeId { get; set; }
        public string? DetaledPaymentMethod { get; set; }
        public string? DeliveryMode { get; set; }
        public string? Details { get; set; }
        public DateTime Date { get; set; }
        public int? PaymentStatus { get; set; }
        public int? CashedCo { get; set; }
        public decimal? ShippingTax { get; set; }
        public Customer? Customer { get; set; }
        public bool IsContainingInvoice { get; set; }
        public string InvoiceLanguage { get; set; }
        public List<OrderProducts> Products { get; set; }
    }
}
