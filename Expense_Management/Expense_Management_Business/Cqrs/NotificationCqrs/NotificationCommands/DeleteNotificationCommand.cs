using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;

public record DeleteNotificationCommand(int NotificationId) : IRequest<ApiResponse>;