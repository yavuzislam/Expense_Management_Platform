using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;

public record DeleteEFTDetailCommand(int EFTDetailId) : IRequest<ApiResponse>;