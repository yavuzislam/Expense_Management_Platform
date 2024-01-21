using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Expenses", Schema = "dbo")]
public class Expense
{
    public int ExpenseNumber { get; set; }
    public int UserID { get; set; }
    public int CategoryID { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Receipt { get; set; }
    public int Status { get; set; } 
    public string RejectionReason { get; set; } 
    public bool IsActive { get; set; }

    public virtual User User { get; set; }
    public virtual Category Category { get; set; }

}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.ExpenseNumber);
        builder.Property(x => x.ExpenseNumber).ValueGeneratedOnAdd();
        builder.Property(x => x.UserID).IsRequired(true);
        builder.Property(x => x.CategoryID).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.Date).IsRequired(true);
        builder.Property(x => x.Receipt).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(0);
        builder.Property(x => x.RejectionReason).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(x => x.UserID);
        builder.HasIndex(x => x.CategoryID);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ExpenseNumber).IsUnique(true);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(x => x.UserID);

        builder.HasOne(x => x.Category)
            .WithMany(c => c.Expenses)
            .HasForeignKey(x => x.CategoryID);
    }
}