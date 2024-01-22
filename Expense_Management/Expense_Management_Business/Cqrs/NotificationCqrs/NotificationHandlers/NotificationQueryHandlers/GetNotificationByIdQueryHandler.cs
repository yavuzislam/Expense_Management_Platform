using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationHandlers.NotificationQueryHandlers;

public class GetNotificationByIdQueryHandler:IRequestHandler<GetNotificationByIdQuery, ApiResponse<NotificationResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNotificationByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<NotificationResponse>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<Notification>()
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.NotificationId == request.NotificationId, cancellationToken);
         
        if (value is null)
            return new ApiResponse<NotificationResponse>("Notification not found");
        
        var mappedValue = _mapper.Map<NotificationResponse>(value);
        return new ApiResponse<NotificationResponse>(mappedValue);
    }
}