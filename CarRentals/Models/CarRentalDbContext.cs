using Microsoft.EntityFrameworkCore;

namespace CarRentals.Models
{
    public class CarRentalDbContext : DbContext, ICarRentalDbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
