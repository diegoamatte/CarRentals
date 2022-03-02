using CarRentals.Models;
using CarRentals.Validators;
using System;
using Xunit;

namespace CarRentalsTests.Validators
{
    public class CarValidatorTests
    {
        private Car _car;
        private readonly CarValidator _carValidator;

        public CarValidatorTests()
        {
            _car = new Car { 
                Brand = "Brand", 
                Id = Guid.NewGuid(), 
                Model = "ValidModel", 
                Type = "Valid type",
                LicensePlate = "AAA-555",
            };
            _carValidator = new CarValidator();
        }

        [Fact]
        public void Validator_IsnotValid_WhenFieldsAreEmpty()
        {
            //Arrange
            var car = new Car();

            //Act
            var result = _carValidator.Validate(car);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validator_IsNotValid_WhenLicensePlateHasNotMinimumLengthRequired()
        {
            //Arrange
            _car.LicensePlate = "AAAAA";

            //Act
            var result = _carValidator.Validate(_car);

            //Assert
            Assert.False(result.IsValid);
        }

        
    }
}
