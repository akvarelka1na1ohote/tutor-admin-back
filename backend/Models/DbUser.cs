using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class DbUser
{
    public const string TableName = "User";
    public Guid Id { get; set; }
    [NotMapped]
    public ICollection<DbPerformer> Performers { get; set; }
    [NotMapped]
    public ICollection<DbClient> Clients { get; set; }
    
    public string Name_User { get; set; }

    
    public DateOnly Birth_User { get; set; }
   
    public bool Gender_User { get; set; }

    public string Phone_User { get; set; }

    public string Email_User { get; set; }


}

    public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.
                ToTable(DbUser.TableName);

            builder.HasKey(u => u.Id);

            //с анкетой
            builder
                .HasMany(u => u.Performers)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            //с заявкой
            builder
                .HasMany(u => u.Clients)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.Cascade);

    }
}







