using Expense_Management_Base.Response;
using Expense_Management_Schema.Notifications.Requests;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;

public record CreateNotificationCommand(NotificationRequest Model) : IRequest<ApiResponse<NotificationResponse>>;