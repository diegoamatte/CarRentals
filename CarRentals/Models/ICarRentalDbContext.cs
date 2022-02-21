using Microsoft.EntityFrameworkCore;

namespace CarRentals.Models
{
    public interface ICarRentalDbContext
    {
        DbSet<Car> Cars { get; set; }
    }
}