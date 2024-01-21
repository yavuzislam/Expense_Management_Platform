using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;

namespace Expense_Management_Business.Services.NotificationService;

public class NotificationService : INotificationService
{
    private readonly ExpenseManagementDbContext _dbContext;

    public NotificationService(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateNotificationAsync(int userId, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            Message = message,
            IsRead = false,
            DateCreated = DateTime.UtcNow
        };

        await _dbContext.Notifications.AddAsync(notification);
        await _dbContext.SaveChangesAsync();
        
    }
}