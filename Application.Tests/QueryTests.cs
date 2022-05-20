using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Template.API.Application.Queries;
using Xunit;

namespace Template.API.Application.Tests;

public class QueryTests
{
    [Fact]
    public async Task PongQuery_Equals_Pong()
    {
        var mediator = new  Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<PingQuery>(), default))
            .Verifiable("Notification was not sent.");

        PingQuery query = new PingQuery();
        PingQueryHandler handler = new PingQueryHandler();

        string x = await handler.Handle(query, new CancellationToken());

        Assert.Equal("Pong", x);
        }
}