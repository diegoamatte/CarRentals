using Microsoft.AspNetCore.Mvc;
using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentalDbContext _context;
        public CarService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> SaveCar(Car car)
        {
            car.Id = Guid.NewGuid();
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateCar(Guid id, Car car)
        {
            if(!CarExists(id))
                throw new ArgumentException(nameof(id));
            if (id != car.Id)
                throw new ArgumentException(nameof(car));

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                    throw new ArgumentException(nameof(id));
                throw;
            }
            return car;
        }

        public async Task DeleteCar(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                throw new ArgumentException();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

    }
}
