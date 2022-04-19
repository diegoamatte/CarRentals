using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public static class GetRentals
    {
        public record Query(): IRequest<Response>;
        public record Response(IEnumerable<RentalDto> Rentals);

        public class Handler : IRequestHandler<Query, Response>
        {
            private IMapper _mapper;
            private IService<Rental> _rentalService;

            public Handler(IService<Rental> rentalService, IMapper mapper)
            {
                _mapper = mapper;
                _rentalService = rentalService;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response(_mapper.Map<IEnumerable<RentalDto>>(await _rentalService.GetAsync()));
            }
        }
    }
}
