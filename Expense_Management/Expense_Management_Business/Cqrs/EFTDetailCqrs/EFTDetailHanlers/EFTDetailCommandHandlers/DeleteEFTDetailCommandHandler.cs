using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailHanlers.EFTDetailCommandHandlers;

public class DeleteEFTDetailCommandHandler : IRequestHandler<DeleteEFTDetailCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteEFTDetailCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteEFTDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<EFTDetail>()
            .FirstOrDefaultAsync(x => x.EFTDetailID == request.EFTDetailId, cancellationToken);

        if (entity is null)
            return new ApiResponse("EFTDetail not found");

        entity.IsActive = false;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("EFTDetail deleted successfully");
    }
}