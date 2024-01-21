using Expense_Management_Base.Response;

namespace Expense_Management_Business.Services.EFTService;

public interface IEFTService
{
    Task<ApiResponse> PerformEftAsync(int senderUserId, int receiverUserId, decimal amount, string bankName, string iban);
}