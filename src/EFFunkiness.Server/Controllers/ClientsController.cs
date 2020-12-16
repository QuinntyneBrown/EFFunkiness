using EFFunkiness.Server.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace EFFunkiness.Server.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator) => _mediator = mediator;
        
        [HttpGet(Name = "GetClientsRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetClients.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetClients.Response>> Get()
            => await _mediator.Send(new GetClients.Request());           
    }
}
