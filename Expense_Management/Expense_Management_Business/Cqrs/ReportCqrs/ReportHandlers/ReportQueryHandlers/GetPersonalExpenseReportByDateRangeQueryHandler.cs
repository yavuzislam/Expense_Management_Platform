using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Data.Context;
using Expense_Management_Schema.Reports.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportQueryHandlers;

public class GetPersonalExpenseReportByDateRangeQueryHandler:IRequestHandler<GetPersonalExpenseReportByDateRangeQuery, ApiResponse<IEnumerable<PersonalExpenseReportResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public GetPersonalExpenseReportByDateRangeQueryHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task<ApiResponse<IEnumerable<PersonalExpenseReportResponse>>> Handle(GetPersonalExpenseReportByDateRangeQuery request, CancellationToken cancellationToken)
    {
        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);

            var endDate = request.EndDate.Date.AddDays(1).AddTicks(-1);
            var startDate = request.StartDate.Date;

            var sql = @"
                SELECT 
                    FORMAT(r.Date, 'yyyy-MM-dd') as TimePeriod, 
                    SUM(CASE WHEN r.Status = 1 THEN r.Amount ELSE 0 END) as ApprovedAmount,
                    SUM(CASE WHEN r.Status = 2 THEN r.Amount ELSE 0 END) as RejectedAmount,
                    SUM(r.Amount) as TotalAmount,
                    u.UserNumber as UserId,
                    (u.UserFirstName + ' ' + u.UserLastName) as UserName
                FROM Reports r 
                INNER JOIN Users u ON r.RequesterUserID = u.UserNumber
                WHERE r.Date >= @StartDate AND r.Date <= @EndDate 
                GROUP BY u.UserNumber, (u.UserFirstName + ' ' + u.UserLastName), FORMAT(r.Date, 'yyyy-MM-dd')";

            var reports = await connection.QueryAsync<PersonalExpenseReportResponse>(sql, new { StartDate = startDate, EndDate = endDate });

            return new ApiResponse<IEnumerable<PersonalExpenseReportResponse>>(reports);
        }
    }
}