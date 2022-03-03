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
    public class ClientsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "api/clients";
        private Client _validClient;
        public static IEnumerable<object[]> InvalidClients =>
            new List<object[]>
            {
                        new object[]{ new Client { Name = "", Surname = "", Address = "", DNI = 45444333 } },
                        new object[]{ new Client { Name = "", Surname = "Doe", Address = "Fake Street 122", DNI = 45444333 } },
                        new object[]{ new Client { Name = "John", Surname = "", Address = "Fake Street 122", DNI = 45444333 } },
                        new object[]{ new Client { Name = "John", Surname = "Doe", Address = "", DNI = 45444333 } },
                        new object[]{ new Client { Name = "John", Surname = "Doe", Address = "Fake Street 122", DNI = 145444333 } },
            };
        public ClientsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _validClient = new Client
            {
                Name = "John",
                Surname = "Doe",
                Address = "Fake Street 123",
                DNI = 34567888
            };
        }

        [Fact]
        public async void GetClients_ReturnsEmpty_WhenNoData()
        {
            //Arrange
            var expected = new List<Client>();

            //Act
            var result = await _httpClient.GetFromJsonAsync<List<Client>>(_baseUrl); 

            //Assert
            Assert.Equal(expected.Count, result.Count);
        }

        [Fact]
        public async void PostClients_SavesData_WhenClientIsValid()
        {
            //Act
            var clientSaved = await SaveClientAsync(_validClient);

            //Assert
            Assert.Equal(clientSaved.DNI, _validClient.DNI);
        }

        [Theory]
        [MemberData(nameof(InvalidClients))]
        public async void PostClients_ReturnsBadRequest_WhenClientIsInvalid(Client client)
        {
            //Act
            var result = await _httpClient.PostAsJsonAsync(_baseUrl, client);
            
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void PutClient_ReturnsNotFound_WhenClientNotExists()
        {
            //Act
            var result = await _httpClient.GetAsync($"{ _baseUrl }/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(InvalidClients))]
        public async void PutClient_ReturnsBadRequest_WhenClientIsInvalid(Client client)
        {
            //Arrange
            var clientSaved = await SaveClientAsync(_validClient);
            client.Id = clientSaved.Id;
            var url = $"{_baseUrl}/{clientSaved.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<Client>(url, client);
            
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void PutClient_ModifiesClient_WhenClientIsValid()
        {
            //Arrange
            var clientSaved = await SaveClientAsync(_validClient);
            _validClient.Id = clientSaved.Id;
            _validClient.Name = "SomeName";
            _validClient.Surname = "Surname";
            _validClient.Address = "SomeAddress";
            _validClient.DNI = 40000000;
            var url = $"{_baseUrl}/{clientSaved.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<Client>(url, _validClient);
            var updatedClient = await _httpClient.GetFromJsonAsync<Client>(url);

            //Assert
            Assert.Equal(_validClient, updatedClient);
        }

        [Fact]
        public async void PutClient_ReturnsNoContent_WhenClientIsValid()
        {
            //Arrange
            var clientSaved = await SaveClientAsync(_validClient);
            var url = $"{_baseUrl}/{clientSaved.Id}";

            //Act
            var result = await _httpClient.PutAsJsonAsync<Client>(url, _validClient);
            
            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async void DeleteClient_ReturnsNoContent_WhenClientIsValid()
        {
            //Arrange
            var clientSaved = await SaveClientAsync(_validClient);
            var url = $"{_baseUrl}/{clientSaved.Id}";

            //Act
            var result = await _httpClient.DeleteAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async void DeleteClient_ReturnsNotFound_WhenClientIsDeletedOrNotExists()
        {
            //Arrange
            var clientSaved = await SaveClientAsync(_validClient);
            var url = $"{_baseUrl}/{clientSaved.Id}";

            //Act
            _ = await _httpClient.DeleteAsync(url);
            var result = await _httpClient.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        private async Task<Client> SaveClientAsync(Client client)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseUrl, client);
            result.Headers.TryGetValues("location", out var location);
            var clientSaved = await _httpClient.GetFromJsonAsync<Client>(location.First());
            return clientSaved;
        }
    }
}
