using Template.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(IPongRepository), typeof(PongRepository));
        return services;
    }
}
