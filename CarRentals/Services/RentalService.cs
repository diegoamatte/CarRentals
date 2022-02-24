using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services
{
    public class RentalService : IService<Rental>
    {
        private readonly CarRentalDbContext _context;

        public RentalService(CarRentalDbContext context)
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

        public async Task<Rental> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Rentals.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rental> SaveAsync(Rental rental)
        {
            rental.Id = Guid.NewGuid();
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental> UpdateAsync(Guid id, Rental rental)
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
            return rental;
        }

        private bool RentalExists(Guid id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
