using CarRentals.Exceptions;
using CarRentals.Models;
using CarRentals.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services
{
    public class RentalService : IService<Rental>
    {
        private IRepository<Rental> _rentalRepository;

        public RentalService(IRepository<Rental> repository)
        {
            _rentalRepository = repository;
        }
        public async Task DeleteAsync(Guid id)
        {
            _rentalRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Rental>> GetAsync()
        {
            return await _rentalRepository.GetAsync();
        }

        public async Task<Rental> GetByIdAsync(Guid id)
        {
            return await _rentalRepository.GetByIDAsync(id);
        }

        public async Task<Rental> SaveAsync(Rental rental)
        {

            foreach(var car in rental.RentedCars)
            {
                if (car.State is CarState.Damaged)
                    throw new InvalidCarStateException("Car is damaged, cannot be rent.");
            }
            rental.Id = Guid.NewGuid();
            await _rentalRepository.SaveAsync(rental);
            return rental;
        }

        public async Task UpdateAsync(Guid id, Rental rental)
        {
            await _rentalRepository.UpdateAsync(rental, id);
        }
        
    }
}
