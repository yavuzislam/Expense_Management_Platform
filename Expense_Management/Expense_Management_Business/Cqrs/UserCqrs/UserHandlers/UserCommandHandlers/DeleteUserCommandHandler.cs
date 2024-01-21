using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserCommandHandlers;

public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteUserCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.UserNumber == request.UserId, cancellationToken);
        if (entity is null)
            return new ApiResponse("User not found");
        if (!entity.IsActive)
            return new ApiResponse("User already deleted");
        entity.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("User deleted successfully")
        {
            Success = true
        };
    }
}