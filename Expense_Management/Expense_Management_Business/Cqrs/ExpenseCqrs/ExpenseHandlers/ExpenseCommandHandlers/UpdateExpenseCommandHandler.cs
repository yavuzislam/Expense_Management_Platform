using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseCommandHandlers;

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateExpenseCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseNumber == request.ExpenseNumber, cancellationToken);

        if (entity is null)
            return new ApiResponse("Expense not found");

        var categoryExists = await _dbContext.Set<Category>()
            .AnyAsync(x => x.CategoryID == request.Model.CategoryID, cancellationToken);

        if (!categoryExists)
            return new ApiResponse("Invalid UserID or CategoryID");

        entity.CategoryID = request.Model.CategoryID;
        entity.Amount = request.Model.Amount;
        entity.Description = request.Model.Description;
        entity.Date = request.Model.Date;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Expense updated successfully");
    }
}