using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchClientsController : ControllerBase
    {
        private readonly MatchClientRepository _repository;

        public MatchClientsController(MatchClientRepository repository)
        {
            _repository = repository;
        }

        // GET: api/MatchClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbMatchClient>>> GetAll()
        {
            try
            {
                var matchClients = await _repository.GetAllAsync();
                return Ok(matchClients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MatchClients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DbMatchClient>> GetById(Guid id)
        {
            try
            {
                var matchClient = await _repository.FindByIdAsync(id);

                if (matchClient == null)
                {
                    return NotFound();
                }

                return Ok(matchClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MatchClients
        [HttpPost]
        public async Task<ActionResult<DbMatchClient>> Create([FromBody] DbMatchClient matchClient)
        {
            try
            {
                if (matchClient == null)
                {
                    return BadRequest("MatchClient object is null");
                }

                await _repository.AddAsync(matchClient);

                return CreatedAtAction(nameof(GetById), new { id = matchClient.Id }, matchClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MatchClients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DbMatchClient matchClient)
        {
            try
            {
                if (id != matchClient.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingMatchClient = await _repository.FindByIdAsync(id);
                if (existingMatchClient == null)
                {
                    return NotFound();
                }

                await _repository.UpdateAsync(matchClient);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MatchClients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var matchClient = await _repository.FindByIdAsync(id);
                if (matchClient == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}