namespace Expense_Management_Schema.Payments.Responses;

public class PaymentResponse
{
    public int PaymentID { get; set; }
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } 
}