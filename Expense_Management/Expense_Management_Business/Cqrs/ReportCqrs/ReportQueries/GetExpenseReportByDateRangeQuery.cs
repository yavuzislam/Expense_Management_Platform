﻿using Expense_Management_Base.Response;
using Expense_Management_Schema.Reports.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;

public record GetExpenseReportByDateRangeQuery(DateTime StartDate, DateTime EndDate)
    : IRequest<ApiResponse<IEnumerable<ExpenseReportResponse>>>;