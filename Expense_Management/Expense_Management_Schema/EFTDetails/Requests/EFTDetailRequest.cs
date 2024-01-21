namespace Expense_Management_Schema.EFTDetails.Requests;

public class EFTDetailRequest
{
    public int PaymentID { get; set; } 
    public string BankName { get; set; } 
    public string IBAN { get; set; }
    public DateTime TransactionDate { get; set; } 
}