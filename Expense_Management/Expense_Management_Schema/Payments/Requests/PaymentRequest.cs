namespace Expense_Management_Schema.Payments.Requests;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int UserID { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
}