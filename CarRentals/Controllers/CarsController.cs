#nullable disable
using CarRentals.Models;
using CarRentals.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return Ok(await _carService.GetCars());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _carService.GetCarById(id);
            if(car == null)
                return NotFound();
            return Ok(car);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, Car car)
        {
            try
            {
                var updatedCar = _carService.UpdateCar(id, car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            await _carService.SaveCar(car);
            return CreatedAtAction("GetCar", new { car.Id }, car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                await _carService.DeleteCar(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
