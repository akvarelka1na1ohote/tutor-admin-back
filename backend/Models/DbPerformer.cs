using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbPerformer
{
    public const string TableName = "Performer";

    public Guid Id { get; set; }
    [NotMapped]
    public ICollection<DbMatchPerformer> MatchPerformers { get; set; }
    public Guid PerformerToTimeId { get; set; }
    public DbTimetablePerformer PerformerToTime { get; set; }




    public Guid UserId { get; set; }
    public DbUser User { get; set; }


    public int Id_Role { get; set; } //НАМ НУЖНО СДЕЛАТЬ ЭТО КЛЮЧОМ ТОЖЕ!!!!


    public string Education_User { get; set; }


    public int Course_User { get; set; }


    public string About_User { get; set; }

    public string Timetable_Anketa { get; set; }

    


}

public class DbPerformerConfiguration : IEntityTypeConfiguration<DbPerformer>
{
    public void Configure(EntityTypeBuilder<DbPerformer> builder)
    {
        builder.
            ToTable(DbPerformer.TableName);

        builder.HasKey(p => p.Id);
        builder.HasAlternateKey(p => new { p.UserId, p.Id_Role });

        //с пользователем
        builder
            .HasOne(p => p.User)
            .WithMany(u => u.Performers)
            //.HasForeignKey(b => b.Id_User) 
            .OnDelete(DeleteBehavior.Cascade);

        //с сопоставлением
        builder
            .HasMany(p => p.MatchPerformers)
            .WithOne(mp => mp.Performer)
            .OnDelete(DeleteBehavior.Cascade);

        ////с расписанием
        //builder
        //    .HasOne(p => p.PerformerToTime)
        //    .WithOne(tp => tp.TimeToPerformer)
        //    .OnDelete(DeleteBehavior.Cascade);

    }
}







