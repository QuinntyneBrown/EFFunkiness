using EFFunkiness.Server.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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


    public class GetClients
    {
        public record Request(): IRequest<Response>;

        public record Response(List<ClientDto> Clients);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEFFunkinessDbContext _context;

            public Handler(IEFFunkinessDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var clients = new List<ClientDto>();

                var query = from client in _context.Clients
                            select client;

                foreach (var client in query)
                {
                    var user = _context.Users.Find(client.CreatedByUserId);

                    clients.Add(new ClientDto(client.ClientId, client.Name, new UserDto(user.UserId, user.Name)));
                }

                return new Response(clients);

            }
        }
    }



    public record ClientDto(Guid ClientId, string Name, UserDto CreatedByUser);

    public record UserDto(Guid UserId, string Name);

}
