using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.EMAG.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace InvoiceSystem.EMAG.Services
{
    public class OrderDataDownloading : IOrderDataDownloading
    {

        public async Task<List<Order>> DownloadDataFromAllCountries()
        {
            Task<List<Order>> romaniaTask = DownloadData("ro");
            Task<List<Order>> bulgariaTask = DownloadData("bg");
            Task<List<Order>> hungaryTask = DownloadData("hu");
            List<Order> allCountriesData = new List<Order>();

            await Task.WhenAll(romaniaTask, bulgariaTask, hungaryTask);

            List<Order> romaniaOrderData = romaniaTask.Result;
            List<Order> bulgariaOrderData = bulgariaTask.Result;
            List<Order> hungaryOrderData = hungaryTask.Result;

            allCountriesData.AddRange(romaniaOrderData);
            allCountriesData.AddRange(bulgariaOrderData);
            allCountriesData.AddRange(hungaryOrderData);

            return allCountriesData;
        }
        public async Task<List<Order>> DownloadData(string language)
        {
            var options = new RestClientOptions($"https://marketplace-api.emag.{language}/api-3")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };

            var client = new RestClient(options);
            var request = new RestRequest("order/read?status=3", Method.Get); //tworzenie request-u do zamówień o statusie = 3, czyli prepared
            var response = await client.ExecuteAsync(request);
            var jsonOrderData = JArray.Parse(response.Content);
            var parsedOrderData = ParseOrderData(jsonOrderData, language);
            var filteredOrderData = parsedOrderData.Where(a => a.IsContainingInvoice == false).ToList(); // filtruje listę zamówień po tym czy mają fakturę
            return filteredOrderData;


        }
        private static List<Order> ParseOrderData(JArray allOrders, string lang)
        {
            var parsedData = allOrders.Select(t => new Order
            {
                Id = (int)t["id"],
                Status = (int)t["title"],
                IsComplete = (int?)t["is_complete"] ?? null,
                Type = (int?)t["type"] ?? null,
                PaymentModeId = (int)t["payment_mode_id"],
                DetaledPaymentMethod = (string?)t["detailed_payment_method"] ?? null,
                DeliveryMode = (string?)t["delivery_mode"] ?? null,
                Details = (string?)t["detail"] ?? null,
                Date = (DateTime)t["date"],
                PaymentStatus = (int?)t["payment_status"] ?? null,
                CashedCo = (int?)t["cashed_co"] ?? null,
                ShippingTax = (decimal?)t["shipping_tax"] ?? null,
                Customer = ParseCustomerData(t) ?? null,
                IsContainingInvoice = IsContainingInvoice(t),
                InvoiceLanguage = lang,
                Products = ParseProductsData(t)
            }
            ).ToList();

            return parsedData;
        }

        private static List<OrderProducts> ParseProductsData(JToken t)
        {
            if (t["products"].HasValues)
            {
                List<OrderProducts> orderProductsParsedData = t["products"].Select(p => new OrderProducts
                {
                    Id = (int?)p["id"] ?? null,
                    ProductId = (int?)p["product_id"] ?? null,
                    Status = (int?)p["status"] ?? null,
                    Quantity = (int?)p["quantity"] ?? null,
                    SalePrice = (decimal?)p["sale_price"] ?? null
                }).ToList();
                return orderProductsParsedData;
            }
            return null;
        }

        private static Customer? ParseCustomerData(JToken t)
        {
            if (t["customer"].HasValues)
            {
                Customer customerParsedData = new Customer()
                {
                    Id = (int?)t["customer"]["id"] ?? null,
                    Name = (string?)t["customer"]["name"] ?? null,
                    Email = (string?)t["customer"]["email"] ?? null,
                    Comapny = (string?)t["customer"]["comapny"] ?? null,
                    Gender = (string?)t["customer"]["gender"] ?? null,
                    Code = (string?)t["customer"]["code"] ?? null,
                    RegistrationNumber = (string?)t["customer"]["registration_number"] ?? null,
                    Bank = (string?)t["customer"]["bank"] ?? null,
                    IBan = (string?)t["customer"]["iban"] ?? null,
                    Fax = (string?)t["customer"]["fax"] ?? null,
                    LegalEntity = (int?)t["customer"]["legal_entity"] ?? null,
                    IsVatPayer = (int?)t["customer"]["is_vat_payer"] ?? null,
                    Phone1 = (string?)t["customer"]["phone_1"] ?? null,
                    Phone2 = (string?)t["customer"]["phone_2"] ?? null,
                    Phone3 = (string?)t["customer"]["phone_3"] ?? null,
                    BlillingName = (string?)t["customer"]["billing_name"] ?? null,
                    BilingPhne = (string?)t["customer"]["billing_phone"] ?? null,
                    BillingCountry = (string?)t["customer"]["billing_country"] ?? null,
                    BillingSuburb = (string?)t["customer"]["billing_suburb"] ?? null,
                    BillingCity = (string?)t["customer"]["billing_city"] ?? null,
                    BillingStreet = (string?)t["customer"]["billing_street"] ?? null,
                    BillingPostCode = (string?)t["customer"]["billing_postal_code"] ?? null,
                    ShippingContact = (string?)t["customer"]["shipping_contact"] ?? null,
                    ShippingPhone = (string?)t["customer"]["shipping_phone"] ?? null
                };

                return customerParsedData;
                
            }
            
            return null;
        }
        //sprawdzam czy wśród listy attachmentów jest typ == 1 (faktura)
        private static bool IsContainingInvoice(JToken t)
        {
            if(t["attachments"].HasValues)
            {
                List<int> typesOfAttachments = new List<int>();
                for(int i = 0; i < t["attachments"].Count(); i++)
                {
                    typesOfAttachments.Add((int)t["attachments"][i]["type"]);
                }
                if (typesOfAttachments.Contains(1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
