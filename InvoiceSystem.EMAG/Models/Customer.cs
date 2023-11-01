using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Models
{
    public class Customer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comapny { get; set; }
        public string Gender { get; set; }
        public string Code { get; set; }
        public string RegistrationNumber { get; set; }
        public string Bank { get; set; }
        public string IBan { get; set; }
        public string Fax { get; set; }
        public int? LegalEntity { get; set; }
        public int? IsVatPayer { get; set; }
        public string Phone1 {get; set;}
        public string Phone2 { get; set;}
        public string Phone3 { get; set;}
        public string BlillingName { get; set;}
        public string BilingPhne { get; set; }
        public string BillingCountry { get; set;}
        public string BillingSuburb { get; set; }
        public string BillingCity { get; set; }
        public string BillingStreet { get; set; }
        public string BillingPostCode { get; set; }
        public string ShippingContact { get; set; }
        public string ShippingPhone { get; set; }

    }
}
