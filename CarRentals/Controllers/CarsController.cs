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

        private readonly IService<Car> _carService;

        public CarsController(IService<Car> carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return Ok(await _carService.GetAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if(car == null)
                return NotFound();
            return Ok(car);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, Car car)
        {
            try
            {
                var updatedCar = _carService.UpdateAsync(id, car);
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
            await _carService.SaveAsync(car);
            return CreatedAtAction("GetCar", new { car.Id }, car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                await _carService.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
