using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Template.API.Application.Models;
using Template.Infrastructure.Repositories;

namespace Template.API.Application.Queries;

public record PingQuery : IRequest<Ping>;

public class PingQueryHandler : IRequestHandler<PingQuery, Ping>
{
    private readonly IPongRepository _pongRepository;
    private readonly PingMapper _mapper;

    public PingQueryHandler(IPongRepository pongRepository)
    {
        _pongRepository = pongRepository;
        _mapper = new PingMapper();
    }

    public Task<Ping> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        PongEntity pingDto = _pongRepository.Get();
        Ping ping = _mapper.Map(pingDto);
        return Task.FromResult(ping);
    }
}
