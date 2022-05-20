using System.Threading;
using System.Threading.Tasks;
using Template.Infrastructure.Repositories;
using MediatR;

namespace Template.API.Application.Queries;

public class PingCommand : IRequest<string> { }

public class PingCommandHandler : IRequestHandler<PingCommand, string>
{
    private readonly IPongRepository _pongRepository;

    public PingCommandHandler(IPongRepository pongRepository)
    {
        _pongRepository = pongRepository;
    }
    public Task<string> Handle(PingCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_pongRepository.Get());
    }
}