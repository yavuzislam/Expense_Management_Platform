using System.Data;
using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Data.Context;
using Expense_Management_Schema.Reports.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportQueryHandlers;

public class GetCustomPaymentReportQueryHandler:IRequestHandler<GetCustomPaymentReportQuery, ApiResponse<PaymentReportResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IDbConnection _dbConnection;

    public GetCustomPaymentReportQueryHandler(ExpenseManagementDbContext dbContext,  IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponse<PaymentReportResponse>> Handle(GetCustomPaymentReportQuery request, CancellationToken cancellationToken)
    {
        string sql = @"
            SELECT 
                @StartDate as StartDate, 
                @EndDate as EndDate, 
                SUM(r.Amount) as TotalAmount,
                COUNT(*) as ReportCount
            FROM Reports r
            WHERE r.Date >= @StartDate AND r.Date <= @EndDate";

        var report = await _dbConnection.QuerySingleOrDefaultAsync<PaymentReportResponse>(
            sql, 
            new { StartDate = request.StartDate, EndDate = request.EndDate });

        return new ApiResponse<PaymentReportResponse>(report);
    }
}