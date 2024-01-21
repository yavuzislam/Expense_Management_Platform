using Expense_Management_Base.Response;
using Expense_Management_Schema.Users.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserCommands;

public record UpdateUserCommand(int UserId, UserRequest Model) : IRequest<ApiResponse>;