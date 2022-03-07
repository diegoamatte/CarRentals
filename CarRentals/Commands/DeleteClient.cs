using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class DeleteClient
    {
        public record Command(Guid Id) : IRequest;
        public class Handler : IRequestHandler<Command, Unit>
        {
            private IService<Client> _clientService;
            public Handler(IService<Client> clientService)
            {
                _clientService = clientService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _clientService.DeleteAsync(request.Id);
                return Unit.Value;
            }
        }
    }
}
