using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public class GetClientsById
    {
        public record Query(Guid id) : IRequest<Response>;

        public record Response(ClientDto Client);

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
                return new Response(_mapper.Map<ClientDto>(await _clientService.GetByIdAsync(request.id)));
            }
        }
    }
}
