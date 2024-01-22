using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryCommands;
using Expense_Management_Business.Cqrs.CategoryCqrs.CategoryQueries;
using Expense_Management_Schema.Categories.Requests;
using Expense_Management_Schema.Categories.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly  IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CategoryResponse>>> GetCategorys()
        {
            var operation = new GetAllCategoriesQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ApiResponse<CategoryResponse>> GetCategory(int id)
        {
            var operation = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CategoryResponse>> CreateCategory([FromBody] CategoryRequest Category)
        {
            var operation = new CreateCategoryCommand(Category);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateCategory(int id, [FromBody] CategoryRequest Category)
        {
            var operation = new UpdateCategoryCommand(id, Category);
            var result = await _mediator.Send(operation);
            return result;
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse> RemoveCategory(int id)
        {
            var operation = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
