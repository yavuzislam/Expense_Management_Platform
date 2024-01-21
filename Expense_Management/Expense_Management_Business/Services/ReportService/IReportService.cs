using Expense_Management_Base.Response;

namespace Expense_Management_Business.Services.ReportService;

public interface IReportService
{
    Task<ApiResponse> CreateReportAsync(int createdByUserId, int requesterUserId, int categoryId, decimal amount, int status, DateTime date);
}