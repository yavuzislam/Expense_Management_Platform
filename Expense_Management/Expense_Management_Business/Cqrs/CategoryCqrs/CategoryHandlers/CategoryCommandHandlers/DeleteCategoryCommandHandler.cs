using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryHandlers.CategoryCommandHandlers;

public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteCategoryCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Category>()
            .FirstOrDefaultAsync(x => x.CategoryID == request.CategoryId, cancellationToken);
        if (entity is null)
            return new ApiResponse("Category not found");
        
        if (await _dbContext.Set<Expense>().AnyAsync(x => x.CategoryID == request.CategoryId && x.IsActive, cancellationToken))
            return new ApiResponse("Category cannot be deleted as it has active expenses");
        
        entity.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Category deleted successfully");
    }
}