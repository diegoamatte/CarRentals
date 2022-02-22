using CarRentals.Controllers;
using CarRentals.Models;
using CarRentals.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarRentalsTests
{
    public class CarControllerTests
    {
        private readonly Mock<ICarService> _carService;
        private readonly CarsController _carController;
        private readonly Guid _validId;
        private readonly Car _testCar; 

        public static IEnumerable<object[]> InvalidId =>
            new List<object[]>
            {
                new object[]{ Guid.NewGuid() },
                new object[]{ Guid.Empty },
            };

        public CarControllerTests()
        {
            _validId = Guid.NewGuid();
            _testCar = new Car { Id = _validId, Brand = "Brand1", LicensePlate = "AAA-555", Model = "Model1", Type = "SUV" };

            _carService = new Mock<ICarService>();
            _carController = new CarsController(_carService.Object);

            //Arrange
            _carService.Setup(cs => cs.GetCars()).Returns(GetCarListAsync());
            _carService.Setup(cs => cs.GetCarById(_validId)).Returns(
                Task.FromResult(_testCar));
        }

        [Fact]
        public void GetCars_CallsMethodGetFromService()
        {
            //Act
            _ = _carController.GetCars();
            //Assert
            _carService.Verify(cs => cs.GetCars(), Times.Once);
        }

        [Fact]
        public async void GetCars_RetrievesCarLists()
        {
            //Act
            var actionResult = await _carController.GetCars();
            var result = (OkObjectResult)actionResult.Result;
            var cars = (IEnumerable<Car>)result.Value;
            var carList = GetCarList();
            //Assert
            Assert.True(cars.SequenceEqual(carList));
        }

        [Theory]
        [MemberData(nameof(InvalidId))]
        public async void GetCar_ReturnNotFoundStatus_WhenIdIsNotValid(Guid id)
        {
            //Act
            var result = await _carController.GetCar(id);
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetCar_ReturnsOkStatus_WhenIdIsValid()
        {
            //Act
            var result = await _carController.GetCar(_validId);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        [Fact]
        public async void GetCar_ReturnsCarWithSameId_WhenIdIsValid()
        {
            //Act
            var actionResult =  await _carController.GetCar(_validId);
            var objectResult = actionResult.Result as OkObjectResult;
            var car = objectResult.Value as Car;

            //Assert
            Assert.Equal(_testCar, car);
        }

        [Fact]
        public void PutCar_ReturnsBadRequest_WhenArgumentExceptionIsCatched()
        {
            //Arrange
            var guid = Guid.NewGuid();
            _carService.Setup(cs => cs.UpdateCar(guid, new Car()))
                .Throws<ArgumentException>();
            //Act
            var result = _carController.PutCar(guid, new Car());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void PutCar_CallsCarUpdate()
        {
            //Act
            _ = _carController.PutCar(_validId, _testCar);

            //Assert
            _carService.Verify(cs => cs.UpdateCar(_validId, _testCar), Times.Once);
        }

        [Fact]
        public void PutCar_ReturnsNoContent_WhenSuccess()
        {
            //Act
            var result = _carController.PutCar(_validId, _testCar);

            //Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void DeleteCar_CallsServiceDeleteOnce()
        {
            //Act
            _ = _carController.DeleteCar(_validId);

            //Assert
            _carService.Verify(cs => cs.DeleteCar(_validId), Times.Once);
        }

        [Fact]
        public void DeleteCar_ReturnsNotFound_WhenArgumentExceptionIsCatched()
        {
            //Arrange
            var invalidId = Guid.NewGuid();
            _carService.Setup(cs =>cs.DeleteCar(invalidId)).Throws(new ArgumentException());

            //Act
            var actionresult = _carController.DeleteCar(invalidId);

            //Assert
            Assert.IsType<NotFoundResult>(actionresult.Result);
        }

        [Fact]
        public void PostCar_CallsSaveCar()
        {
            //Act
            _ = _carController.PostCar(_testCar);

            //Assert
            _carService.Verify(cs => cs.SaveCar(_testCar), Times.Once);
        }

        private Task<IEnumerable<Car>> GetCarListAsync()
        {
            return Task.FromResult(GetCarList());
        }

        private IEnumerable<Car> GetCarList()
        {
            return new List<Car>
            {
                _testCar,
                new Car { Id = Guid.NewGuid(), Brand = "Brand2", LicensePlate = "BBB-666", Model = "Model2", Type = "SEDAN"},
                new Car { Id = Guid.NewGuid(), Brand = "Brand3", LicensePlate = "CCC-777", Model = "Model3", Type = "MINIVAN"},
                new Car { Id = Guid.NewGuid(), Brand = "Brand4", LicensePlate = "DDD-888", Model = "Model4", Type = "COUPE"},
            };
        }
    }
}
