using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationHandlers.NotificationCommandHandlers;

public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public DeleteNotificationCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Notification>()
            .FirstOrDefaultAsync(x => x.NotificationId == request.NotificationId, cancellationToken);
        if (entity is null)
            return new ApiResponse("Notification not found");

        _dbContext.Set<Notification>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse("Notification deleted successfully");
    }
}