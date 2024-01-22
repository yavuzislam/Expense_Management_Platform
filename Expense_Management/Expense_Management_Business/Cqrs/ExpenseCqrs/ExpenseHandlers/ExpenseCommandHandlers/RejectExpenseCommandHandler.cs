using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;
using Expense_Management_Business.Services.NotificationService;
using Expense_Management_Business.Services.PaymentService;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseCommandHandlers;

public class RejectExpenseCommandHandler : IRequestHandler<RejectExpenseCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly INotificationService _notificationService;
    private readonly IPaymentService _paymentService;

    public RejectExpenseCommandHandler(ExpenseManagementDbContext dbContext, INotificationService notificationService,
        IPaymentService paymentService)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
        _paymentService = paymentService;
    }

    public async Task<ApiResponse> Handle(RejectExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseNumber == request.ExpenseNumber, cancellationToken);
        if (entity is null)
            return new ApiResponse("Expense not found");
        if (entity.Status == 2)
            return new ApiResponse("Expense already rejected");

        entity.Status = 2;
        entity.RejectionReason = request.Reason;

        var job = BackgroundJob.Schedule(
            () => _paymentService.PerformEftAsync(entity.UserID, entity.Amount, request.SenderUserNumber,
                entity.CategoryID, 2, entity.Date),
            TimeSpan.FromSeconds(10));

        BackgroundJob.ContinueJobWith(job,
            () => _notificationService.CreateNotificationAsync(entity.UserID,
                $"Your expense claim has been rejected. Reason: {request.Reason}"));

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Expense rejected with reason:" + request.Reason);
    }
}