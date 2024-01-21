using Expense_Management_Schema.Users.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class UserValidator: AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username cannot be empty")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(5).WithMessage("Password must be at least 6 characters long");

        RuleFor(user => user.Role)
            .InclusiveBetween(1, 2).WithMessage("Role must be  1 or 2");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email address cannot be empty")
            .EmailAddress().WithMessage("Please enter a valid email address");

        RuleFor(user => user.IBAN)
            .NotEmpty().WithMessage("IBAN cannot be empty")
            .Length(5).WithMessage("IBAN must be 5 characters long");
        
        RuleFor(user => user.UserFirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .MaximumLength(50).WithMessage("First name cannot be more than 50 characters");

        RuleFor(user => user.UserLastName)
            .NotEmpty().WithMessage("Last name cannot be empty")
            .MaximumLength(50).WithMessage("Last name cannot be more than 50 characters");
    }
}