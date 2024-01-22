using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseQueryHandlers;

public class
    GetExpensesByUserIdQueryHandler : IRequestHandler<GetExpensesByUserIdQuery,
    ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpensesByUserIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetExpensesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<Expense>()
            .Include(x => x.User)
            .Include(x => x.Category)
            .Where(x => x.UserID == request.UserId);

        if (request.status.HasValue)
            query = query.Where(x => x.Status == request.status.Value);

        var expenses = await query.ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<ExpenseResponse>>(expenses);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(mappedValues);
    }
}