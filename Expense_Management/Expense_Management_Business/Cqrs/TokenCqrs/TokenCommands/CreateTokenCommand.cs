using Expense_Management_Base.Response;
using Expense_Management_Schema.Token.Requests;
using Expense_Management_Schema.Token.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.TokenCqrs.TokenCommands;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;