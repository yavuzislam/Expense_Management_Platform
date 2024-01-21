using Expense_Management_Schema.Token.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class TokenValidator : AbstractValidator<TokenRequest>
{
    public TokenValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(50);
    }
}