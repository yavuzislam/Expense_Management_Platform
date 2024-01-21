using Expense_Management_Base.Response;
using Expense_Management_Schema.Reports.Requests;
using Expense_Management_Schema.Reports.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;

public record CreateReportCommand(ReportRequest Model,int Id) : IRequest<ApiResponse<ReportResponse>>;