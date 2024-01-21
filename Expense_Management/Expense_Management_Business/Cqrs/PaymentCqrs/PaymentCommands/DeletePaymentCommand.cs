using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;

public record DeletePaymentCommand(int PaymentId) : IRequest<ApiResponse>;