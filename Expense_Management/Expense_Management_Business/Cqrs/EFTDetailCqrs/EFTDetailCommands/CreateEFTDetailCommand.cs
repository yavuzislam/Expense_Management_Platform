using Expense_Management_Base.Response;
using Expense_Management_Schema.EFTDetails.Requests;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;

public record CreateEFTDetailCommand(EFTDetailRequest Model) : IRequest<ApiResponse<EFTDetailResponse>>;