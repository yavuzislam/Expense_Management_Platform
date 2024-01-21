using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;

public record DeleteExpenseCommand(int ExpenseNumber) : IRequest<ApiResponse>;