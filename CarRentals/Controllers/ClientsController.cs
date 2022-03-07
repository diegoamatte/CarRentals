#nullable disable
using CarRentals.Commands;
using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Queries;
using CarRentals.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Gets a list of clients.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(typeof(IEnumerable<ClientDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            var response = await _sender.Send(new GetClients.Query());
            return Ok(response.Clients);
        }

        /// <summary>
        /// Finds a client by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Successful operation.</response>
        /// <response code="404">Client not found.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var response = await _sender.Send(new GetClientsById.Query(id));
            if (response.Client == null)
                return NotFound();
            return Ok(response.Client);
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <response code="204">Successful operation.</response>
        /// <response code="400">Validation error or malformed request.</response>
        /// <response code="404">Client not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, ClientDto client)
        {
            try
            {
                _ = _sender.Send(new UpdateClient.Command(id, client));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Adds a new client.
        /// </summary>
        /// <param name="client"></param>
        /// <response code="201">Successfully created client.</response>
        /// <response code="400">Validation problem.</response>
        [HttpPost]
        public async Task<IActionResult> PostClient(ClientDto client)
        {
            var id = await _sender.Send(new AddClient.Command(client));
            return CreatedAtAction("GetClient", new { id }, client);
        }

        /// <summary>
        /// Deletes an existing client.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Successfully deleted client.</response>
        /// <response code="404">Client not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                _ = await _sender.Send(new DeleteClient.Command(id));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
