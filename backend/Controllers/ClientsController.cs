using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientRepository _repository;

        public ClientsController(ClientRepository repository)
        {
            _repository = repository;
        }

        // GET api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbClient>>> GetClients()
        {
            try
            {
                var clients = await _repository.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ошибка при получении клиентов: {ex.Message}");
            }
        }

        // GET api/clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DbClient>> GetClient(Guid id)
        {
            try
            {
                var client = await _repository.FindByIdAsync(id);

                if (client == null)
                    return NotFound($"Клиент с ID={id} не найден");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ошибка при поиске клиента: {ex.Message}");
            }
        }

        // POST api/clients
        [HttpPost]
        public async Task<ActionResult<DbClient>> PostClient([FromBody] DbClient client)
        {
            try
            {
                if (client == null)
                    return BadRequest("Данные клиента не могут быть пустыми");

                // Генерация нового ID, если не указан
                if (client.Id == Guid.Empty)
                    client.Id = Guid.NewGuid();

                await _repository.AddAsync(client);

                return CreatedAtAction(nameof(GetClient),
                    new { id = client.Id },
                    client);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ошибка при создании клиента: {ex.Message}");
            }
        }

        // PUT api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, [FromBody] DbClient client)
        {
            try
            {
                if (id != client.Id)
                    return BadRequest("ID клиента не совпадает");

                var existingClient = await _repository.FindByIdAsync(id);
                if (existingClient == null)
                    return NotFound($"Клиент с ID={id} не найден");

                await _repository.UpdateAsync(client);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ошибка при обновлении клиента: {ex.Message}");
            }
        }

        // DELETE api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                var client = await _repository.FindByIdAsync(id);
                if (client == null)
                    return NotFound($"Клиент с ID={id} не найден");

                await _repository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ошибка при удалении клиента: {ex.Message}");
            }
        }
    }
}