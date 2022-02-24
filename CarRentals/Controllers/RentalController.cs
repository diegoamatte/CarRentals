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

        [HttpGet]
        public async Task<IActionResult> GetRentalsAsync()
        {
            return Ok(await _rentalService.GetAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostRentalAsync(Rental rental)
        {
            await _rentalService.SaveAsync(rental);
            return CreatedAtAction("GetRentalById", new { Id = rental.Id }, rental);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalByIdAsync(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null)
                return NotFound();
            return Ok(rental);
        }
        
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
