using Expense_Management_Schema.Expenses.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class ExpenseValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseValidator()
    {
        RuleFor(x => x.CategoryID)
            .NotEmpty().WithMessage("Category ID cannot be empty")
            .GreaterThan(0).WithMessage("Category ID must be greater than 0");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount cannot be empty")
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Description)
            .MaximumLength(50).WithMessage("Description must be less than or equal to 50 characters");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date cannot be empty")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future");

        RuleFor(x => x.Receipt)
            .NotEmpty().WithMessage("Receipt cannot be empty");
    }
}