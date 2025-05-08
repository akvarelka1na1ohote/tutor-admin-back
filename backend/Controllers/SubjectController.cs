using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectRepository _repository;

        public SubjectsController(SubjectRepository repository)
        {
            _repository = repository;
        }

        // GET api/subjects
        [HttpGet]
        public ActionResult<IEnumerable<DbSubject>> GetSubjects()
        {
            try
            {
                var subjects = _repository.GetAll().ToList();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при получении предметов: {ex.Message}");
            }
        }

        // GET api/subjects/{id}
        [HttpGet("{id}")]
        public ActionResult<DbSubject> GetSubject(Guid id)
        {
            try
            {
                var subject = _repository.GetById(id);

                if (subject == null)
                    return NotFound($"Предмет с ID={id} не найден");

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при поиске предмета: {ex.Message}");
            }
        }

        // POST api/subjects
        [HttpPost]
        public IActionResult PostSubject([FromBody] DbSubject subject)
        {
            try
            {
                _repository.Add(subject);
                _repository.SaveChanges();
                return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при создании предмета: {ex.Message}");
            }
        }

        // PUT api/subjects/{id}
        [HttpPut("{id}")]
        public IActionResult PutSubject(Guid id, [FromBody] DbSubject updatedSubject)
        {
            try
            {
                if (updatedSubject.Id != id)
                    return BadRequest("ID предмета не совпадает.");

                _repository.Update(updatedSubject);
                _repository.SaveChanges();
                return NoContent(); // 204 - успешное обновление без тела ответа
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при обновлении предмета: {ex.Message}");
            }
        }

        // DELETE api/subjects/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(Guid id)
        {
            try
            {
                var subject = _repository.GetById(id);

                if (subject == null)
                    return NotFound($"Предмет с ID={id} не найден");

                _repository.Delete(subject);
                _repository.SaveChanges();
                return NoContent(); // 204 - успешное удаление без тела ответа
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при удалении предмета: {ex.Message}");
            }
        }
    }
}