using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Tests.Extensions;

public static class ServiceExtensions
{
    public static void SwapService<TService, TImpl>(this IServiceCollection services, Func<IServiceProvider, TImpl> implementationFactory, ServiceLifetime serviceType) where TImpl : class
    {
        if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == serviceType))
        {
            var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == serviceType).ToList();
            foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
            {
                services.Remove(serviceDescriptor);
            }
        }

        services.Add(new ServiceDescriptor(typeof(TService), implementationFactory, serviceType));
    }
}
