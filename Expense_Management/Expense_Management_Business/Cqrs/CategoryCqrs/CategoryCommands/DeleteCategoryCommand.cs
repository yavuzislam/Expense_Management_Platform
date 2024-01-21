using Expense_Management_Base.Response;
using MediatR;

namespace Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;

public record DeleteCategoryCommand(int CategoryId) : IRequest<ApiResponse>;