using Expense_Management_Base.Response;

namespace Expense_Management_Business.Services.PaymentService;

public interface IPaymentService
{
    Task<ApiResponse> PerformEftAsync(int receiverUser, decimal amount, int senderUser,int categoryId, int status,DateTime date);
}