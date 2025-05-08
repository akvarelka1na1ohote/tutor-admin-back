using backend.Models;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class MatchPerformerRepository
{
    // Приватное поле для хранения контекста EF Core
    private readonly ApplicationDbContext _context;

    // Конструктор, принимающий экземпляр контекста базы данных
    public MatchPerformerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Метод для извлечения ВСЕХ записей из таблицы
    public async Task<List<DbMatchPerformer>> GetAllAsync()
    {
        // Используем метод ToListAsync(), возвращающий все объекты типа DbMatchPerformer
        return await _context.MatchPerformers.ToListAsync();
    }

    // Метод для поиска одной записи по ID
    public async Task<DbMatchPerformer> FindByIdAsync(Guid id)
    {
        // Найти конкретный объект с указанным ID
        return await _context.MatchPerformers.FindAsync(id);
    }

    // Метод добавления нового объекта в базу данных
    public async Task AddAsync(DbMatchPerformer performer)
    {
        // Добавляем новый объект и сохраняем изменения в базе
        await _context.MatchPerformers.AddAsync(performer);
        await _context.SaveChangesAsync();
    }

    // Метод обновления существующего объекта
    public async Task UpdateAsync(DbMatchPerformer performer)
    {
        // Применяем изменение к экземпляру объекта и фиксируем изменения
        _context.MatchPerformers.Update(performer);
        await _context.SaveChangesAsync();
    }

    // Метод удаления объекта по его ID
    public async Task DeleteAsync(Guid id)
    {
        // Сначала находим объект, потом удаляем его и сохраняем изменения
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            _context.MatchPerformers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}