using Expense_Management_Schema.Notifications.Requests;
using FluentValidation;

namespace Expense_Management_Business.Validators;

public class NotificationValidator: AbstractValidator<NotificationRequest>
{
    public NotificationValidator()
    {
        RuleFor(n => n.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty")
            .GreaterThan(0).WithMessage("User ID must be a positive number");
    
        RuleFor(n => n.Message)
            .NotEmpty().WithMessage("Message cannot be empty")
            .MaximumLength(500).WithMessage("Message must be less than or equal to 500 characters");   
    }
}