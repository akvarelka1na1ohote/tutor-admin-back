using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbMatchPerformer
{
    public const string TableName = "MatchPerformer";

    public Guid Id { get; set; }
   

    public Guid PerformerId { get; set; }
    public DbPerformer Performer { get; set; }


    public Guid SubjectId { get; set; }
    public DbSubject Subject { get; set; }

}

public class DbMatchPerformerConfiguration : IEntityTypeConfiguration<DbMatchPerformer>
{
    public void Configure(EntityTypeBuilder<DbMatchPerformer> builder)
    {
        builder.
            ToTable(DbMatchPerformer.TableName);

        builder.HasKey(mp => mp.Id);

        //с предметом
        builder
            .HasOne(mp => mp.Subject)
            .WithMany(s => s.MatchPerformers)
            .OnDelete(DeleteBehavior.Cascade);

        //с анкетой
        builder
            .HasOne(mp => mp.Performer)
            .WithMany(p => p.MatchPerformers)
            //.HasForeignKey(z => z.Id_Zayavka)
            .OnDelete(DeleteBehavior.Cascade);
    }
}







