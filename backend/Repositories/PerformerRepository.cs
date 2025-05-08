using System.Linq;
using backend.Models;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;


// Пространство имен для репозиториев приложения
namespace backend.Repositories;

// Реализация конкретного репозитория для сущностей типа DbPerformer
public class PerformerRepository 
{
    // Контекст данных приложения (подключение к БД)
    private readonly ApplicationDbContext _context;

    // Конструктор репозитория принимает контекст базы данных
    public PerformerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Получение списка всех исполнителей
    public IEnumerable<DbPerformer> GetAll()
    {
        // Возвращаем список всех объектов DbPerformer, преобразуя набор в перечисляемый тип
        return _context.Set<DbPerformer>().AsEnumerable();
    }

    // Поиск исполнителя по ID
    public DbPerformer GetById(Guid id)
    {
        // Используем FirstOrDefault(), чтобы вернуть первого найденного исполнителя с заданным ID,
        // либо null, если такого исполнителя нет
        return _context.Set<DbPerformer>().FirstOrDefault(x => x.Id == id);
    }

    // Добавление нового исполнителя
    public void Add(DbPerformer performer)
    {
        // Просто добавляем новый объект в контекст данных, метод SaveChanges позже зафиксирует изменение
        _context.Add(performer);
    }

    // Обновление существующих данных исполнителя
    public void Update(DbPerformer performer)
    {
        // Сообщаем контексту, что переданный экземпляр должен быть помечен как измененный (Modified),
        // чтобы потом сохранить изменения
        _context.Entry(performer).State = EntityState.Modified;
    }

    // Удаление исполнителя
    public void Delete(DbPerformer performer)
    {
        // Удаляем указанный экземпляр исполнителя из контекста данных
        _context.Remove(performer);
    }

    // Сохранение изменений в базе данных
    public void SaveChanges()
    {
        // Выполняем операцию сохранения всех внесенных изменений в базу данных
        _context.SaveChanges();
    }
}