using CarRentals.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRentalsTests;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CarRentalDbContext>));

            if(descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<CarRentalDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CarRentalDbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                try
                {
                    db.Clients.AddRange(GetClients());
                    db.Cars.AddRange(GetCars());
                    db.Rentals.AddRange(GetRentals());
                    db.SaveChanges();
                    db.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    logger.LogError($"An error has ocurred: {ex.Message}");
                    throw;
                }
            }
        });
    }

    private IEnumerable<Car> GetCars()
    {
        return new List<Car>
        {
            new Car{ Brand = "FORD", Id = Guid.NewGuid(), LicensePlate = "ABC-123", Model = "Ka", State = CarState.Available, Type = "Sedan"},
            new Car{ Brand = "VW", Id = Guid.NewGuid(), LicensePlate = "ABC-124", Model = "Gol", State = CarState.Available, Type = "Sedan"},
            new Car{ Brand = "CHEVROLET", Id = Guid.NewGuid(), LicensePlate = "ABC-143", Model = "Corsa", State = CarState.Damaged, Type = "Sedan"},
        };
    }

    private IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client{ Id = Guid.NewGuid(), Name = "John", Address = "Fake Street 123", DNI = 12345789, Surname = "Doe"},
            new Client{ Id = Guid.NewGuid(), Name = "Jane", Address = "Fake Street 125", DNI = 90234569, Surname = "Doe Doe"},
        };
    }

    private IEnumerable<Rental> GetRentals()
    {
        var rentals = new List<Rental>
        {
            new Rental {
                Id = Guid.NewGuid(),
                Client = GetClients().First(),
                RentedCars = GetCars().Where(c => c.State == CarRentals.Models.CarState.Available).Take(1).ToList(),
                RentStartTime = DateTime.Now,
                RentEndTime = DateTime.Now.AddDays(10),
            },
            new Rental {
                Id= Guid.NewGuid(),
                Client = GetClients().ElementAt(1),
                RentedCars = GetCars().Where(c => c.State == CarRentals.Models.CarState.Available).Skip(1).ToList(),
                RentStartTime = DateTime.Now,
                RentEndTime = DateTime.Now.AddDays(10),
            },
        };

        return rentals;
    }
}