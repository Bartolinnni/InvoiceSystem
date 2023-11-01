using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Models
{
    public class CalculatingStrategy
    {
        public string position { get; set; }
        public string sum { get; set; }
        public string invoice_form_price_kind { get; set; }
    }

    public class Position
    {
        public int? product_id { get; set; }
        public string name { get; set; }
        public string additional_info { get; set; }
        public string discount_percent { get; set; }
        public string discount { get; set; }
        public int? quantity { get; set; }
        public string quantity_unit { get; set; }
        public decimal price_net { get; set; }
        public string tax { get; set; }
        public decimal? price_gross { get; set; }
        public decimal? total_price_net { get; set; }
        public decimal? total_price_gross { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string gtu_code { get; set; }
        public string lump_sum_tax { get; set; }
    }

    public class Invoice
    {
        public string? number { get; set; }
        public string? kind { get; set; }
        public string? income { get; set; }
        public DateTime? issue_date { get; set; }
        public string? place { get; set; }
        public DateTime? sell_date { get; set; }
        public string? category_id { get; set; }
        public string? department_id { get; set; }
        public string? accounting_kind { get; set; }
        public string? seller_name { get; set; }
        public string? seller_tax_no { get; set; }
        public string? seller_tax_no_kind { get; set; }
        public string? seller_bank_account { get; set; }
        public string? seller_bank { get; set; }
        public string? seller_post_code { get; set; }
        public string? seller_city { get; set; }
        public string? seller_street { get; set; }
        public string? seller_country { get; set; }
        public string? seller_email { get; set; }
        public string? seller_www { get; set; }
        public string? seller_fax { get; set; }
        public string? seller_phone { get; set; }
        public string? use_invoice_issuer { get; set; }
        public string? invoice_issuer { get; set; }
        public string? client_id { get; set; }
        public string? buyer_name { get; set; }
        public string? buyer_tax_no { get; set; }
        public string? buyer_tax_no_kind { get; set; }
        public string? disable_tax_no_validation { get; set; }
        public string? buyer_post_code { get; set; }
        public string? buyer_city { get; set; }
        public string? buyer_street { get; set; }
        public string? buyer_country { get; set; }
        public string? buyer_note { get; set; }
        public string? buyer_email { get; set; }
        public string? recipient_id { get; set; }
        public string? recipient_name { get; set; }
        public string? recipient_street { get; set; }
        public string? recipient_post_code { get; set; }
        public string? recipient_city { get; set; }
        public string? recipient_country { get; set; }
        public string? recipient_email { get; set; }
        public string? recipient_phone { get; set; }
        public string? recipient_note { get; set; }
        public string? additional_info { get; set; }
        public string? additional_info_desc { get; set; }
        public string? show_discount { get; set; }
        public string? payment_type { get; set; }
        public string? payment_to_kind { get; set; }
        public DateTime? payment_to { get; set; }
        public string? status { get; set; }
        public string? paid { get; set; }
        public string? oid { get; set; }
        public string? oid_unique { get; set; }
        public string? warehouse_id { get; set; }
        public string? seller_person { get; set; }
        public string? buyer_person { get; set; }
        public string? buyer_first_name { get; set; }
        public string? buyer_last_name { get; set; }
        public string? paid_date { get; set; }
        public string? currency { get; set; }
        public string? lang { get; set; }
        public string? use_oss { get; set; }
        public string? exchange_currency { get; set; }
        public string? exchange_kind { get; set; }
        public string? exchange_currency_rate { get; set; }
        public string? invoice_template_id { get; set; }
        public string? description { get; set; }
        public string? description_footer { get; set; }
        public string? description_long { get; set; }
        public string? invoice_id { get; set; }
        public string? from_invoice_id { get; set; }
        public string? delivery_date { get; set; }
        public string? buyer_company { get; set; }
        public string? additional_invoice_field { get; set; }
        public string? internal_note { get; set; }
        public string? exclude_from_stock_level { get; set; }
        public List<object>? gtu_codes { get; set; }
        public List<object>? procedure_designations { get; set; }
        public List<Position>? positions { get; set; }
        public CalculatingStrategy? calculating_strategy { get; set; }
        public string? split_payment { get; set; }
        public string? accounting_vat_tax_date { get; set; }
        public string? accounting_income_tax_date { get; set; }
        public string? skonto_active { get; set; }
        public string? skonto_discount_date { get; set; }
        public string? skonto_discount_value { get; set; }
        public string? exempt_tax_kind { get; set; }
        public string? corrected_content_before { get; set; }
        public string? corrected_content_after { get; set; }
    }
}
