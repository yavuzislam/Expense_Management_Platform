using Expense_Management_Base.Response;
using Expense_Management_Schema.Categories.Requests;
using Expense_Management_Schema.Categories.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;

public record CreateCategoryCommand(CategoryRequest Model) : IRequest<ApiResponse<CategoryResponse>>;