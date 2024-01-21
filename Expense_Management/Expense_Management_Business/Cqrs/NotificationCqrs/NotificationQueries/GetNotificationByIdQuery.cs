using Expense_Management_Base.Response;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationQueries;

public record GetNotificationByIdQuery(int NotificationId) : IRequest<ApiResponse<NotificationResponse>>;