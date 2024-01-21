using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense_Management_Data.Entities;

[Table("Categories", Schema = "dbo")]
public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    
    public virtual List<Expense> Expenses { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(x => x.Name).IsUnique(true);

        builder.HasMany(x => x.Expenses)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryID);

        builder.HasData(
            new Category { CategoryID = 1, Name = "Food", Description = "Food expenses", IsActive = true },
            new Category
                { CategoryID = 2, Name = "Transportation", Description = "Transportation expenses", IsActive = true },
            new Category
                { CategoryID = 3, Name = "Accommodation", Description = "Accommodation expenses", IsActive = true },
            new Category
                { CategoryID = 4, Name = "Entertainment", Description = "Entertainment expenses", IsActive = true },
            new Category { CategoryID = 5, Name = "Other", Description = "Other expenses", IsActive = true }
        );
    }
}