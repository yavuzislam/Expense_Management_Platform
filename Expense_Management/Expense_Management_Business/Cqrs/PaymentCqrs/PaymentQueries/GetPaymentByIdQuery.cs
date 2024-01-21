using Expense_Management_Base.Response;
using Expense_Management_Schema.Payments.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentQueries;

public record GetPaymentByIdQuery(int PaymentId) : IRequest<ApiResponse<PaymentResponse>>;