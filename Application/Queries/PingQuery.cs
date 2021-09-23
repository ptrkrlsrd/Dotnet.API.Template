using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Template.API.Application.Queries
{
    public class PingQuery : IRequest<string> { }

    public class PingQueryHandler : IRequestHandler<PingQuery, string>
    {
        public Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
