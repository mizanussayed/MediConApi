using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Mail.Service;
using DinkToPdf.Contracts;
using DinkToPdf;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.Mail.Helper
{
    internal class MailDependencyResolver : IInjectServices
    {
        public void Configure(IServiceCollection services)
        {
            services.AddScoped<MailService>();
            // Register DinkToPdf services
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

        }
    }
}