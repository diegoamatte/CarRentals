using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public static class GetClients
    {
        public record Query() : IRequest<Response>;

        public record Response(IEnumerable<ClientDto> Clients);

        public class Handler : IRequestHandler<Query, Response>
        {
            private IService<Client> _clientService;
            private IMapper _mapper;

            public Handler(IService<Client> clientService, IMapper mapper)
            {
                _clientService = clientService;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response(_mapper.Map<IEnumerable<ClientDto>>(await _clientService.GetAsync()));
            }
        }
    }
}
