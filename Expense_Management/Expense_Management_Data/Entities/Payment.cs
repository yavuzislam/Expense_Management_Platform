using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Payments", Schema = "dbo")]
public class Payment
{
    public int PaymentID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int UserID { get; set; } 
    public int Status { get; set; } 
    public string Description { get; set; } 
    public int EFTDetailID { get; set; } 
    public bool IsActive { get; set; }

    public virtual User User { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentID);
        builder.Property(p => p.PaymentID).ValueGeneratedOnAdd();
        builder.Property(p => p.Amount).IsRequired(true).HasPrecision(18, 2);
        builder.Property(p => p.Date).IsRequired(true);
        builder.Property(p => p.UserID).IsRequired(true);
        builder.Property(p => p.Status).IsRequired(true);
        builder.Property(p => p.Description).IsRequired(false).HasMaxLength(500);
        builder.Property(p => p.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(p => p.UserID);
        builder.HasIndex(p => p.PaymentID).IsUnique(true);
    }
}