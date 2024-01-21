using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;

public record RejectExpenseCommand(int ExpenseNumber,int SenderUserNumber,string Reason) : IRequest<ApiResponse>;