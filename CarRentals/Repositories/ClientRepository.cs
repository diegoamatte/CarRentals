using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly CarRentalDbContext _context;
        public ClientRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                throw new ArgumentException();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIDAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Clients.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client, Guid id)
        {
            if (!ClientExists(id))
                throw new ArgumentException(nameof(id));

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                    throw new ArgumentException(nameof(id));
                throw;
            }
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
