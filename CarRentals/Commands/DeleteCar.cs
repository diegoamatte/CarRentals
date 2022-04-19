using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class DeleteCar
    {
        public record Command(Guid Id) : IRequest;

        public class Handler : IRequestHandler<Command, Unit>
        {
            private IService<Car> _carService;

            public Handler(IService<Car> carService)
            {
                _carService = carService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _carService.DeleteAsync(request.Id);
                return Unit.Value;
            }
        }
    }
}
