using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class AddCar
    {
        public record AddCarCommand(Car car) : IRequest<Car>;

        public class Handler : IRequestHandler<AddCarCommand, Car>
        {
            private IService<Car> _carService;

            public Handler(IService<Car> carService)
            {
                _carService = carService;
            }

            public async Task<Car> Handle(AddCarCommand request, CancellationToken cancellationToken)
            {
                return await _carService.SaveAsync(request.car);
            }
        }
    }
}
