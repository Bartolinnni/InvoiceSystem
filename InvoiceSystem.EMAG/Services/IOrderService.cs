using InvoiceSystem.EMAG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.EMAG.Services
{
    public interface IOrderService
    {
        public Task<List<Order>> DownloadDataFromAllCountries();
        public Task<List<Order>> DownloadData(string country);

    }
}
