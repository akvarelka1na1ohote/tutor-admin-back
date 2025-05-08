using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbTimetablePerformer
{
    public const string TableName = "TimetablePerformer";

    public Guid Id { get; set; }
    // Внешний ключ на таблицу DbClient
    //public Guid TimeToPerformerId { get; set; }
    //// Свойство навигации на клиента
    public DbPerformer TimeToPerformer { get; set; }

    public bool Monday { get; set; }

    public bool Tuesday { get; set; }


    public bool Wednesday { get; set; }


    public bool Thursday { get; set; }

    public bool Friday { get; set; }

    public bool Saturday { get; set; }

    public bool Sunday { get; set; }

    public bool Arrangement { get; set; }



}

public class DbTimetablePerformerConfiguration : IEntityTypeConfiguration<DbTimetablePerformer>
{
    public void Configure(EntityTypeBuilder<DbTimetablePerformer> builder)
    {
        builder.
            ToTable(DbTimetablePerformer.TableName);

        builder.HasKey(tp => tp.Id);

        //builder
        //    .HasOne(tp => tp.TimeToPerformer)
        //    .WithOne(p => p.PerformerToTime)
        //    //.HasForeignKey(b => b.Id_Zayavka)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}







