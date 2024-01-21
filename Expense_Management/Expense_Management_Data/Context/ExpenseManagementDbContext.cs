using Expense_Management_Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Data.Context;

public class ExpenseManagementDbContext : DbContext
{
   
    public ExpenseManagementDbContext(DbContextOptions<ExpenseManagementDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<EFTDetail> EftDetails { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new EFTDetailConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
    }
}