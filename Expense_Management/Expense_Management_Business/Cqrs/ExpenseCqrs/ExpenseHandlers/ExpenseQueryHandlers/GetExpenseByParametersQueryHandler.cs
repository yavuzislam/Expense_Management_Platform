using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Expenses.Responses;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseQueryHandlers;

public class GetExpenseByParametersQueryHandler:IRequestHandler<GetExpenseByParametersQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpenseByParametersQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetExpenseByParametersQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true);
        if (request.Status != 0)
            predicate.And(x => x.Status == request.Status);

        var list = await _dbContext.Set<Expense>()
            .Include(x => x.User)
            .Include(x => x.Category)
            .Where(x => x.Status == request.Status)
            .ToListAsync(cancellationToken);

        var mappedValues = _mapper.Map<IEnumerable<ExpenseResponse>>(list);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(mappedValues);
    }
}