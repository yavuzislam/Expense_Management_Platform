using Expense_Management_Base.Response;
using Expense_Management_Schema.Categories.Requests;
using MediatR;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;

public record UpdateCategoryCommand(int CategoryId, CategoryRequest Model) : IRequest<ApiResponse>;