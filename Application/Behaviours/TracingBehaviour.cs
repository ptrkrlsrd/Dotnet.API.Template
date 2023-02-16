using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Template.API.Application.Behavior;

public static class MediatrTracing
{
    public static readonly ActivitySource ActivitySource = new(MediatrConstants.ActivitySourceName);
}

internal sealed class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using Activity activity = MediatrTracing.ActivitySource.StartActivity(request?.ToString() ?? MediatrConstants.DefaultActivityName);

        TResponse response = await next();

        return response;
    }
}
