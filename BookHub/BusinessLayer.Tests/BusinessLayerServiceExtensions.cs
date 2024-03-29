using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Tests;

public static class BusinessLayerServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPublisherService, PublisherService>();
    }
    

}