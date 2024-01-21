namespace Expense_Management_Schema.Reports.Responses;

public class ReportResponse
{
    public int ReportID { get; set; }
    public string RequesterUserName { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public bool IsActive { get; set; }
}