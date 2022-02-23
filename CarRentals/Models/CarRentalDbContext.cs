using Microsoft.EntityFrameworkCore;
using CarRentals.Models;

namespace CarRentals.Models
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Client { get; set; }
    }
}
