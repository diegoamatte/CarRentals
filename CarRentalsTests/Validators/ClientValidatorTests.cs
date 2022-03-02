using CarRentals.Models;
using CarRentals.Validators;
using System.Collections.Generic;
using Xunit;

namespace CarRentalsTests.Validators
{
    public class ClientValidatorTests
    {
        public static IEnumerable<object[]> InvalidClients =>
            new List<object[]>
            {
                new object[]{ new Client{ Address = "", DNI = 1, Name = "", Surname = ""} },            
                new object[]{ new Client{ DNI = 12000000, Name = "", Surname = ""}, },            
                new object[]{ new Client{ Address = "", DNI = 12000000, Surname = ""} },            
                new object[]{ new Client{ Address = "", DNI = 12000000, Name = ""} },            
                new object[]{ new Client{ Address = "", DNI = 100000000, Name = "", Surname = ""} },
            };

        [Theory]
        [MemberData(nameof(InvalidClients))]
        public void Validator_Fails_WhenClientIsNotValid(Client client)
        {
            // Arrange
            var validator = new ClientValidator();
            // Act
            var result = validator.Validate(client).IsValid;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validator_Pass_WhenClientIsValid()
        {
            // Arrange
            var client = new Client { Address = "San Martin 3456", DNI = 23456789, Name = "John", Surname = "Doe" };
            var validator = new ClientValidator();
            // Act
            var result = validator.Validate(client).IsValid;
            // Assert
            Assert.True(result);
        }


    }
}
