using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailHanlers.EFTDetailCommandHandlers;

public class CreateEFTDetailCommandHandler:IRequestHandler<CreateEFTDetailCommand, ApiResponse<EFTDetailResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateEFTDetailCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<EFTDetailResponse>> Handle(CreateEFTDetailCommand request,
        CancellationToken cancellationToken)
    {
        var eftDetail = _mapper.Map<EFTDetail>(request.Model);
        eftDetail.TransactionReference = new Random().Next(1000000, 9999999).ToString();

        if (await _dbContext.Set<EFTDetail>()
                .AnyAsync(x => x.IBAN == request.Model.IBAN, cancellationToken))
            return new ApiResponse<EFTDetailResponse>("EFTDetail already exists");

        if (await _dbContext.Set<EFTDetail>()
                .AnyAsync(x => x.PaymentID == request.Model.PaymentID, cancellationToken))
            return new ApiResponse<EFTDetailResponse>("EFTDetail already exists");

        if (await _dbContext.Set<EFTDetail>()
                .AnyAsync(x => x.TransactionReference == eftDetail.TransactionReference, cancellationToken))
            return new ApiResponse<EFTDetailResponse>("EFTDetail already exists");

        var result = await _dbContext.Set<EFTDetail>().AddAsync(eftDetail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var mappedResult = _mapper.Map<EFTDetailResponse>(result.Entity);
        return new ApiResponse<EFTDetailResponse>(mappedResult);
    }
}