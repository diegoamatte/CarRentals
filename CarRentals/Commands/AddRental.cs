using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class AddRental
    {
        public record Command(RentalDto Rental) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private IService<Rental> _rentalService;
            private IMapper _mapper;

            public Handler(IService<Rental> rentalService, IMapper mapper)
            {
                _rentalService = rentalService;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var rental =  await _rentalService.SaveAsync(_mapper.Map<Rental>(request.Rental));
                    return rental.Id;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
