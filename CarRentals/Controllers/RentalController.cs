using CarRentals.Models;
using CarRentals.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarRentals.Commands;
using CarRentals.DTOs;
using CarRentals.Queries;
using CarRentals.Exceptions;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of rentals.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(typeof(IEnumerable<RentalDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetRentalsAsync()
        {
            var response = await _mediator.Send(new GetRentals.Query());
            return Ok(response.Rentals);
        }

        /// <summary>
        /// Adds a new rental.
        /// </summary>
        /// <param name="rental"></param>
        /// <response code="201">Successfully created rental.</response>
        /// <response code="400">Validation problem.</response>
        [HttpPost]
        public async Task<IActionResult> PostRentalAsync(RentalDto rental)
        {
            var id = await _mediator.Send(new AddRental.Command(rental));
            var notification = new SendMessageNotification.SendMessage(rental);
            await _mediator.Publish(notification);
            return CreatedAtAction("GetRentalById", new { Id = id }, rental);
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
            var response = await _mediator.Send(new GetRentalById.Query(id));
            var rental = response.Rental;
            if (rental == null)
                return NotFound();
            return Ok(rental);
        }

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rental"></param>
        /// <response code="204">Successful operation.</response>
        /// <response code="400">Validation error or malformed request.</response>
        /// <response code="404">Rental not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<IActionResult> PutRentalAsync(Guid id, RentalDto rental)
        {
            try
            {
                _ = await _mediator.Send(new UpdateRental.Command(id, rental));
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
                _ = await _mediator.Send(new DeleteRental.Command(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
