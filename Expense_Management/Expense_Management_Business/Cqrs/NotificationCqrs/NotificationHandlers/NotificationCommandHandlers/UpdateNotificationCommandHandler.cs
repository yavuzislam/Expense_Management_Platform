using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationHandlers.NotificationCommandHandlers;

public class UpdateNotificationCommandHandler:IRequestHandler<UpdateNotificationCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateNotificationCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Notification>()
            .FirstOrDefaultAsync(x => x.NotificationId == request.NotificationId, cancellationToken);
        if (entity is null)
            return new ApiResponse("Notification not found");
        
        entity.Message = request.Model.Message;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Notification updated successfully");
    }
}