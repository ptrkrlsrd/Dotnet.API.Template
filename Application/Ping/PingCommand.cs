using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Template.Application.Commands
{
    public class PingCommand : IRequest<string> { }

    public class PingHandler : IRequestHandler<PingCommand, string>
    {
        public Task<string> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
