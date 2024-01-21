namespace Expense_Management_Schema.Users.Requests;

public class UserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
    public string Email { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string IBAN { get; set; }
}