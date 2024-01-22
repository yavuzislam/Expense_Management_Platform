using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseCommandHandlers;

public class DeleteExpenseCommandHandler:IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteExpenseCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseNumber == request.ExpenseNumber, cancellationToken);
        
        if (entity is null)
            return new ApiResponse("Expense not found");
        
        if (!entity.IsActive)
            return new ApiResponse("Expense already deleted");

        entity.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Expense deleted successfully");
    }
}