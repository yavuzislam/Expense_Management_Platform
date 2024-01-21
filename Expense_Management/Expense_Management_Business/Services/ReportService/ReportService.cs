using Expense_Management_Base.Response;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;

namespace Expense_Management_Business.Services.ReportService;

public class ReportService : IReportService
{
    private readonly ExpenseManagementDbContext _dbContext;

    public ReportService(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> CreateReportAsync(int createdByUserId, int requesterUserId, int categoryId, decimal amount, int status, DateTime date)
    {
        var report = new Report
        {
            CreatedByUserID = createdByUserId,
            RequesterUserID = requesterUserId,
            CategoryId = categoryId,
            Amount = amount,
            Status = status,
            Date = DateTime.UtcNow
        };
        
        await _dbContext.Set<Report>().AddAsync(report);
        await _dbContext.SaveChangesAsync();
        return new ApiResponse("Report created successfully");
    }
}