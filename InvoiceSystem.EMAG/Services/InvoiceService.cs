using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.EMAG.Models;
using RestSharp.Authenticators;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace InvoiceSystem.EMAG.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IOrderService _orderDataDownloading;
        public InvoiceService(IOrderService orderDataDownloading)
        {
            _orderDataDownloading = orderDataDownloading;
        }
        // Metoda DataUploading wywołuje metode DownloadDataFromAllCountries() która zwróci liste zamówień ze wszystkich niepolskich serwisów, o statusie prepered i bez faktury z EMAG-u. Metoda DataUploading wykonuje Post Request na Fakturownia.pl tworząc fakture. Metoda DataUploading zwraca długość listy faktur która będzie wykorzystana do pobrania odpowiedniej ilosci faktur z fakturowni.pl żeby określić ID pdf-ów tych faktur i móc przekazać odpowiedni url na platformę EMAG-u (prawdopodobnie mógłbym wykorzystać response z Post Request-a, bo pewnie w jakiejś formie zwraca obiekt faktura, ale w dokumentacji nie jest uściślone w jakiej).
        public async Task<int> DataUploading()
        {

            var orderDataAllCountries = await _orderDataDownloading.DownloadDataFromAllCountries();
            List<Invoice> invoicesList = new List<Invoice>();

            invoicesList = orderDataAllCountries.Select(x => new Invoice
            {
                kind = "vat",
                issue_date = x.Date,
                payment_to = x.Date.AddDays(7),
                seller_name = ConfigurationManager.AppSettings["SellerName"],
                seller_tax_no = ConfigurationManager.AppSettings["SellerTaxNo"],
                oid = x.Id,
                buyer_name = x.Customer.Name,
                buyer_email = x.Customer.Email,
                buyer_city = x.Customer.BillingCity,
                buyer_tax_no = x.Customer.BlillingName,
                buyer_company = x.Customer.Comapny,
                buyer_post_code = x.Customer.Code,
                positions = ParsePostionField(x.Products),
                lang = x.InvoiceLanguage, // język faktury
            }).ToList();


            var options = new RestClientOptions($"https://YOUR_DOMAIN.fakturownia.pl/invoices.json")
            {
                Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["API_TOKEN"], "X")
            };

            var client = new RestClient(options);

            for( int i = 0; i< invoicesList.Count; i++)
            {
                var body = new 
                {
                    api_token = ConfigurationManager.AppSettings["API_TOKEN"],
                    invoice = invoicesList[i]
                };
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);
            }

            return invoicesList.Count;
        }

        public async Task<List<OrderAttachment>> DownloadingInvoicesToAttachment()
        {
            int invoicesNumber = await DataUploading();

            var options = new RestClientOptions($"https://twojaDomena.fakturownia.pl/invoices.json?")
            {
                Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["API_TOKEN"], "X")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"per_page={invoicesNumber}&api_token={ConfigurationManager.AppSettings["API_TOKEN"]}&page=1", Method.Get); //pobieram liste ostatnich faktur o długości takiej jaką dodałem
            var response = await client.ExecuteAsync(request);
            var jsonLatestInvoiceData = JArray.Parse(response.Content);
            var parsedOrderAttachmentData = ParseLatestInvoiceData(jsonLatestInvoiceData);

            return parsedOrderAttachmentData;
        }

        private List<OrderAttachment> ParseLatestInvoiceData(JArray jsonLatestInvoiceData)
        {
            var parsedOrderAttachmentData = jsonLatestInvoiceData.Select(t => new OrderAttachment
            {
                OrderId = (int)t["oid"],
                Url = $"https://twojaDomena.fakturownia.pl/invoices/{(string)t["invoice_template_id"]}.pdf?api_token={ConfigurationManager.AppSettings["API_TOKEN"]}",
                Type = 1,
                Lang = (string)t["lang"]
            }).ToList();
            return parsedOrderAttachmentData;
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
