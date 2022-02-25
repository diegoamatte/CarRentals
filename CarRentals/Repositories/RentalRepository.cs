using CarRentals.Exceptions;
using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Repositories
{
    public class RentalRepository : IRepository<Rental>
    {
        private readonly CarRentalDbContext _context;

        public RentalRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(Guid id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                throw new ArgumentException();
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rental>> GetAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental> GetByIDAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Rentals.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental, Guid id)
        {
            if (!RentalExists(id))
                throw new ArgumentException(nameof(id));
            if (id != rental.Id)
                throw new ArgumentException(nameof(rental));

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                    throw new ArgumentException(nameof(id));
                throw;
            }
        }

        private bool RentalExists(Guid id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }

        private bool OverlapExists(Rental rental)
        {
            var sharedRentals = _context.Rentals
                .Where(r => r.RentedCars.Any(car => rental.RentedCars.Contains(car)));
            var overlap = sharedRentals.Any(shared =>
                shared.RentStartTime <= rental.RentEndTime ||
                shared.RentEndTime >= rental.RentStartTime);
            return overlap;
        }

        public IEnumerable<Car> RentedCarsOnInterval(DateTime start, DateTime end)
        {
            var rentalsOnInterval = _context.Rentals
                .Where(rental => rental.RentStartTime > end || rental.RentEndTime < start);
            var rentedCars = rentalsOnInterval.SelectMany(rental => rental.RentedCars).ToList();
            return rentedCars;
        }
    }
}
