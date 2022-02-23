using CarRentals.Models;

namespace CarRentals.Services
{
    public interface IService<T>
    {
        Task DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> SaveAsync(T t);
        Task<T> UpdateAsync(Guid id, T t);
    }
}