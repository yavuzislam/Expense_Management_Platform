using Expense_Management_Schema.Categories.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class CategoryValidator : AbstractValidator<CategoryRequest>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name cannot be empty")
            .Length(1, 50).WithMessage("Category name must be between 1 and 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description must be less than or equal to 100 characters");
    }
}