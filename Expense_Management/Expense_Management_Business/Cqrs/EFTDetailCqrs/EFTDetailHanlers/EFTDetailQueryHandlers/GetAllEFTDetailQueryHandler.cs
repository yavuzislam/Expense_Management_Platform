using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailHanlers.EFTDetailQueryHandlers;

public class GetAllEFTDetailQueryHandler:IRequestHandler<GetAllEFTDetailQuery, ApiResponse<IEnumerable<EFTDetailResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllEFTDetailQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<EFTDetailResponse>>> Handle(GetAllEFTDetailQuery request, CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<EFTDetail>()
            .Include(x => x.Payment)
            .ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<EFTDetailResponse>>(values);
        return new ApiResponse<IEnumerable<EFTDetailResponse>>(mappedValues);
    }
}