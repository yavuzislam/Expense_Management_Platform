using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Categories.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryHandlers.CategoryQueryHandlers;

public class GetCategoryByIdQueryHandler:IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<Category>()
            .Include(x => x.Expenses)
            .ThenInclude(x=>x.User)
            .FirstOrDefaultAsync(x => x.CategoryID == request.CategoryId, cancellationToken);

        if (value is null)
            return new ApiResponse<CategoryResponse>("Category not found");

        var mappedValue = _mapper.Map<CategoryResponse>(value);
        return new ApiResponse<CategoryResponse>(mappedValue);
    }
}