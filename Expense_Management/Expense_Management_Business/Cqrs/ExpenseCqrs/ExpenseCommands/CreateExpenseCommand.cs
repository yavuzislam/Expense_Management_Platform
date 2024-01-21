using Expense_Management_Base.Response;
using Expense_Management_Schema.Expenses.Requests;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;

public record CreateExpenseCommand(ExpenseRequest Model,int Id) : IRequest<ApiResponse<ExpenseResponse>>;