namespace Expense_Management_Business.Services.NotificationService;

public interface INotificationService
{
    public Task CreateNotificationAsync(int userId, string message);
}