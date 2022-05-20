using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Template.API.Application.Behavior;
using Template.API.Application.Queries;
using Template.Infrastructure;

namespace Template.API.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(typeof(PingQuery).GetTypeInfo().Assembly);
        services.RegisterInfrastructure(); // Fix this
        return services;
    }

    public static IServiceCollection AddTracingMiddleware(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
        return services;
    }
    
    public static IServiceCollection AddLoggingMiddleware(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}
