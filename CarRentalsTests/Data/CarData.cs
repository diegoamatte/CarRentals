using CarRentals.DTOs;
using CarRentals.Models;
using Xunit;

namespace CarRentalsTests.Data
{
    public class InvalidCarData : TheoryData<CarDto>
    {
        public InvalidCarData()
        {
            Add(new CarDto { Brand = "", LicensePlate = "", Model = "", State = CarState.Available, Type = "" });
            Add(new CarDto { Brand = "Renault", LicensePlate = "AAA-555", Model = "", State = CarState.Available, Type = "" });
            Add(new CarDto { Brand = "Renault", LicensePlate = "AAA-555", Model = "Sandero", State = CarState.Available, Type = "" });
        }
    }

    public class ValidCarData : TheoryData<CarDto>
    {
        public ValidCarData()
        {
            Add(new CarDto
            {
                Brand = "Volkswagen",
                LicensePlate = "AAA-555",
                Model = "Bora",
                State = CarState.Available,
                Type = "Sedan"
            });
        }
    }
}
