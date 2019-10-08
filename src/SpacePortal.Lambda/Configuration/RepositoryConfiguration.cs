using Microsoft.Extensions.DependencyInjection;


namespace SpacePortal.Lambda.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            // add repositories
            return services;
        }
    }
}
