using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Categories.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryHandlers.CategoryCommandHandlers;

public class CreateCategoryCommandHandler:IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (await _dbContext.Set<Category>().AnyAsync(x => x.Name == request.Model.Name, cancellationToken))
            return new ApiResponse<CategoryResponse>("Category already exists");

        var category = _mapper.Map<Category>(request.Model);
        var result = await _dbContext.Set<Category>().AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var mappedResult = _mapper.Map<CategoryResponse>(result.Entity);
        return new ApiResponse<CategoryResponse>(mappedResult);
    }
}