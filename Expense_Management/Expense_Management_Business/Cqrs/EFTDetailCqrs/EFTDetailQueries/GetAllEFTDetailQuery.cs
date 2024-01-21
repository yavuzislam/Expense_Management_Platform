using Expense_Management_Base.Response;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailQueries;

public record GetAllEFTDetailQuery() : IRequest<ApiResponse<IEnumerable<EFTDetailResponse>>>;