using CarRentals.DTOs;
using CarRentals.Models;
using CarRentalsTests.Data;
using System;
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

        public CarControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async void GetCars_ReturnsOk()
        {
            //Act
            var result = await _httpClient.GetAsync(_baseUrl);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void PostCar_ReturnsCreated_WhenCarIsValid(CarDto validCar)
        {
            //Act
            var result = await _httpClient.PostAsJsonAsync<CarDto>(_baseUrl, validCar);
            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void PostCar_CreatesCar_WhenCarIsValid(CarDto validCar)
        {
            //Act
            var result = await SaveCarAsync(validCar);

            //Assert
            Assert.Equal(validCar, result);
        }

        [Theory]
        [ClassData(typeof(InvalidCarData))]
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
        [ClassData(typeof(ValidCarData))]
        public async void PutCar_ReturnsBadRequest_WhenCarIsInvalid(CarDto validCar)
        {
            //Arrange
            var savedCar = await SaveCarAsync(validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";
            savedCar.Brand = "";

            //Act
            var result = await _httpClient.PutAsJsonAsync<CarDto>(url, savedCar);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void PutCar_ModifiesCar_WhenIsValid(CarDto validCar)
        {
            //Arrange
            var savedCar = await SaveCarAsync(validCar);
            validCar.Id = savedCar.Id;
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

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void PutCar_ReturnsNoContent_WhenCarIsValid(CarDto validCar)
        {
            //Arrange
            var savedCar = await SaveCarAsync(validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<CarDto>(url, savedCar);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void DeleteCar_ReturnsNoContent_WhenCarIsValid(CarDto validCar)
        {
            //Arrange
            var savedCar = await SaveCarAsync(validCar);
            var url = $"{_baseUrl}/{savedCar.Id}";

            //Act
            var result = await _httpClient.DeleteAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ValidCarData))]
        public async void DeleteCar_ReturnsNotFound_WhenCarIsDeletedOrNotExists(CarDto validCar)
        {
            //Arrange
            var savedCar = await SaveCarAsync(validCar);
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
