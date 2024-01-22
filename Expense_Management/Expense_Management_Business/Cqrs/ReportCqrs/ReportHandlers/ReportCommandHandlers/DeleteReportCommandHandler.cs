using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;
using Expense_Management_Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportCommandHandlers;

public class DeleteReportCommandHandler:IRequestHandler<DeleteReportCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteReportCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
    DELETE FROM Reports
    WHERE ReportID = @ReportID";

        var parameters = new DynamicParameters();
        parameters.Add("@ReportID", request.ReportId);

        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);
            await connection.ExecuteAsync(sql, parameters);
            return new ApiResponse("Report deleted successfully");
        }
    }
}