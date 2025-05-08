using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbClient
{
    public const string TableName = "Client";

    public Guid UserId { get; set; }
    public DbUser User { get; set; } 

    public Guid Id { get; set; }

    [NotMapped]
    public ICollection<DbMatchClient> MatchClients { get; set; }
    public Guid ClientToTimeId { get; set; }
    public DbTimetableClient ClientToTime { get; set; }


    public bool Class_1 { get; set; }
    public bool Class_2 { get; set; }
    public bool Class_3 { get; set; }
    public bool Class_4 { get; set; }
    public bool Class_5 { get; set; }
    public bool Class_6 { get; set; }
    public bool Class_7 { get; set; }
    public bool Class_8 { get; set; }
    public bool Class_9 { get; set; }
    public bool Class_10 { get; set; }
    public bool Class_11 { get; set; }




    public bool Course_1 { get; set; }
    public bool Course_2 { get; set; }
    public bool Course_3 { get; set; }
    public bool Course_4 { get; set; }
    public bool Course_5 { get; set; }
    public bool Course_6 { get; set; }
    public bool Course_vypusknik { get; set; }
    public bool Course_not_important { get; set; }




    public string About_Pupil { get; set; }

    public int Сost_From { get; set; }

    public int Сost_To { get; set; }

    public int Age_From { get; set; }

    public int Age_To { get; set; }

    public string Timetable_Zayavka { get; set; }

    //место (учитель/уученмк/дистант)
    public bool U_Pupil { get; set; }

    public bool U_Tutor { get; set; }

    public bool Distant { get; set; }

    //Локация - город
    public bool SPb { get; set; }

    public bool Len { get; set; }

    public string Other { get; set; }


    //опыт
    public bool With_Expirience { get; set; }

    public bool Without_Expirience { get; set; }


}

public class DbClientConfiguration : IEntityTypeConfiguration<DbClient>
{
    public void Configure(EntityTypeBuilder<DbClient> builder)
    {
        builder.
            ToTable(DbClient.TableName);

        builder.HasKey(c => c.Id);

        //с пользователем
        builder
            .HasOne(c => c.User) 
            .WithMany(u => u.Clients) 
            //.HasForeignKey(b => b.Id_User) 
            .OnDelete(DeleteBehavior.Cascade); 

        //с сопоставлением
        builder
            .HasMany(c => c.MatchClients)
            .WithOne(mc => mc.Client)
            .OnDelete(DeleteBehavior.Cascade);

        //с расписанием
        //builder
        //    .HasOne(c => c.ClientToTime)
        //    .WithOne(tc => tc.Client)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}







