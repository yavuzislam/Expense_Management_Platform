using System.Data;
using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Data.Context;
using Expense_Management_Schema.Reports.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportQueryHandlers;

public class
    GetPaymentReportQueryHandler : IRequestHandler<GetPaymentReportQuery,
    ApiResponse<IEnumerable<PaymentReportResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IDbConnection _dbConnection;

    public GetPaymentReportQueryHandler(ExpenseManagementDbContext dbContext, IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponse<IEnumerable<PaymentReportResponse>>> Handle(GetPaymentReportQuery request,
        CancellationToken cancellationToken)
    {
        var endDate = DateTime.Today.AddDays(1).AddTicks(-1);
        var startDate = endDate;

        string groupByClause;
        switch (request.Period.ToLower())
        {
            case "daily":
                startDate = endDate.AddDays(-1);
                groupByClause = "CONVERT(varchar, r.Date, 23)";
                break;
            case "weekly":
                startDate = endDate.AddDays(-7);
                groupByClause = "DATEPART(iso_week, r.Date)";
                break;
            case "monthly":
                startDate = endDate.AddMonths(-1);
                groupByClause = "DATEPART(month, r.Date)";
                break;
            default:
                throw new ArgumentException("Invalid period type. Allowed values: daily, weekly, monthly.");
        }

        string sql = $@"
    SELECT 
        {groupByClause} as TimePeriod,
        SUM(r.Amount) as TotalAmount,
        COUNT(r.ReportID) as PaymentCount
    FROM Reports r
    WHERE r.Date >= @StartDate AND r.Date <= @EndDate
    GROUP BY {groupByClause}";

        var reports = await _dbConnection.QueryAsync<PaymentReportResponse>(
            sql,
            new { StartDate = startDate, EndDate = endDate });

        return new ApiResponse<IEnumerable<PaymentReportResponse>>(reports);
    }
}