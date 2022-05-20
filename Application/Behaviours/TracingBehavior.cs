using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Template.API.Application.Behavior;

public static class MediatrTracing
{
    public static ActivitySource ActivitySource = new ActivitySource(MediatrConstants.ActivitySourceName);
}

public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        using var activity = MediatrTracing.ActivitySource.StartActivity(request?.ToString() ?? MediatrConstants.DefaultActivityName);
        
        var response = await next();

        return response;
    }
}
