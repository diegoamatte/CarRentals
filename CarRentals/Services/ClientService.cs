using CarRentals.Models;
using CarRentals.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services
{
    public class ClientService : IService<Client>
    {
        private IRepository<Client> _clientRepository;

        public ClientService(IRepository<Client> repository)
        {
            _clientRepository = repository;
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
           return await _clientRepository.GetAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _clientRepository.GetByIDAsync(id);
        }

        public async Task<Client> SaveAsync(Client client)
        {
            client.Id = Guid.NewGuid();
            await _clientRepository.SaveAsync(client);
            return client;
        }

        public async Task UpdateAsync(Guid id, Client client)
        {
            if (id != client.Id)
                throw new ArgumentException(nameof(client));
            try
            {
                await _clientRepository.UpdateAsync(client, id);   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
