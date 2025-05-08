using backend.DataAccess;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchPerformersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchPerformersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbMatchPerformer>>> GetAll()
        {
            try
            {
                var result = await _context.MatchPerformers
                    .Include(mp => mp.Performer)
                    .Include(mp => mp.Subject)
                    .ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DbMatchPerformer>> GetById(Guid id)
        {
            try
            {
                var result = await _context.MatchPerformers
                    .Include(mp => mp.Performer)
                    .Include(mp => mp.Subject)
                    .FirstOrDefaultAsync(mp => mp.Id == id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DbMatchPerformer>> Create([FromBody] DbMatchPerformer model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Model cannot be null");

                model.Id = Guid.NewGuid();
                _context.MatchPerformers.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DbMatchPerformer model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest("ID mismatch");

                var existingModel = await _context.MatchPerformers.FindAsync(id);
                if (existingModel == null)
                    return NotFound();

                _context.Entry(existingModel).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model = await _context.MatchPerformers.FindAsync(id);
                if (model == null)
                    return NotFound();

                _context.MatchPerformers.Remove(model);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}