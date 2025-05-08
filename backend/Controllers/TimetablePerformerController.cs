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
    public class TimetablesPerformersController : ControllerBase
    {
        private readonly TimetablePerformerRepository _repository;
        private readonly ILogger<TimetablesPerformersController> _logger;

        public TimetablesPerformersController(
            TimetablePerformerRepository repository,
            ILogger<TimetablesPerformersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbTimetablePerformer>>> GetAll()
        {
            try
            {
                var timetables = await _repository.GetAllAsync();
                return Ok(timetables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all timetables");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DbTimetablePerformer>> GetById(Guid id)
        {
            try
            {
                var timetable = await _repository.GetByIdAsync(id);
                if (timetable == null)
                {
                    _logger.LogWarning("Timetable with ID {TimetableId} not found", id);
                    return NotFound();
                }
                return Ok(timetable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting timetable with ID {TimetableId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DbTimetablePerformer>> Create([FromBody] DbTimetablePerformer timetable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid timetable data");
                    return BadRequest(ModelState);
                }

                timetable.Id = Guid.NewGuid();
                await _repository.AddAsync(timetable);

                _logger.LogInformation("Created new timetable with ID {TimetableId}", timetable.Id);
                return CreatedAtAction(nameof(GetById), new { id = timetable.Id }, timetable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating timetable");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DbTimetablePerformer timetable)
        {
            try
            {
                if (id != timetable.Id)
                {
                    _logger.LogWarning("ID mismatch in timetable update");
                    return BadRequest("ID mismatch");
                }

                await _repository.UpdateAsync(timetable);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating timetable with ID {TimetableId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Timetable with ID {TimetableId} not found for deletion", id);
                    return NotFound();
                }

                _logger.LogInformation("Deleted timetable with ID {TimetableId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting timetable with ID {TimetableId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}