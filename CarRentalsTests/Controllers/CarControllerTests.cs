using CarRentals.DTOs;
using CarRentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CarRentalsTests.Controllers
{
    public class CarControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "api/cars";
        private CarDto _validCar;
        public static IEnumerable<object[]> InvalidCars =>
            new List<object[]>
            {
                new object[] { new CarDto{ Brand = "", LicensePlate = "", Model = "", State = CarState.Available, Type = ""} },
                new object[] { new CarDto{ Brand = "Renault", LicensePlate = "AAA-555", Model = "", State = CarState.Available, Type = "" } },
                new object[] { new CarDto{ Brand = "Renault", LicensePlate = "AAA-555", Model = "Sandero", State = CarState.Available, Type = "" } },
            };

        public CarControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _validCar = new CarDto
            {
                Brand = "Volkswagen",
                LicensePlate = "AAA-555",
                Model = "Bora",
                State = CarState.Available,
                Type = "Sedan"
            };
        }

        [Fact]
        public async void GetCars_ReturnsOk()
        {
            //Act
            var result = await _httpClient.GetAsync(_baseUrl);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void PostCar_ReturnsCreated_WhenCarIsValid()
        {
            //Act
            var result = await _httpClient.PostAsJsonAsync<CarDto>(_baseUrl, _validCar);
            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void PostCar_CreatesCar_WhenCarIsValid()
        {
            //Act
            var result = await SaveCarAsync(_validCar);

            //Assert
            Assert.Equal(_validCar, result);
        }

        [Theory]
        [MemberData(nameof(InvalidCars))]
        public async void PostCar_ReturnsBadRequest_WhenCarIsInvalid(CarDto car)
        {
            //Act
            var result = await _httpClient.PostAsJsonAsync<CarDto>(_baseUrl, car);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void PutCar_ReturnsNotFound_WhenCarNotExists()
        {
            //Act
            var result = await _httpClient.GetAsync($"{ _baseUrl }/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(InvalidCars))]
        public async void PutCar_ReturnsBadRequest_WhenCarIsInvalid(CarDto car)
        {
            //Arrange
            var savedCar = await SaveCarAsync(_validCar);
            car.Id = savedCar.Id;
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<CarDto>(url, car);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void PutCar_ModifiesCar_WhenIsValid()
        {
            //Arrange
            var savedCar = await SaveCarAsync(_validCar);
            _validCar.Id = savedCar.Id;
            savedCar.Brand = "OtherBrand";
            savedCar.LicensePlate = "AA-767-AB";
            savedCar.State = CarState.Damaged;
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            await _httpClient.PutAsJsonAsync<CarDto>(url, savedCar);
            var updatedCar = await _httpClient.GetFromJsonAsync<CarDto>(url);

            //Assert
            Assert.Equal(savedCar, updatedCar);
        }

        [Fact]
        public async void PutCar_ReturnsNoContent_WhenCarIsValid()
        {
            //Arrange
            var savedCar = await SaveCarAsync(_validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<CarDto>(url, savedCar);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async void DeleteCar_ReturnsNoContent_WhenCarIsValid()
        {
            //Arrange
            var savedCar = await SaveCarAsync(_validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            var result = await _httpClient.DeleteAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async void DeleteCar_ReturnsNotFound_WhenCarIsDeletedOrNotExists()
        {
            //Arrange
            var savedCar = await SaveCarAsync(_validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            _ = await _httpClient.DeleteAsync(url);
            var result = await _httpClient.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        private async Task<CarDto> SaveCarAsync(CarDto car)
        {
            var result = await _httpClient.PostAsJsonAsync<CarDto>(_baseUrl, car);
            result.Headers.TryGetValues("location", out var location);
            var getLocation = location.First();
            return await _httpClient.GetFromJsonAsync<CarDto>(getLocation);
        }
    }
}
