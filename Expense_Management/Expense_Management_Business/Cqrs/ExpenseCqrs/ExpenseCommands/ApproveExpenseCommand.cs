using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;

public record ApproveExpenseCommand(int ExpenseNumber,int SenderUserNumber) : IRequest<ApiResponse>;