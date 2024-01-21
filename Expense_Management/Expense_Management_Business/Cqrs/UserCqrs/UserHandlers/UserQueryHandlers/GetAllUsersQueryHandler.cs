using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Users.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserQueryHandlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<IEnumerable<UserResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<User>()
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Category)
            .OrderBy(x => x.Role == 1)
            .ThenBy(x => x.Role == 2)
            .ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<UserResponse>>(values);
        return new ApiResponse<IEnumerable<UserResponse>>(mappedValues);
    }
}