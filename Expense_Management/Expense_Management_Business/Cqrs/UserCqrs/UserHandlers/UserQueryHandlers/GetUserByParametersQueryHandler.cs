using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Users.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserQueryHandlers;

public class GetUserByParametersQueryHandler:IRequestHandler<GetUserByParametersQuery, ApiResponse<IEnumerable<UserResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByParametersQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<UserResponse>>> Handle(GetUserByParametersQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<User>().AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.UserFirstName.Contains(request.Name.ToUpper()));

        if (!string.IsNullOrEmpty(request.Surname))
            query = query.Where(x => x.UserLastName.Contains(request.Surname.ToUpper()));

        if (!string.IsNullOrEmpty(request.UserName))
            query = query.Where(x => x.Username.Contains(request.UserName.ToUpper()));

        if (request.Role != null)
            query = query.Where(x => x.Role == request.Role);

        query = query.Include(x => x.Expenses);

        var list = await query.ToListAsync(cancellationToken);

        var mappedValues = _mapper.Map<IEnumerable<UserResponse>>(list);
        return new ApiResponse<IEnumerable<UserResponse>>(mappedValues);
    }
}