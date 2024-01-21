using Expense_Management_Base.Response;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailQueries;

public record GetEFTDetailByIdQuery(int EFTDetailId) : IRequest<ApiResponse<EFTDetailResponse>>;