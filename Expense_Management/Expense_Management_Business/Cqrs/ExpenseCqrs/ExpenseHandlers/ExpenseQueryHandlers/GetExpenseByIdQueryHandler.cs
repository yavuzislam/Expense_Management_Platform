using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseHandlers.ExpenseQueryHandlers;

public class GetExpenseByIdQueryHandler:IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpenseByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<Expense>()
            .Include(x => x.User)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.ExpenseNumber == request.ExpenseNumber, cancellationToken);

        if (value is null)
            return new ApiResponse<ExpenseResponse>("Expense not found");

        var mappedValue = _mapper.Map<ExpenseResponse>(value);
        return new ApiResponse<ExpenseResponse>(mappedValue);
    }
}