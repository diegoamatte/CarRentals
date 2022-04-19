using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarRentalsTests.Validators
{
    public class RentalValidatorTests
    {
        [Fact]
        public void Validator_Fails_WhenOneOrMoreCarsAreDamaged()
        {
            //Arrange
            var validator = new RentalDtoValidator();
            var cars = GetCarList();
            cars.First().State = CarState.Damaged;
            var client = new ClientDto();
            var rental = new RentalDto
            {
                Client = client,
                RentedCars = cars,
                RentStartTime = DateTime.Now,
                RentEndTime = DateTime.Now.AddDays(1),
            };
            //Act
            var result = validator.Validate(rental);
            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Validator_Fails_WhenStartTimeIsGreaterThanEndTime()
        {
            //Arrange
            var validator = new RentalDtoValidator();
            var cars = GetCarList();
            var client = new ClientDto();
            var rental = new RentalDto
            {
                Client = client,
                RentedCars = cars,
                RentStartTime = DateTime.Now.AddDays(1),
                RentEndTime = DateTime.Now,
            };
            //Act
            var result = validator.Validate(rental);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validator_Fails_WhenCarsListIsEmpty()
        {
            //Arrange
            var validator = new RentalDtoValidator();
            var cars = new List<CarDto>();
            var client = new ClientDto();
            var rental = new RentalDto
            {
                Client = client,
                RentedCars = cars,
                RentStartTime = DateTime.Now.AddDays(1),
                RentEndTime = DateTime.Now,
            };
            //Act
            var result = validator.Validate(rental);
            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validator_Pass_WhenArgumentsAreCorrect()
        {
            var validator = new RentalDtoValidator();
            var cars = GetCarList();
            var client = new ClientDto();
            var rental = new RentalDto
            {
                Client = client,
                RentedCars = cars,
                RentStartTime = DateTime.Now,
                RentEndTime = DateTime.Now.AddDays(2),
            };
            //Act
            var result = validator.Validate(rental);
            //Assert
            Assert.True(result.IsValid);
        }

        private IEnumerable<CarDto> GetCarList()
        {
            return new List<CarDto>
            {
                new CarDto { Id = Guid.NewGuid(), Brand = "Brand2", LicensePlate = "BBB-666", Model = "Model2", Type = "SEDAN", State = CarState.Available },
                new CarDto { Id = Guid.NewGuid(), Brand = "Brand3", LicensePlate = "CCC-777", Model = "Model3", Type = "MINIVAN", State = CarState.Available },
                new CarDto { Id = Guid.NewGuid(), Brand = "Brand4", LicensePlate = "DDD-888", Model = "Model4", Type = "COUPE", State = CarState.Available },
            };
        }
    }
}
