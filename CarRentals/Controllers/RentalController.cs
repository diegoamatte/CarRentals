using CarRentals.Models;
using CarRentals.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IService<Rental> _rentalService;

        public RentalController(IService<Rental> rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Gets a list of rentals.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(typeof(IEnumerable<Rental>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetRentalsAsync()
        {
            return Ok(await _rentalService.GetAsync());
        }

        /// <summary>
        /// Adds a new rental.
        /// </summary>
        /// <param name="rental"></param>
        /// <response code="201">Successfully created rental.</response>
        /// <response code="400">Validation problem.</response>
        [HttpPost]
        public async Task<IActionResult> PostRentalAsync(Rental rental)
        {
            await _rentalService.SaveAsync(rental);
            return CreatedAtAction("GetRentalById", new { Id = rental.Id }, rental);
        }

        /// <summary>
        /// Finds a rental by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Successful operation.</response>
        /// <response code="404">Rental not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalByIdAsync(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null)
                return NotFound();
            return Ok(rental);
        }

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <response code="204">Successful operation.</response>
        /// <response code="400">Validation error or malformed request.</response>
        /// <response code="404">Rental not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<IActionResult> PutRentalAsync(Guid id, Rental rental)
        {
            try
            {
                _ = _rentalService.UpdateAsync(id, rental);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing rental.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Successfully deleted rental.</response>
        /// <response code="404">Rental not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalAsync(Guid id)
        {
            try
            {
                await _rentalService.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
