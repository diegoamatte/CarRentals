using CarRentals.DTOs;
using CarRentals.Validators;
using System;
using Xunit;

namespace CarRentalsTests.Validators
{
    public class CarDtoValidatorTests
    {
        private CarDto _validCar;
        private readonly CarDtoValidator _carValidator;

        public CarDtoValidatorTests()
        {
            _validCar = new CarDto { 
                Brand = "Brand", 
                Id = Guid.NewGuid(), 
                Model = "ValidModel", 
                Type = "Valid type",
                LicensePlate = "AAA-555",
            };
            _carValidator = new CarDtoValidator();
        }

        [Fact]
        public void Validator_IsnotValid_WhenFieldsAreEmpty()
        {
            //Arrange
            var car = new CarDto();

            //Act
            var result = _carValidator.Validate(car);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validator_IsNotValid_WhenLicensePlateIsInvalid()
        {
            //Arrange
            var car = new CarDto { 
                Brand = _validCar.Brand,
                Id = _validCar.Id,
                LicensePlate = "AAA",
                Model = _validCar.Model,
                State = _validCar.State,
                Type = _validCar.Type,
            };

            //Act
            var result = _carValidator.Validate(car);

            //Assert
            Assert.False(result.IsValid);
        }

        
    }
}
