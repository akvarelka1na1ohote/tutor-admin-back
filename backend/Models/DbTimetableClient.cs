using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbTimetableClient
{
    public const string TableName = "TimetableClient";

    public Guid Id { get; set; }
    // Внешний ключ на таблицу DbClient
    //public Guid ClientId { get; set; }
    //// Свойство навигации на клиента
    public DbClient Client { get; set; }

    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public bool Arrangement { get; set; }
}

public class DbTimetableClientConfiguration : IEntityTypeConfiguration<DbTimetableClient>
{
    public void Configure(EntityTypeBuilder<DbTimetableClient> builder)
    {
        builder.
            ToTable(DbTimetableClient.TableName);

        builder.HasKey(tc => tc.Id);

        //builder
        //    .HasOne(tc => tc.Client)
        //    .WithOne(c => c.ClientToTime)
        //    //.HasForeignKey(b => b.Id_Zayavka)
        //    .OnDelete(DeleteBehavior.Cascade);

    }
}







