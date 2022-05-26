using System;
using Microsoft.AspNetCore.Builder;

namespace Template.API;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddIf(
            this IApplicationBuilder applicationBuilder,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (applicationBuilder is null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                applicationBuilder = action(applicationBuilder);
            }

            return applicationBuilder;
        }
}