using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformersController : ControllerBase
    {
        private readonly PerformerRepository _repository;

        public PerformersController(PerformerRepository repository)
        {
            _repository = repository;
        }

        // GET api/performers
        [HttpGet]
        public ActionResult<IEnumerable<DbPerformer>> GetPerformers()
        {
            try
            {
                var performers = _repository.GetAll().ToList();
                return Ok(performers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при получении исполнителей: {ex.Message}");
            }
        }

        // GET api/performers/{id}
        [HttpGet("{id}")]
        public ActionResult<DbPerformer> GetPerformer(Guid id)
        {
            try
            {
                var performer = _repository.GetById(id);

                if (performer == null)
                    return NotFound($"Исполнитель с ID={id} не найден");

                return Ok(performer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при поиске исполнителя: {ex.Message}");
            }
        }

        // POST api/performers
        [HttpPost]
        public IActionResult PostPerformer([FromBody] DbPerformer performer)
        {
            try
            {
                _repository.Add(performer);
                _repository.SaveChanges();
                return CreatedAtAction(nameof(GetPerformer), new { id = performer.Id }, performer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при создании исполнителя: {ex.Message}");
            }
        }

        // PUT api/performers/{id}
        [HttpPut("{id}")]
        public IActionResult PutPerformer(Guid id, [FromBody] DbPerformer updatedPerformer)
        {
            try
            {
                if (updatedPerformer.Id != id)
                    return BadRequest("ID исполнителя не совпадает.");

                _repository.Update(updatedPerformer);
                _repository.SaveChanges();
                return NoContent(); // 204 - успешное обновление без тела ответа
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при обновлении исполнителя: {ex.Message}");
            }
        }

        // DELETE api/performers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePerformer(Guid id)
        {
            try
            {
                var performer = _repository.GetById(id);

                if (performer == null)
                    return NotFound($"Исполнитель с ID={id} не найден");

                _repository.Delete(performer);
                _repository.SaveChanges();
                return NoContent(); // 204 - успешное удаление без тела ответа
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при удалении исполнителя: {ex.Message}");
            }
        }
    }
}