using backend.DataAccess;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Repositories;

/// <summary>
/// Репозиторий для работы с расписаниями клиентов (таблица TimetableClient).
/// </summary>
public class TimetableClientRepository 
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Конструктор инициализирует контекст базы данных.
    /// </summary>
    /// <param name="context">Контекст подключения к базе данных.</param>
    public TimetableClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает коллекцию всех расписаний клиентов.
    /// </summary>
    public IEnumerable<DbTimetableClient> GetAll()
    {
        return _context.Set<DbTimetableClient>();
    }

    /// <summary>
    /// Возвращает расписание клиента по идентификатору клиента.
    /// </summary>
    public DbTimetableClient GetById(object id)
    {
        var clientId = (Guid)id;
        return _context.Set<DbTimetableClient>().SingleOrDefault(t => t.Id == clientId);
    }

    /// <summary>
    /// Создает новое расписание клиента в базе данных.
    /// </summary>
    public void Create(DbTimetableClient timetable)
    {
        _context.Add(timetable);
    }

    /// <summary>
    /// Обновляет существующее расписание клиента.
    /// </summary>
    public void Update(DbTimetableClient timetable)
    {
        _context.Update(timetable);
    }

    /// <summary>
    /// Удаляет расписание клиента из базы данных.
    /// </summary>
    public void Delete(DbTimetableClient timetable)
    {
        _context.Remove(timetable);
    }

    /// <summary>
    /// Применяет изменения в базе данных.
    /// </summary>
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}