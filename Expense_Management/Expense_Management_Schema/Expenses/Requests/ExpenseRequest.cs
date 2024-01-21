namespace Expense_Management_Schema.Expenses.Requests;

public class ExpenseRequest
{
    public int CategoryID { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string Receipt { get; set; }
}