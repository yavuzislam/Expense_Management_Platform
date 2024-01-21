using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Reports", Schema = "dbo")]
public class Report 
{
    public int ReportID { get; set; }
    public int CreatedByUserID { get; set; } 
    public int RequesterUserID { get; set; } 
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; } 

    public virtual User CreatedByUser { get; set; } 
    public virtual User RequesterUser { get; set; } 
    public virtual Category Category { get; set; }
}

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(x => x.ReportID);
        builder.Property(x => x.ReportID).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedByUserID).IsRequired(true);
        builder.Property(x => x.RequesterUserID).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 2);
        builder.Property(x => x.Status).IsRequired(true);
        builder.Property(x => x.Date).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(x => x.ReportID).IsUnique(true);
        builder.HasIndex(x => x.CreatedByUserID);
        builder.HasIndex(x => x.RequesterUserID);
    }
}