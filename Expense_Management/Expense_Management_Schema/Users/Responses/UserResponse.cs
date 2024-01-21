using Expense_Management_Schema.Expenses.Responses;

namespace Expense_Management_Schema.Users.Responses;

public class UserResponse
{
    public int UserNumber { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string UserFullName { get; set; }
    public string IBAN { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
    public string Status { get; set; }
    public bool IsActive { get; set; }

    public virtual List<ExpenseResponse> Expenses { get; set; }
}