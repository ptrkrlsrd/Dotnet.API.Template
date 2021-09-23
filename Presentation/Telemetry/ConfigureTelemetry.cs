using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace Template.API
{
    public static class ConfigureTelemetry
    {
        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, string serviceName, string [] sources)
        {
            services.AddOpenTelemetryTracing((builder) =>
                builder
                    .SetResourceBuilder(ResourceBuilder
                        .CreateDefault().AddService(serviceName))
                    .AddSource(sources)
                    .AddAspNetCoreInstrumentation()
                    .AddJaegerExporter()
            );
            
            return services;
        }
    }
}