using Expense_Management_Base.Response;
using Expense_Management_Schema.Users.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserQueries;

public record GetUserByParametersQuery(string? Name, string? Surname, string? UserName,int? Role)
    : IRequest<ApiResponse<IEnumerable<UserResponse>>>;