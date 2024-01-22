using System.Data;
using AutoMapper;
using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Reports.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportCommandHandlers;

public class CreateReportCommandHandler:IRequestHandler<CreateReportCommand, ApiResponse<ReportResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;

    public CreateReportCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper, IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dbConnection = dbConnection;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(CreateReportCommand request,
        CancellationToken cancellationToken)
    {
        var sql = @"
        INSERT INTO [dbo].[Reports] ([Amount], [CategoryId], [CreatedByUserID], [Date], [Status])
        OUTPUT INSERTED.[ReportID], INSERTED.[IsActive]
        VALUES (@Amount, @CategoryId, @CreatedByUserID, @Date, @Status)";

        var parameters = new DynamicParameters();
        parameters.Add("CreatedByUserID", request.Id);
        parameters.Add("@Amount", request.Model.Amount);
        parameters.Add("@CategoryId", request.Model.CategoryId);
        parameters.Add("@Date", request.Model.Date);
        parameters.Add("@Status", request.Model.Status);

        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);
            // var insertedReports=await connection.ExecuteAsync(sql, parameters);
            var insertedReports = await connection.QueryAsync<Report>(sql, parameters);
            var reportResponse = insertedReports.Select(report => _mapper.Map<ReportResponse>(report)).FirstOrDefault();

            return new ApiResponse<ReportResponse>(reportResponse);
        }
    }
}