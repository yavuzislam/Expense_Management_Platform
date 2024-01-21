namespace Expense_Management_Schema.Reports.Responses;

public class PersonalExpenseReportResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string TimePeriod { get; set; }
    public decimal TotalAmount { get; set; }
}