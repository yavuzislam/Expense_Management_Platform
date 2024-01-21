namespace Expense_Management_Schema.Reports.Responses;

public class ExpenseReportResponse
{
    public string TimePeriod { get; set; }
    public decimal TotalAmount { get; set; } 
    public decimal ApprovedAmount { get; set; } 
    public decimal RejectedAmount { get; set; }  
}