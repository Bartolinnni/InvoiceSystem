using InvoiceSystem.EMAG.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Services
{
    public class OrderAttachmentService : IOrderAttachmentService
    {
        private readonly IInvoiceService _invoiceService;
        public OrderAttachmentService(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async void UploadingOrderAttachment()
        {
            var taskOrderAttachmentData = await _invoiceService.DownloadingInvoicesToAttachment();
            var orderAttachmentDataList = taskOrderAttachmentData;
            for (int i = 0; i < orderAttachmentDataList.Count(); i++)
            {
                var options = new RestClientOptions($"https://marketplace-api.emag.{orderAttachmentDataList[i].Lang}/api-3")
                {
                    Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["EMAG_USER_NAME"], ConfigurationManager.AppSettings["EMAG_PASSWORD"])
                };

                var client = new RestClient(options);
                var request = new RestRequest("order/attachments/save", Method.Post);
                var body = new
                {
                    type = orderAttachmentDataList[i].Type,
                    url = orderAttachmentDataList[i].Url,
                    order_id = orderAttachmentDataList[i].OrderId,
                };
                request.AddJsonBody(body);
                var response = await client.ExecuteAsync(request);
                client.Dispose();
            }
        }
    }
}
