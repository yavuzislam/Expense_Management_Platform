using Expense_Management_Schema.Payments.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class PaymentValidator : AbstractValidator<PaymentRequest>
{
    public PaymentValidator()
    {
        RuleFor(p => p.Amount)
            .NotEmpty().WithMessage("Amount cannot be empty")
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(p => p.Date)
            .NotEmpty().WithMessage("Date cannot be empty");
        
        RuleFor(p => p.UserID)
            .NotEmpty().WithMessage("User ID cannot be empty")
            .GreaterThan(0).WithMessage("User ID must be a positive number");

        RuleFor(p => p.Status)
            .NotEmpty().WithMessage("Status cannot be empty")
            .Must(s => s == 1 || s == 2).WithMessage("Status must be either 1 or 2");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description must be less than or equal to 500 characters");
    }
}