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

public class ApproveExpenseCommandHandler : IRequestHandler<ApproveExpenseCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly INotificationService _notificationService;
    private readonly IPaymentService _paymentService;

    public ApproveExpenseCommandHandler(ExpenseManagementDbContext dbContext, INotificationService notificationService,
        IPaymentService paymentService)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
        _paymentService = paymentService;
    }

    public async Task<ApiResponse> Handle(ApproveExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseNumber == request.ExpenseNumber, cancellationToken);

        if (entity is null)
            return new ApiResponse("Expense not found");

        if (entity.Status == 1)
            return new ApiResponse("Expense already approved");

        entity.Status = 1;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var job = BackgroundJob.Schedule(
            () => _paymentService.PerformEftAsync(entity.UserID, entity.Amount, request.SenderUserNumber,
                entity.CategoryID, 1, entity.Date),
            TimeSpan.FromSeconds(10));

        BackgroundJob.ContinueJobWith(job,
            () => _notificationService.CreateNotificationAsync(entity.UserID,
                "Your expense claim has been approved"));

        return new ApiResponse("Expense approved successfully");
    }
}