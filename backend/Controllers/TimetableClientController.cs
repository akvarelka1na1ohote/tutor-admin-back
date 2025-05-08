using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/timetables-clients")]
public class TimetablesClientsController : ControllerBase
{
    private readonly TimetableClientRepository _timetableClientRepo;

    public TimetablesClientsController(TimetableClientRepository timetableClientRepo)
    {
        _timetableClientRepo = timetableClientRepo;
    }

    // GET api/timetables-clients
    [HttpGet]
    public IActionResult GetAllTimetables()
    {
        var timetables = _timetableClientRepo.GetAll().ToList();
        return Ok(timetables);
    }

    // GET api/timetables-clients/{id}
    [HttpGet("{id}")]
    public IActionResult GetTimetableById(Guid id)
    {
        var timetable = _timetableClientRepo.GetById(id);
        if (timetable == null)
        {
            return NotFound($"Расписание с указанным идентификатором ({id}) не найдено.");
        }
        return Ok(timetable);
    }

    // POST api/timetables-clients
    [HttpPost]
    public IActionResult CreateTimetable([FromBody] DbTimetableClient timetable)
    {
        if (timetable == null || !ModelState.IsValid)
        {
            return BadRequest("Неверные данные");
        }

        _timetableClientRepo.Create(timetable);
        _timetableClientRepo.SaveChanges();
        return CreatedAtAction(nameof(GetTimetableById), new { id = timetable.Id }, timetable);
    }

    // PUT api/timetables-clients/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTimetable(Guid id, [FromBody] DbTimetableClient timetable)
    {
        if (timetable == null || timetable.Id != id)
        {
            return BadRequest("Идентификатор должен совпадать!");
        }

        var existingTimetable = _timetableClientRepo.GetById(id);
        if (existingTimetable == null)
        {
            return NotFound($"Расписание с данным идентификатором ({id}) не существует.");
        }

        _timetableClientRepo.Update(timetable);
        _timetableClientRepo.SaveChanges();
        return NoContent();
    }

    // DELETE api/timetables-clients/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTimetable(Guid id)
    {
        var timetable = _timetableClientRepo.GetById(id);
        if (timetable == null)
        {
            return NotFound($"Расписание с указанным идентификатором ({id}) не найдено.");
        }

        _timetableClientRepo.Delete(timetable);
        _timetableClientRepo.SaveChanges();
        return NoContent();
    }
}