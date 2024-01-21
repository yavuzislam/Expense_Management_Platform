using Expense_Management_Base.Response;
using Expense_Management_Schema.Expenses.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;

public record UpdateExpenseCommand(int ExpenseNumber, ExpenseRequest Model) : IRequest<ApiResponse>;