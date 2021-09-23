using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Commands;

namespace Template.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(PingCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}