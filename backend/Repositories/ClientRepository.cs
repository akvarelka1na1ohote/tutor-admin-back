using backend.Models;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class ClientRepository 
{
    // Приватное поле для хранения контекста EF Core
    private readonly ApplicationDbContext _context;

    // Конструктор, принимающий экземпляр контекста базы данных
    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Метод для извлечения ВСЕХ записей из таблицы
    public async Task<List<DbClient>> GetAllAsync()
    {
        // Использует метод ToListAsync(), возвращающий все объекты типа DbClient
        return await _context.Clients.Include(x => x.MatchClients).ThenInclude(x => x.Subject).ToListAsync();
    }



    // Метод для поиска одной записи по ID
    public async Task<DbClient> FindByIdAsync(Guid id)
    {
        // Найти конкретный объект с указанным ID
        return await _context.Clients.FindAsync(id);
    }

    // Метод добавления нового объекта в базу данных
    public async Task AddAsync(DbClient client)
    {
        // Добавляем новый объект и сохраняем изменения в базе
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    // Метод обновления существующего объекта
    public async Task UpdateAsync(DbClient client)
    {
        // Применяем изменение к экземпляру объекта и фиксируем изменения
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    // Метод удаления объекта по его ID
    public async Task DeleteAsync(Guid id)
    {
        // Сначала находим объект, потом удаляем его и сохраняем изменения
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}