using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class UpdateCar
    {
        public record UpdateCarCommand(Guid Id, Car Car) : IRequest;

        public class Handler : IRequestHandler<UpdateCarCommand, Unit>
        {
            private IService<Car> _carService;

            public Handler(IService<Car> carService) => _carService = carService;

            public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
                await _carService.UpdateAsync(request.Car.Id, request.Car);
                return Unit.Value;
            }
        }
    }
}
