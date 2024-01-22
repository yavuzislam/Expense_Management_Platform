using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationHandlers.NotificationCommandHandlers;

public class CreateNotificationCommandHandler:IRequestHandler<CreateNotificationCommand, ApiResponse<NotificationResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<NotificationResponse>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(request.Model);
        var result = await _dbContext.Set<Notification>().AddAsync(notification, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var mappedResult = _mapper.Map<NotificationResponse>(result.Entity);
        return new ApiResponse<NotificationResponse>(mappedResult);
    }
}