using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                throw new ArgumentException();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIDAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car, Guid id)
        {
            if (!CarExists(id))
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
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
