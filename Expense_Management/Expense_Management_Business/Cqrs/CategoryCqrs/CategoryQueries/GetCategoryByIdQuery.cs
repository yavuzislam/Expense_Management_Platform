using Expense_Management_Base.Response;
using Expense_Management_Schema.Categories.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryQueries;

public record GetCategoryByIdQuery(int CategoryId) : IRequest<ApiResponse<CategoryResponse>>;