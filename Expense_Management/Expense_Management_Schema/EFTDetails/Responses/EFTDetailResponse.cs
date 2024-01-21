namespace Expense_Management_Schema.EFTDetails.Responses;

public class EFTDetailResponse
{
    public int EFTDetailID { get; set; }
    public int PaymentID { get; set; } 
    public string BankName { get; set; } 
    public string IBAN { get; set; } 
    public string TransactionReference { get; set; }
    public string TransactionDate { get; set; } 
    public bool IsActive { get; set; }
}