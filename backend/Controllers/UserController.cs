using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataAccess;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbUser>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Set<DbUser>().ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Поиск пользователя по идентификатору
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DbUser>> GetUserById(Guid id)
        {
            try
            {
                var user = await _context.Set<DbUser>().FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<DbUser>> CreateUser([FromBody] DbUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                user.Id = Guid.NewGuid();
                await _context.Set<DbUser>().AddAsync(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновление существующего пользователя
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] DbUser updatedUser)
        {
            try
            {
                if (id != updatedUser.Id)
                {
                    return BadRequest("ID mismatch");
                }

                _context.Entry(updatedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_context.Set<DbUser>().Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, $"Concurrency error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Set<DbUser>().FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Set<DbUser>().Remove(user);
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