using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Notifications", Schema = "dbo")]
public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } 
    public bool IsRead { get; set; } 
    public DateTime DateCreated { get; set; } 

    public virtual User User { get; set; }
}

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.NotificationId);
        builder.Property(n => n.NotificationId).ValueGeneratedOnAdd();
        builder.Property(n => n.UserId).IsRequired(true);
        builder.Property(n => n.Message).IsRequired(true).HasMaxLength(500);
        builder.Property(n => n.IsRead).IsRequired(true).HasDefaultValue(false);
        builder.Property(n => n.DateCreated).IsRequired(true).HasDefaultValue(DateTime.UtcNow);

        builder.HasIndex(n => n.IsRead);
        builder.HasIndex(n => n.UserId);
    }
}