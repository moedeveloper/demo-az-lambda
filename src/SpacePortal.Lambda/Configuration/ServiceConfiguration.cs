using Amazon;
using Amazon.SimpleEmail;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SpacePortal.Lambda.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
