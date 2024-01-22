using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryHandlers.CategoryCommandHandlers;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateCategoryCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Category>()
            .FirstOrDefaultAsync(x => x.CategoryID == request.CategoryId, cancellationToken);
        
        if (entity is null)
            return new ApiResponse("Category not found");

        if (await _dbContext.Set<Category>()
                .AnyAsync(x => x.Name == request.Model.Name && x.CategoryID != request.CategoryId, cancellationToken))
            return new ApiResponse("Category already exists");

        entity.Name = request.Model.Name;
        entity.Description = request.Model.Description;
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse("Category updated successfully");
    }
}