using backend.DataAccess;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<DbUser>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }


    public async Task<DbUser?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddUserAsync(DbUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateUserAsync(DbUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
