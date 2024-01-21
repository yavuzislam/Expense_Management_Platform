namespace Expense_Management_Schema.Reports.Requests;

public class ReportRequest
{
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }
    public DateTime? Date { get; set; }
}