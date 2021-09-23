using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.Application.Commands;

namespace Template.Presentation.Controllers
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
        public async Task<ActionResult> Get()
        {
            string result = await _mediator.Send(new PingCommand());
            _logger.LogInformation("returning {Result}", result);
            return Ok(result);
        }
    }
}
