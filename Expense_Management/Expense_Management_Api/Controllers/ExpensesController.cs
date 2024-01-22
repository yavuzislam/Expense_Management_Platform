using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseCommands;
using Expense_Management_Business.Cqrs.ExpenseCqrs.ExpenseQueries;
using Expense_Management_Schema.Expenses.Requests;
using Expense_Management_Schema.Expenses.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetMyExpenses")]
        [Authorize(Roles = "Personel")]
        public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetMyExpenses(int? status)
        {
            var id = User.FindFirst("id")?.Value;
            var operation = new GetExpensesByUserIdQuery(int.Parse(id), status);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetExpenses()
        {
            var operation = new GetAllExpensesQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetExpense")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ExpenseResponse>> GetExpense(int id)
        {
            var operation = new GetExpenseByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        [Authorize(Roles = "Personel, Admin")]
        public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetExpenseByParameters([FromQuery] int status)
        {
            var operation = new GetExpenseByParametersQuery(status);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Personel")]
        public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest expense)
        {
            var id = User.FindFirst("id")?.Value;
            var operation = new CreateExpenseCommand(expense, int.Parse(id));
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost("approve/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> ApproveExpense(int id)
        {
            var userNumber = User.FindFirst("id")?.Value;
            var operation = new ApproveExpenseCommand(id, int.Parse(userNumber));
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost("reject/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> RejectExpense(int id, [FromBody] string reason)
        {
            var userNumber = User.FindFirst("id")?.Value;
            var operation = new RejectExpenseCommand(id, int.Parse(userNumber), reason);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Personel")]
        public async Task<ApiResponse> UpdateExpense(int id, [FromBody] ExpenseRequest expense)
        {
            var operation = new UpdateExpenseCommand(id, expense);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Personel")]
        public async Task<ApiResponse> RemoveExpense(int id)
        {
            var operation = new DeleteExpenseCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
