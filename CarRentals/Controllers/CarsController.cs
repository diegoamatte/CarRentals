#nullable disable
using CarRentals.Models;
using CarRentals.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarRentals.Queries;
using CarRentals.Commands;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly ISender _sender;

        public CarsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Gets a list of cars.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(typeof(IEnumerable<Car>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _sender.Send(new GetCars.Query());
            return Ok(cars);
        }

        /// <summary>
        /// Finds a car by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Successful operation.</response>
        /// <response code="404">Car not found.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar([FromRoute] Guid id)
        {
            var response = await _sender.Send(new GetCarsById.Query(id));
            if(response.Car == null)
                return NotFound();
            return Ok(response.Car);
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <response code="204">Successful operation.</response>
        /// <response code="400">Validation error or malformed request.</response>
        /// <response code="404">Car not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar([FromRoute] Guid id,[FromBody] Car car)
        {
            if(id != car.Id)
                return BadRequest();
            try
            {
                await _sender.Send(new UpdateCar.UpdateCarCommand(id,car));
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
        /// <param name="command"></param>
        /// <response code="201">Successfully created car.</response>
        /// <response code="400">Validation problem.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            var command = new AddCar.AddCarCommand(car);
            var savedCar  = await _sender.Send(command);
            return CreatedAtAction("GetCar", new { car.Id }, car);
        }

        /// <summary>
        /// Deletes an existing car.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Successfully deleted car.</response>
        /// <response code="404">Car not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(DeleteCar.DeleteCarCommand command)
        {
            try
            {
                await _sender.Send(command);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
