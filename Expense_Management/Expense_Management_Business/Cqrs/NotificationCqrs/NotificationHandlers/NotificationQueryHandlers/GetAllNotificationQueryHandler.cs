using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.NotificationCqrs.NotificationHandlers.NotificationQueryHandlers;

public class GetAllNotificationQueryHandler:IRequestHandler<GetAllNotificationQuery, ApiResponse<IEnumerable<NotificationResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllNotificationQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<NotificationResponse>>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<Notification>()
            .Include(x=>x.User)
            .ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<NotificationResponse>>(values);
        return new ApiResponse<IEnumerable<NotificationResponse>>(mappedValues);
    }
}