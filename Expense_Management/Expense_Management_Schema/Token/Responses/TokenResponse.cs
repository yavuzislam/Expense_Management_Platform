namespace Expense_Management_Schema.Token.Responses;

public class TokenResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}