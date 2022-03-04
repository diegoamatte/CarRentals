using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class AddRental
    {
        public record AddRequestCommand(Rental Rental) : IRequest<Rental>;

        public class Handler : IRequestHandler<AddRequestCommand, Rental>
        {
            private IService<Rental> _rentalService;

            public Handler(IService<Rental> rentalService)
            {
                _rentalService = rentalService;
            }

            public async Task<Rental> Handle(AddRequestCommand request, CancellationToken cancellationToken)
            {
                return await _rentalService.SaveAsync(request.Rental);
            }
        }
    }
}
