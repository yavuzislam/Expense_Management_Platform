namespace Expense_Management_Schema.Expenses.Responses;

public class ExpenseResponse
{
    public int ExpenseNumber { get; set; }
    public string UserName { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Receipt { get; set; }
    public string Status { get; set; }
    public string RejectionReason { get; set; }
}