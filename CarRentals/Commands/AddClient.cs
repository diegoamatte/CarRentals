using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class AddClient
    {
        public record Command(ClientDto Client) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private IService<Client> _clientService;
            private IMapper _mapper;

            public Handler(IService<Client> clientService, IMapper mapper)
            {
                _clientService = clientService;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await _clientService.SaveAsync(_mapper.Map<Client>(request.Client));
                return client.Id;
            }
        }
    }
}
