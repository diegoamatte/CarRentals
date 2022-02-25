namespace CarRentals.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIDAsync(Guid id);
        Task SaveAsync(T t);
        Task UpdateAsync(T t, Guid id);
        Task DeleteAsync(Guid id);
    }
}