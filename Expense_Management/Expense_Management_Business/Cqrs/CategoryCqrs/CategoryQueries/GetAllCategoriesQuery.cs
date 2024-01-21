using Expense_Management_Base.Response;
using Expense_Management_Schema.Categories.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryQueries;

public record GetAllCategoriesQuery() : IRequest<ApiResponse<IEnumerable<CategoryResponse>>>;