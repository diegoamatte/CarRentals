using CarRentals.Models;

namespace CarRentals.Services
{
    public interface ICarService
    {
        Task DeleteCar(Guid id);
        Task<Car> GetCarById(Guid id);
        Task<IEnumerable<Car>> GetCars();
        Task<Car> SaveCar(Car car);
        Task<Car> UpdateCar(Guid id, Car car);
    }
}