using backend.DataAccess;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Repositories;


public class SubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Вернуть все предметы
    public IEnumerable<DbSubject> GetAll()
    {
        return _context.Set<DbSubject>().AsEnumerable();
    }

    // Найти предмет по идентификатору
    public DbSubject GetById(Guid id)
    {
        return _context.Set<DbSubject>().FirstOrDefault(x => x.Id == id);
    }

    // Добавить новый предмет
    public void Add(DbSubject subject)
    {
        _context.Add(subject);
    }

    // Обновить предмет
    public void Update(DbSubject subject)
    {
        _context.Entry(subject).State = EntityState.Modified;
    }

    // Удалить предмет
    public void Delete(DbSubject subject)
    {
        _context.Remove(subject);
    }

    // Фиксируем изменения в базе данных
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}