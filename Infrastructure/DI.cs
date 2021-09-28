using System.Reflection;
using Template.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPongRepository), typeof(PongRepository));
            return services;
        }
    }
}