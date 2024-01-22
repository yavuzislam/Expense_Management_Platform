using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailHanlers.EFTDetailCommandHandlers;

public class UpdateEFTDetailCommandHandler:IRequestHandler<UpdateEFTDetailCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateEFTDetailCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateEFTDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<EFTDetail>()
            .FirstOrDefaultAsync(x => x.EFTDetailID == request.EFTDetailId, cancellationToken);
        if (entity is null)
            return new ApiResponse("EFTDetail not found");

        if (await _dbContext.Set<EFTDetail>()
                .AnyAsync(x => x.IBAN == request.Model.IBAN && x.EFTDetailID != request.EFTDetailId,
                    cancellationToken))
            return new ApiResponse("EFTDetail already exists");

        if (await _dbContext.Set<EFTDetail>()
                .AnyAsync(x => x.PaymentID == request.Model.PaymentID && x.EFTDetailID != request.EFTDetailId,
                    cancellationToken))
            return new ApiResponse("EFTDetail already exists");

        entity.BankName = request.Model.BankName;
        entity.IBAN = request.Model.IBAN;
        entity.TransactionReference = new Random().Next(1000000, 9999999).ToString();

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("EFTDetail updated successfully");
    }
}