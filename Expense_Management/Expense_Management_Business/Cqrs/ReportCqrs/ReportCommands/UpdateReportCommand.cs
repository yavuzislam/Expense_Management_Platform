using Expense_Management_Base.Response;
using Expense_Management_Schema.Reports.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;

public record UpdateReportCommand(int ReportId, ReportRequest Model) : IRequest<ApiResponse>;