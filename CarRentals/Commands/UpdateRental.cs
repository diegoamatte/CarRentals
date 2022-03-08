using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;
using AutoMapper;

namespace CarRentals.Commands
{
    public static class UpdateRental
    {
        public record Command(Guid Id, RentalDto Rental) : IRequest;

        public class Handle : IRequestHandler<Command, Unit>
        {
            private IService<Rental> _rentalService;
            private IMapper _mapper;

            public Handle(IService<Rental> rentalService, IMapper mapper)
            {
                _rentalService = rentalService;
                _mapper = mapper;
            }
            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                await _rentalService.UpdateAsync(request.Id, _mapper.Map<Rental>(request.Rental));
                return Unit.Value;
            }
        }
    }
}
