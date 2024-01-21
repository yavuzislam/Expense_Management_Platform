using Expense_Management_Base.Response;
using Expense_Management_Schema.Users.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserQueries;

public record GetAllUsersQuery() : IRequest<ApiResponse<IEnumerable<UserResponse>>>;