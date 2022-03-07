using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class UpdateClient
    {
        public record Command(Guid Id, ClientDto Client) : IRequest;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private IService<Client> _clientService;
            private IMapper _mapper;

            public Handler(IService<Client> clientService, IMapper mapper)
            {
                _clientService = clientService;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _clientService.UpdateAsync(request.Id, _mapper.Map<Client>(request.Client));
                return Unit.Value;
            }
        }
    }
}
