using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Queries
{
    public static class GetCarsById
    {
        public record Query(Guid Id) : IRequest<Response>;

        public record Response(Car Car);
        public class Handler : IRequestHandler<Query, Response>
        {
            private IService<Car> _carService;

            public Handler(IService<Car> carService)
            {
                _carService = carService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {

                return new Response(await _carService.GetByIdAsync(request.Id));
            }
        }
    }
}
