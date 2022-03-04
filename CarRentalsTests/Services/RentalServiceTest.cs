using CarRentals.Models;
using CarRentals.Repositories;
using CarRentals.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CarRentals.Exceptions;

namespace CarRentalsTests.Services
{
    public class RentalServiceTest
    {
        private readonly Mock<IRepository<Rental>> _rentalRepository;
        private readonly RentalService _rentalService;

        public RentalServiceTest()
        {
            _rentalRepository = new Mock<IRepository<Rental>>();
            _rentalService = new RentalService(_rentalRepository.Object);
        }
        [Fact]
        public void SaveAsync_ThrowsException_WhenCarIsDamagedOrRented()
        {
            //Arrange
            var damagedCar = new Car { State = CarState.Damaged };
            var rental = new Rental { RentedCars = new List<Car> { damagedCar } };

            //Act & Assert
            Assert.ThrowsAsync<InvalidCarStateException>(() => _rentalService.SaveAsync(rental));
        }

        [Fact]
        public void SaveAsync_CallsSaveOnRepository_WithValidRental()
        {
            // Arrange
            var cars = new List<Car>();
            cars.Add(new Car { State = CarState.Available });
            var rental = new Rental { RentedCars = cars };

            // Act
            var result = _rentalService.SaveAsync(rental);

            // Assert
            _rentalRepository.Verify(repo => repo.SaveAsync(rental), Times.Once);
        }
    }
}
