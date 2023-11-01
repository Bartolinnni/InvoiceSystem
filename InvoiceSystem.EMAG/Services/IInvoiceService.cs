using InvoiceSystem.EMAG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Services
{
    public interface IInvoiceService
    {
        public Task<int> DataUploading();
        public Task<List<OrderAttachment>> DownloadingInvoicesToAttachment();
    }
}
