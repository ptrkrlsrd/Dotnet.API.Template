using System.Threading;
using System.Threading.Tasks;
using Template.Infrastructure.Repositories;
using MediatR;
using Template.API.Application.Models;

namespace Template.API.Application.Queries;

public record PingCommand : IRequest<Ping>;

public class PingCommandHandler : IRequestHandler<PingCommand, Ping>
{
    private readonly IPongRepository _pongRepository;
    private readonly PingMapper _mapper;

    public PingCommandHandler(IPongRepository pongRepository)
    {
        _pongRepository = pongRepository;
        _mapper = new PingMapper();
    }
    public Task<Ping> Handle(PingCommand request, CancellationToken cancellationToken)
    {
        PongEntity pingDto = _pongRepository.Get();
        Ping ping = _mapper.Map(pingDto);
        return Task.FromResult(ping);
    }
}
