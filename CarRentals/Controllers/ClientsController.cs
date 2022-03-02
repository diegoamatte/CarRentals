#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentals.Models;
using CarRentals.Services;

namespace CarRentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IService<Client> _clientService;

        public ClientsController(IService<Client> service)
        {
            _clientService = service;
        }

        /// <summary>
        /// Gets a list of clients.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            return Ok(await _clientService.GetAsync());
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
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
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
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            try
            {
                var updatedClient = _clientService.UpdateAsync(id, client);
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
        public async Task<IActionResult> PostClient(Client client)
        {
            await _clientService.SaveAsync(client);
            return CreatedAtAction("GetClient", new { client.Id }, client);
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
                await _clientService.DeleteAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
