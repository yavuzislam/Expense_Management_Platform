using Expense_Management_Base.Response;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;

public record GetExpensesByUserIdQuery(int UserId,int? status) : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;