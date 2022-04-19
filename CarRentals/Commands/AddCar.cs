using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class AddCar
    {
        public record Command(CarDto Car) : IRequest<Guid>;

        public class Handler : IRequestHandler<Command, Guid>
        {
            private IService<Car> _carService;
            private IMapper _mapper;

            public Handler(IService<Car> carService, IMapper mapper)
            {
                _carService = carService;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var car =  await _carService.SaveAsync(_mapper.Map<Car>(request.Car));
                return car.Id;
            }
        }
    }
}
