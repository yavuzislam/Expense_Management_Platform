using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;

public record DeleteReportCommand(int ReportId) : IRequest<ApiResponse>;