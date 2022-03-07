using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public static class GetCars
    {
        public record Query() : IRequest<Response>;

        public record Response(IEnumerable<CarDto> Cars);

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
                return new Response(_mapper.Map<IEnumerable<CarDto>>(await _carService.GetAsync()));
            }
        }
    }
}
