using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class DeleteRental
    {
        public record Command(Guid Id) : IRequest;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private IService<Rental> _rentalService;

            public Handler(IService<Rental> rentalService)
            {
                _rentalService = rentalService;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _rentalService.DeleteAsync(request.Id);
                return Unit.Value;
            }
        }
    }
}
