﻿using OnlineMarket.Service.Interfaces.Accounts;
using OnlineMarket.Service.Interfaces.Products;
using OnlineMarket.Service.Services.Accounts;
using OnlineMarket.Service.Services.Products;

namespace OnlineMarket.Web.Configurations.LayerConfigurations;
public static class ServiceLayerConfiguration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAccountService, AccountService>();
    }
}
