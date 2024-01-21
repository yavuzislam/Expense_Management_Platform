using Expense_Management_Base.Response;
using Expense_Management_Schema.Notifications.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;

public record UpdateNotificationCommand(int NotificationId, NotificationRequest Model) : IRequest<ApiResponse>;