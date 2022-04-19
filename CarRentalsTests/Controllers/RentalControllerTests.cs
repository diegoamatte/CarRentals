using CarRentals.DTOs;
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
    public class RentalControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;
        private readonly string _baseUrl;

        public RentalControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _baseUrl = "api/rental";
        }

        [Fact]
        public async void GetRental_ReturnsOK()
        {
            //Act
            var result = await _httpClient.GetAsync(_baseUrl);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void PostRental_ReturnsCreated_WhenRentalIsValid()
        {
            //Arrange
            var rental = await GetValidRentalAsync();

            //Act
            var result = await _httpClient.PostAsJsonAsync(_baseUrl, rental);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async void PostRental_ReturnsBadRequest_WhenRentalisInvalid()
        {
            //Arrange
            var rental = new RentalDto();

            //Act
            var result = await _httpClient.PostAsJsonAsync(_baseUrl, rental);
            
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void PutRental_ReturnsBadRequest_WhenRentalisInvalid()
        {
            // Arrange
            var rentals = await _httpClient.GetFromJsonAsync<List<RentalDto>>(_baseUrl);
            var rental = rentals.FirstOrDefault();
            rental.Client = null;
            var url = $"{_baseUrl}/{rental.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync(url, rental);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
        
        private async Task<RentalDto> GetValidRentalAsync()
        {
            var clients = await _httpClient.GetFromJsonAsync<IEnumerable<ClientDto>>("api/clients");
            var cars = await _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>("api/cars");

            return new RentalDto
            {
                Client = clients.First(),
                RentedCars = cars.Where(c=>c.State == CarRentals.Models.CarState.Available),
                RentStartTime = DateTime.Now,
                RentEndTime = DateTime.Now.AddDays(10),
            };
        }
    }
}
