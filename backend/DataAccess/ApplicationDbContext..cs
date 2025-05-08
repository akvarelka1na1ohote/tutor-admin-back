using backend.Models;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbClient> Clients { get; set; }
    public DbSet<DbMatchPerformer> MatchPerformers { get; set; }
    public DbSet<DbMatchClient> MatchClients { get; set; }
    public DbSet<DbPerformer> Performers { get; set; }
    public DbSet<DbSubject> Subjects { get; set; }
    public DbSet<DbTimetableClient> TimetableClients { get; set; }
    public DbSet<DbTimetablePerformer> TimetablePerformers { get; set; }
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));

        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
       .EnableSensitiveDataLogging();
    }

}