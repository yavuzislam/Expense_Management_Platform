using Expense_Management_Base.Response;
using Expense_Management_Schema.Users.Requests;
using Expense_Management_Schema.Users.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserCommands;

public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;