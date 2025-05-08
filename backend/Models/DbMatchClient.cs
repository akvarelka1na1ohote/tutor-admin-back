using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbMatchClient
{
    public const string TableName = "MatchClient";

    public Guid Id { get; set; }
    
    public Guid ClientId { get; set; }
    public DbClient Client { get; set; }

    public Guid SubjectId { get; set; }
    public DbSubject Subject { get; set; }

    
}

public class DbMatchClientConfiguration : IEntityTypeConfiguration<DbMatchClient>
{
    public void Configure(EntityTypeBuilder<DbMatchClient> builder)
    {
        builder.
            ToTable(DbMatchClient.TableName);

        builder.HasKey(mc => mc.Id);

        //с предметом
        builder
            .HasOne(mc => mc.Subject)
            .WithMany(s => s.MatchClients)
            .OnDelete(DeleteBehavior.Cascade);

        //с заявкой
        builder
            .HasOne(mc => mc.Client)
            .WithMany(c => c.MatchClients)
            //.HasForeignKey(z => z.Id_Zayavka)
            .OnDelete(DeleteBehavior.Cascade);

    }
}







