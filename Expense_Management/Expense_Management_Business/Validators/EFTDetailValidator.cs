using Expense_Management_Schema.EFTDetails.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class EFTDetailValidator : AbstractValidator<EFTDetailRequest>
{
    public EFTDetailValidator()
    {
        RuleFor(e => e.PaymentID)
            .NotEmpty().WithMessage("Payment ID cannot be empty")
            .GreaterThan(0).WithMessage("Payment ID must be a positive number");

        RuleFor(e => e.BankName)
            .NotEmpty().WithMessage("Bank name cannot be empty")
            .MaximumLength(100).WithMessage("Bank name must be less than or equal to 100 characters");

        RuleFor(e => e.IBAN)
            .NotEmpty().WithMessage("IBAN cannot be empty")
            .Length(5).WithMessage("IBAN must be 34 characters long");

        RuleFor(e => e.TransactionDate)
            .NotEmpty().WithMessage("Transaction date cannot be empty");
    }
}