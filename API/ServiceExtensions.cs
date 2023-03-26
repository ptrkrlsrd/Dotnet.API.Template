using System;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Presentation;

public static class ServiceExtensions
{
    public static IServiceCollection AddIf(this IServiceCollection services, bool predicate, ServiceDescriptor service)
    {
      if (services == null)
        throw new ArgumentNullException(nameof (services));
      
      if (service == null)
        throw new ArgumentNullException(nameof (service));

      if (!predicate)
        return services;

      services.Add(service);
      return services;
    }
}
