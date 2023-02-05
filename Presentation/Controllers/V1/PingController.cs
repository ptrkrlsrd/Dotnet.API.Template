using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.API.Application.Queries;

namespace Template.API.Presentation.Controllers;

[ApiController]
[ApiVersion(Constants.ApiVersion)]
[Route("api/v{version:apiVersion}/[controller]")]
public class PingController : ControllerBase
{
    private readonly ILogger<PingController> _logger;
    private readonly IMediator _mediator;

    public PingController(ILogger<PingController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<JsonResult> Get() => new(await _mediator.Send(new PingQuery()));

    [HttpPost]
    public async Task<OkResult> Post([FromBody] PingCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
