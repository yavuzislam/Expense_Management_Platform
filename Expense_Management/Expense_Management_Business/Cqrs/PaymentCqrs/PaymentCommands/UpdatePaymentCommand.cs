using Expense_Management_Base.Response;
using Expense_Management_Schema.Payments.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;

public record UpdatePaymentCommand(int PaymentId, PaymentRequest Model) : IRequest<ApiResponse>;