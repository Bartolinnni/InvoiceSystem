using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using InvoiceSystem.EMAG.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceSystem.EMAG
{
    public class Function1
    {
        private readonly IOrderAttachmentService _orderAttachmentService;
        public Function1(IOrderAttachmentService orderAttachmentService)
        {
            _orderAttachmentService = orderAttachmentService;
        }
        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/30  *  *  *  *")]TimerInfo myTimer, ILogger log)
        {
            _orderAttachmentService.UploadingOrderAttachment();
        }
    }
}
