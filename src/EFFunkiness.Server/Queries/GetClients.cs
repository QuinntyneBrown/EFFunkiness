using EFFunkiness.Server.Data;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EFFunkiness.Server.Queries
{
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
}
