using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class TimetablePerformerRepository
    {
        private readonly ApplicationDbContext _context;

        public TimetablePerformerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DbTimetablePerformer>> GetAllAsync()
        {
            return await _context.TimetablePerformers.ToListAsync();
        }

        public async Task<DbTimetablePerformer> GetByIdAsync(Guid id)
        {
            return await _context.TimetablePerformers.FindAsync(id);
        }

        public async Task AddAsync(DbTimetablePerformer timetable)
        {
            await _context.TimetablePerformers.AddAsync(timetable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DbTimetablePerformer timetable)
        {
            _context.TimetablePerformers.Update(timetable);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var timetable = await _context.TimetablePerformers.FindAsync(id);
            if (timetable == null)
                return false;

            _context.TimetablePerformers.Remove(timetable);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}