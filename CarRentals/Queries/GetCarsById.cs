using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public static class GetCarsById
    {
        public record Query(Guid Id) : IRequest<Response>;

        public record Response(CarDto Car);
        public class Handler : IRequestHandler<Query, Response>
        {
            private IService<Car> _carService;
            private IMapper _mapper;

            public Handler(IService<Car> carService, IMapper mapper)
            {
                _carService = carService;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {

                return new Response(_mapper.Map<CarDto>(await _carService.GetByIdAsync(request.Id)));
            }
        }
    }
}
