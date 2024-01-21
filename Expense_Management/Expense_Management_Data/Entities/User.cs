using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Users", Schema = "dbo")]
public class User
{
    public int UserNumber { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
    public int Role { get; set; } 
    public string Email { get; set; }
    public string UserFirstName { get; set; } 
    public string UserLastName { get; set; } 
    public string IBAN { get; set; } 
    public decimal Balance { get; set; } 
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public int Status { get; set; }
    public string HireDate { get; set; } 
    public string TerminationDate { get; set; } 
    public bool IsActive { get; set; } 

    public virtual List<Expense> Expenses { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserNumber);
        builder.Property(x => x.UserNumber).ValueGeneratedNever();
        builder.Property(x => x.Username).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Role).IsRequired(true).HasDefaultValue(1);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.UserFirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UserLastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Balance).IsRequired(true);
        builder.Property(x => x.LastActivityDate).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        builder.Property(x => x.PasswordRetryCount).IsRequired(true).HasDefaultValue(0m);
        builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(1);
        builder.Property(x => x.HireDate).IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow.ToString()); 
        builder.Property(x => x.TerminationDate).IsRequired(true)
            .HasDefaultValue("-"); 
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(x => x.Role);
        builder.HasIndex(x => x.Email).IsUnique(true);
        builder.HasIndex(x => x.Username).IsUnique(true);
        builder.HasIndex(x => x.UserNumber).IsUnique(true);

        builder.HasMany(x => x.Expenses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserID);

        builder.HasData(new User()
            {
                UserNumber = 1,
                Username = "yavuz55",
                Password = "59b60b2d79d04ad1795c2afede5472b4", //yavuz
                Role = 2,
                Email = "Ragozza@com",
                IBAN = "TR123456789012345678901234",
                Balance = 0,
                UserFirstName = "yavuz",
                UserLastName = "ozdemir",
            },
            new User()
            {
                UserNumber = 2,
                Username = "yunus55",
                Password = "59b60b2d79d04ad1795c2afede5472b4", //yavuz
                Role = 2,
                Email = "yunus@com",
                IBAN = "TR123456789012345678901234",
                Balance = 0,
                UserFirstName = "yunus",
                UserLastName = "ozdemir",
            }
        );
    }
}