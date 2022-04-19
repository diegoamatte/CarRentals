using CarRentals.DTOs;
using CarRentals.Validators;
using System.Collections.Generic;
using Xunit;

namespace CarRentalsTests.Validators
{
    public class ClientDtoValidatorTests
    {
        public static IEnumerable<object[]> InvalidClients =>
            new List<object[]>
            {
                new object[]{ new ClientDto{ Address = "", DNI = 1, Name = "", Surname = ""} },            
                new object[]{ new ClientDto{ DNI = 12000000, Name = "", Surname = ""}, },            
                new object[]{ new ClientDto{ Address = "", DNI = 12000000, Surname = ""} },            
                new object[]{ new ClientDto{ Address = "", DNI = 12000000, Name = ""} },            
                new object[]{ new ClientDto{ Address = "", DNI = 100000000, Name = "", Surname = ""} },
            };

        [Theory]
        [MemberData(nameof(InvalidClients))]
        public void Validator_Fails_WhenClientIsNotValid(ClientDto client)
        {
            // Arrange
            var validator = new ClientDtoValidator();
            // Act
            var result = validator.Validate(client).IsValid;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validator_Pass_WhenClientIsValid()
        {
            // Arrange
            var client = new ClientDto { Address = "San Martin 3456", DNI = 23456789, Name = "John", Surname = "Doe" };
            var validator = new ClientDtoValidator();
            // Act
            var result = validator.Validate(client).IsValid;
            // Assert
            Assert.True(result);
        }


    }
}
