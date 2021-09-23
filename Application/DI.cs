using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.API.Application.Behavior;
using Template.API.Application.Queries;

namespace Template.API.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediatrHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(PingQuery).GetTypeInfo().Assembly);
            return services;
        }

        public static IServiceCollection AddTracingMiddleware(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
            return services;
        }
        
        public static IServiceCollection AddLoggingMiddleware(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}