using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.API.Application.Behavior;
using Template.API.Application.Queries;
using Template.Infrastructure;

namespace Template.API.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services) =>
        services
            .RegisterInfrastructure()
            .AddMediatR(typeof(PingQuery).GetTypeInfo().Assembly);

    public static IServiceCollection AddTracingMiddleware(this IServiceCollection services) => 
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));

    public static IServiceCollection AddLoggingMiddleware(this IServiceCollection services) => 
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
}
