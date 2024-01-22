using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseQueryHandlers;

public class GetAllExpensesQueryHandler:IRequestHandler<GetAllExpensesQuery, ApiResponse<IEnumerable<ExpenseResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllExpensesQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Handle(GetAllExpensesQuery request,
        CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<Expense>()
            .Include(x => x.User)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<ExpenseResponse>>(values);
        return new ApiResponse<IEnumerable<ExpenseResponse>>(mappedValues);
    }
}