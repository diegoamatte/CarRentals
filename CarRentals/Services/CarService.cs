using Microsoft.AspNetCore.Mvc;
using CarRentals.Models;
using Microsoft.EntityFrameworkCore;
using CarRentals.Repositories;

namespace CarRentals.Services
{
    public class CarService : IService<Car>
    {
        private IRepository<Car> _carRepository;

        public CarService(IRepository<Car> repository)
        {
            _carRepository = repository;
        }

        public async Task<IEnumerable<Car>> GetAsync()
        {
            return await _carRepository.GetAsync();
        }

        public async Task<Car> GetByIdAsync(Guid id)
        {
            return await _carRepository.GetByIDAsync(id);
        }

        public async Task<Car> SaveAsync(Car car)
        {
            car.Id = Guid.NewGuid();
            await _carRepository.SaveAsync(car);
            return car;
        }

        public async Task UpdateAsync(Guid id, Car car)
        {
            await _carRepository.UpdateAsync(car, id);
        }

        public async Task DeleteAsync(Guid id)
        {
            _carRepository.DeleteAsync(id);
        }
    }
}
