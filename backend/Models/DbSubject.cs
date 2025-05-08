using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbSubject
{
    public const string TableName = "Subject";
    public Guid Id { get; set; }

    public string Name_Subject { get; set; }

    [NotMapped]
    public ICollection<DbMatchClient> MatchClients { get; set; }
    [NotMapped]
    public ICollection<DbMatchPerformer> MatchPerformers { get; set; }
}


public class DbItemsConfiguration : IEntityTypeConfiguration<DbSubject>
{
    public void Configure(EntityTypeBuilder<DbSubject> builder)
    {
        builder.
            ToTable(DbSubject.TableName);

        builder.HasKey(s => s.Id);

        //с сопоставлением заявок
        builder
            .HasMany(s => s.MatchClients)
            .WithOne(mc => mc.Subject)
            .OnDelete(DeleteBehavior.NoAction);

        //с сопоставлением анкет
        builder
            .HasMany(s => s.MatchPerformers)
            .WithOne(mp => mp.Subject)
            .OnDelete(DeleteBehavior.NoAction);

    }
}







