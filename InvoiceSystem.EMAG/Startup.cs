using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using InvoiceSystem.EMAG.Services;
[assembly: FunctionsStartup(typeof(InvoiceSystem.EMAG.Startup))]

namespace InvoiceSystem.EMAG
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IOrderAttachmentService, OrderAttachmentService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
        }
    }
}