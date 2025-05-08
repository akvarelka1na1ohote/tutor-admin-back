using backend.Models;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class MatchClientRepository
{
    // Приватное поле для хранения контекста EF Core
    private readonly ApplicationDbContext _context;

    // Конструктор, принимающий экземпляр контекста базы данных
    public MatchClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Метод для извлечения ВСЕХ записей из таблицы
    public async Task<List<DbMatchClient>> GetAllAsync()
    {
        // Используем метод ToListAsync(), возвращающий все объекты типа DbMatchClient
        return await _context.MatchClients.ToListAsync();
    }

    // Метод для поиска одной записи по ID
    public async Task<DbMatchClient> FindByIdAsync(Guid id)
    {
        // Найти конкретный объект с указанным ID
        return await _context.MatchClients.FindAsync(id);
    }

    // Метод добавления нового объекта в базу данных
    public async Task AddAsync(DbMatchClient client)
    {
        // Добавляем новый объект и сохраняем изменения в базе
        await _context.MatchClients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    // Метод обновления существующего объекта
    public async Task UpdateAsync(DbMatchClient client)
    {
        // Применяем изменение к экземпляру объекта и фиксируем изменения
        _context.MatchClients.Update(client);
        await _context.SaveChangesAsync();
    }

    // Метод удаления объекта по его ID
    public async Task DeleteAsync(Guid id)
    {
        // Сначала находим объект, потом удаляем его и сохраняем изменения
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            _context.MatchClients.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}