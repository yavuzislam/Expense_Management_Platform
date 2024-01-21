using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Data.Context;
using Expense_Management_Schema.Reports.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportQueryHandlers;

public class GetExpenseReportQueryHandler: IRequestHandler<GetExpenseReportQuery, ApiResponse<IEnumerable<ExpenseReportResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public GetExpenseReportQueryHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseReportResponse>>> Handle(GetExpenseReportQuery request, CancellationToken cancellationToken)
    {
        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);

            var endDate = DateTime.Today.AddDays(1).AddTicks(-1); // Günün sonuna ayarla
            var startDate = endDate;

            switch (request.Period.ToLower())
            {
                case "daily":
                    startDate = endDate.AddDays(-1).AddTicks(1); // Önceki günün sonundan başla
                    break;
                case "weekly":
                    startDate = endDate.AddDays(-7);
                    break;
                case "monthly":
                    startDate = endDate.AddMonths(-1);
                    break;
                default:
                    throw new ArgumentException("Invalid period type. Allowed values: daily, weekly, monthly.");
            }

            var sql = @"
            SELECT 
                FORMAT(r.Date, 'yyyy-MM-dd') as TimePeriod, 
                SUM(CASE WHEN r.Status = 1 THEN r.Amount ELSE 0 END) as ApprovedAmount,
                SUM(CASE WHEN r.Status = 2 THEN r.Amount ELSE 0 END) as RejectedAmount,
                SUM(r.Amount) as TotalAmount
            FROM Reports r 
            WHERE r.Date >= @StartDate AND r.Date <= @EndDate 
            GROUP BY FORMAT(r.Date, 'yyyy-MM-dd')";

            var reports = await connection.QueryAsync<ExpenseReportResponse>(sql, new { StartDate = startDate, EndDate = endDate });

            return new ApiResponse<IEnumerable<ExpenseReportResponse>>(reports);
        }
    }

}