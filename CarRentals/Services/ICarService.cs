using CarRentals.Models;

namespace CarRentals.Services
{
    public interface ICarService
    {
        Task DeleteCarAsync(Guid id);
        Task<Car> GetCarByIdAsync(Guid id);
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<Car> SaveCarAsync(Car car);
        Task<Car> UpdateCarAsync(Guid id, Car car);
    }
}