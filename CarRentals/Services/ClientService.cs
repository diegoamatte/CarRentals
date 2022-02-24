using CarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services
{
    public class ClientService : IService<Client>
    {
        private readonly CarRentalDbContext _context;
        public ClientService(CarRentalDbContext context)
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

        public async Task<Client> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            return await _context.Clients.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> SaveAsync(Client client)
        {
            client.Id = Guid.NewGuid();
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateAsync(Guid id, Client client)
        {
            if (!ClientExists(id))
                throw new ArgumentException(nameof(id));
            if (id != client.Id)
                throw new ArgumentException(nameof(client));

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
            return client;
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
