using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Categories.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryHandlers.CategoryQueryHandlers;

public class GetAllCategoriesQueryHandlers:IRequestHandler<GetAllCategoriesQuery, ApiResponse<IEnumerable<CategoryResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandlers(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<Category>()
            .Include(x => x.Expenses)
            .ThenInclude(x=>x.User)
            .ToListAsync(cancellationToken);
        var mappedValues = _mapper.Map<IEnumerable<CategoryResponse>>(values);
        return new ApiResponse<IEnumerable<CategoryResponse>>(mappedValues);
    }
}