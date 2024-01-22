using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;
using Expense_Management_Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportCommandHandlers;

public class UpdateReportCommandHandler:IRequestHandler<UpdateReportCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateReportCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var sql = @"
    UPDATE Reports
    SET Amount = @Amount, CategoryId = @CategoryId, Date = @Date, Status = @Status
    WHERE ReportID = @ReportID";


        var parameters = new DynamicParameters();
        parameters.Add("@ReportID", request.ReportId);
        parameters.Add("@Amount", request.Model.Amount);
        parameters.Add("@CategoryId", request.Model.CategoryId);
        parameters.Add("@Date", request.Model.Date);
        parameters.Add("@Status", request.Model.Status);

        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken); 
            await connection.ExecuteAsync(sql, parameters);
            
            return new ApiResponse("Report updated successfully");
        }
    }
}