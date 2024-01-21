using Expense_Management_Schema.Reports.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class ReportValidator : AbstractValidator<ReportRequest>
{
    public ReportValidator()
    {
        RuleFor(report => report.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(report => report.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than 0.")
            .ScalePrecision(2, 18).WithMessage("Amount must not exceed 2 decimal places and must be less than 18 digits in total.");

        RuleFor(report => report.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(report => report.Date)
            .NotEmpty().WithMessage("Date is required.");
    }
}