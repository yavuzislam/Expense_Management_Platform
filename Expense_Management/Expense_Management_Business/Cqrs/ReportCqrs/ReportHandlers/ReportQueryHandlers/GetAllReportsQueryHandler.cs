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

public class GetAllReportsQueryHandler:IRequestHandler<GetAllReportsQuery, ApiResponse<IEnumerable<ReportResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllReportsQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ReportResponse>>> Handle(GetAllReportsQuery request,
        CancellationToken cancellationToken)
    {
        var sql = @"
    SELECT 
        r.*, 
        u.*, 
        c.*
    FROM 
        Reports r
        INNER JOIN Users u ON r.CreatedByUserID = u.UserNumber
        INNER JOIN Categories c ON r.CategoryId = c.CategoryId";
        

        using (var connection = _dbContext.Database.GetDbConnection())
        {
            await connection.OpenAsync(cancellationToken);
            var reports = await connection.QueryAsync<Report, User, Category, ReportResponse>(
                sql,
                (report, user, category) =>
                {
                    var reportResponse = _mapper.Map<ReportResponse>(report);
                    reportResponse.RequesterUserName = $"{user.UserFirstName} {user.UserLastName}";
                    reportResponse.CategoryName = category.Name;
                    return reportResponse;
                },
                splitOn: "UserNumber,CategoryId");

            return new ApiResponse<IEnumerable<ReportResponse>>(reports);
        }
    }
}