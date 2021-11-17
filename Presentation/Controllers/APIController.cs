using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.API.Application.Queries;

namespace Template.API.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;
        private readonly IMediator _mediator;

        public APIController(ILogger<APIController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get() => Ok(await _mediator.Send(new PingQuery()));

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PingCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
