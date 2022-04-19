using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class UpdateCar
    {
        public record Command(Guid Id, CarDto Car) : IRequest;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private IService<Car> _carService;
            private IMapper _mapper;

            public Handler(IService<Car> carService, IMapper mapper)
            {
                _carService = carService;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _carService.UpdateAsync(request.Id, _mapper.Map<Car>(request.Car));
                return Unit.Value;
            }
        }
    }
}
