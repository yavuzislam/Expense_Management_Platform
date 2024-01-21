using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Users.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserQueryHandlers;

public class GetUserByIdQueryHandler:IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<User>()
            .Include(x => x.Expenses)
            .FirstOrDefaultAsync(x => x.UserNumber == request.UserId, cancellationToken);

        if (value is null)
            return new ApiResponse<UserResponse>("User not found");

        var mappedValue = _mapper.Map<UserResponse>(value);
        return new ApiResponse<UserResponse>(mappedValue);
    }
}