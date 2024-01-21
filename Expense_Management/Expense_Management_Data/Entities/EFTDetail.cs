using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("EFTDetails", Schema = "dbo")]
public class EFTDetail
{
    public int EFTDetailID { get; set; }
    public int PaymentID { get; set; } 
    public string BankName { get; set; } 
    public string IBAN { get; set; } 
    public string TransactionReference { get; set; } 
    public DateTime TransactionDate { get; set; } 
    public bool IsActive { get; set; } 
    
    public virtual Payment Payment { get; set; }
}

public class EFTDetailConfiguration : IEntityTypeConfiguration<EFTDetail>
{
    public void Configure(EntityTypeBuilder<EFTDetail> builder)
    {
        builder.HasKey(e => e.EFTDetailID);
        builder.Property(e => e.EFTDetailID).ValueGeneratedOnAdd();
        builder.Property(e => e.PaymentID).IsRequired(true);
        builder.Property(e => e.BankName).IsRequired(true).HasMaxLength(100);
        builder.Property(e => e.IBAN).IsRequired(true).HasMaxLength(34);
        builder.Property(e => e.TransactionReference).IsRequired(true).HasMaxLength(100);
        builder.Property(e => e.TransactionDate).IsRequired(true);
        builder.Property(e=> e.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(e => e.EFTDetailID).IsUnique(true);
        builder.HasIndex(e => e.PaymentID).IsUnique(true);
        
        builder.HasOne(e => e.Payment)
            .WithMany() 
            .HasForeignKey(e => e.PaymentID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}