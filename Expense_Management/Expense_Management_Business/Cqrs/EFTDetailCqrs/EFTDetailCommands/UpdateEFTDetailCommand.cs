using Expense_Management_Base.Response;
using Expense_Management_Schema.EFTDetails.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;

public record UpdateEFTDetailCommand(int EFTDetailId, EFTDetailRequest Model) : IRequest<ApiResponse>;