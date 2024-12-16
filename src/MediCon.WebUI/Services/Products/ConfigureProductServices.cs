using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.Products.Services;

namespace MediCon.WebUI.Services.Products;

public class ConfigureProductServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}
