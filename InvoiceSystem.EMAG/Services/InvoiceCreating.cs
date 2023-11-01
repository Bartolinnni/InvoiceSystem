using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.EMAG.Models;
using RestSharp.Authenticators;

namespace InvoiceSystem.EMAG.Services
{
    public class InvoiceCreating
    {
        private readonly IOrderDataDownloading _orderDataDownloading;
        public InvoiceCreating(IOrderDataDownloading orderDataDownloading)
        {
            _orderDataDownloading = orderDataDownloading;
        }

        public async Task<List<Invoice>> DataUploading()
        {

            var orderDataAllCountries = await _orderDataDownloading.DownloadDataFromAllCountries();
            List<Invoice> invoicesList = new List<Invoice>();

            invoicesList = orderDataAllCountries.Select(x => new Invoice
            {
                kind = "vat",
                issue_date = x.Date,
                payment_to = x.Date.AddDays(7),
                seller_name = "ZROBIC",
                seller_tax_no = "ZROBIC",
                buyer_name = x.Customer.Name,
                buyer_email = x.Customer.Email,
                buyer_city = x.Customer.BillingCity,
                buyer_tax_no = x.Customer.BlillingName,
                positions = ParsePostionField(x.Products)
            }).ToList();


            var options = new RestClientOptions($"https://YOUR_DOMAIN.fakturownia.pl/invoices.json")
            {
                Authenticator = new HttpBasicAuthenticator("API_TOKEN" , "X")
            };

            var client = new RestClient(options);

            for( int i = 0; i< invoicesList.Count; i++)
            {
                var body = new 
                {
                    api_token = "ZROBIC",
                    invoice = invoicesList[i]
                };
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);
            }
        }

        private List<Position> ParsePostionField(List<OrderProducts> listOfProducts)
        {
            if(listOfProducts == null || listOfProducts.Count == 0) return null;
            List<Position> listOfPositions = listOfProducts.Select(x => new Position
            {
                product_id = x.ProductId,
                quantity = x.Quantity,
                total_price_gross = x.SalePrice

            }).ToList();
            return listOfPositions;
        }
    }
}
