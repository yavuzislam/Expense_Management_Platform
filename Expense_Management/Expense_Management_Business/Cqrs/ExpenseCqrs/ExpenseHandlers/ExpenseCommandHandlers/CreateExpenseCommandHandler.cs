using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;
using Expense_Management_Business.Services.NotificationService;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Expenses.Responses;
using Hangfire;
using MediatR;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseCommandHandlers;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public CreateExpenseCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper,
        INotificationService notificationService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request,
        CancellationToken cancellationToken)
    {
        var expense = _mapper.Map<Expense>(request.Model);
        expense.UserID = request.Id;
        var result = await _dbContext.Set<Expense>().AddAsync(expense, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        BackgroundJob.Schedule(
            () => _notificationService.CreateNotificationAsync(expense.UserID,
                $"An expense claim has been entered by the user: {expense.UserID}"),
            TimeSpan.FromSeconds(10));
        var mappedResult = _mapper.Map<ExpenseResponse>(result.Entity);
        return new ApiResponse<ExpenseResponse>(mappedResult);
    }
}