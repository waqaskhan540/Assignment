using Funda.Domain.Interfaces;
using Funda.Domain.Services.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IApiService, ApiService>();
            services.AddSingleton<IApiRateLimitMonitor, ApiRateLimitMonitor>();
            services.Configure<ApiConfig>(configuration.GetSection("ApiConfig"));

            string apiUrl = configuration["ApiConfig:BaseUri"];
            services.AddHttpClient<IApiService, ApiService>(c =>
            {
                c.BaseAddress = new Uri(apiUrl);
            });
        }
    }
}
