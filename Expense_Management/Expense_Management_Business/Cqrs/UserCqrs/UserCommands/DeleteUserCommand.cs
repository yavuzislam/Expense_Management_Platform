using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserCommands;

public record DeleteUserCommand(int UserId) : IRequest<ApiResponse>;