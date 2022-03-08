using CarRentals.DTOs;
using AutoMapper;
using CarRentals.Services;
using MediatR;
using CarRentals.Models;

namespace CarRentals.Queries
{
    public static class GetRentalById
    {
        public record Query(Guid Id) : IRequest<Response>;
        public record Response(RentalDto Rental);

        public class Handler : IRequestHandler<Query, Response>
        {
            private IService<Rental> _rentalService;
            private IMapper _mapper;

            public Handler(IService<Rental> rentalService, IMapper mapper)
            {
                _rentalService = rentalService;
                _mapper = mapper;

            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response(_mapper.Map<RentalDto>(await _rentalService.GetByIdAsync(request.Id)));
            }
        }
    }
}
