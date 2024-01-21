using System.Data;
using AutoMapper;
using Dapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Reports.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportHandlers.ReportQueryHandlers;

public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, ApiResponse<ReportResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReportByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetReportByIdQuery request,
        CancellationToken cancellationToken)
    {
        var sql = @"
        SELECT r.*, u.*, c.*
        FROM [dbo].[Reports] r
        INNER JOIN [dbo].[Users] u ON r.CreatedByUserID = u.UserNumber
        INNER JOIN [dbo].[Categories] c ON r.CategoryId = c.CategoryId
        WHERE r.ReportID = @ReportID";

        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);
            var report = await connection.QueryAsync<Report, User, Category, ReportResponse>(
                sql,
                (report, user, category) =>
                {
                    var reportResponse = _mapper.Map<ReportResponse>(report);
                    reportResponse.RequesterUserName = $"{user.UserFirstName} {user.UserLastName}";
                    reportResponse.CategoryName = category.Name;
                    return reportResponse;
                },
                new { ReportID = request.ReportId },
                splitOn: "UserNumber,CategoryId");

            return new ApiResponse<ReportResponse>(report.FirstOrDefault());
        }
    }
}