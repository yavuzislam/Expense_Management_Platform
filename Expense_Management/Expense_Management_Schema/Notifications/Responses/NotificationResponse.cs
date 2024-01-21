namespace Expense_Management_Schema.Notifications.Responses;

public class NotificationResponse
{
    public int NotificationId { get; set; }
    public string UserName { get; set; } 
    public string Message { get; set; } 
    public bool IsRead { get; set; } 
    public string DateCreated { get; set; }
}