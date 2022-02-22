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

        /// <summary>
        /// Gets a list of cars.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return Ok(await _carService.GetCars());
        }

        /// <summary>
        /// Finds a car by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Successful operation.</response>
        /// <response code="404">Car not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _carService.GetCarById(id);
            if(car == null)
                return NotFound();
            return Ok(car);
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="car"></param>
        /// <response code="204">Successful operation.</response>
        /// <response code="400">Validation or malformed request.</response>
        /// <response code="404">Car not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, Car car)
        {
            try
            {
                var updatedCar = await _carService.UpdateCar(id, car);
            }
            catch (Exception ex)
            {
                if ("id".Equals(ex.Message))
                    return NotFound();
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Adds a new car.
        /// </summary>
        /// <param name="car"></param>
        /// <response code="201">Succesfully created car.</response>
        /// <response code="400">Validation problem.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            await _carService.SaveCar(car);
            return CreatedAtAction("GetCar", new { car.Id }, car);
        }

        /// <summary>
        /// Deletes an existing car.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Successfully deleted car.</response>
        /// <response code="404">Car not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
