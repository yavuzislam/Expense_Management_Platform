using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailHanlers.EFTDetailQueryHandlers;

public class GetEFTDetailByIdQueryHandler: IRequestHandler<GetEFTDetailByIdQuery, ApiResponse<EFTDetailResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetEFTDetailByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<EFTDetailResponse>> Handle(GetEFTDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<EFTDetail>()
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.EFTDetailID == request.EFTDetailId, cancellationToken);
        
        if (value is null)
            return new ApiResponse<EFTDetailResponse>("EFTDetail not found");
        
        var mappedValue = _mapper.Map<EFTDetailResponse>(value);
        return new ApiResponse<EFTDetailResponse>(mappedValue);
    }
}