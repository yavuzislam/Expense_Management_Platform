namespace Expense_Management_Schema.Reports.Responses;

public class PaymentReportResponse
{
    public string TimePeriod { get; set; }
    public decimal TotalAmount { get; set; }
    public int PaymentCount { get; set; } 
}