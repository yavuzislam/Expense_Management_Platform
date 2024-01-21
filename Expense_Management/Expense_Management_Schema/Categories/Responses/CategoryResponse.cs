using Expense_Management_Schema.Expenses.Responses;

namespace Expense_Management_Schema.Categories.Responses;

public class CategoryResponse
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual List<ExpenseResponse> Expenses { get; set; }
}