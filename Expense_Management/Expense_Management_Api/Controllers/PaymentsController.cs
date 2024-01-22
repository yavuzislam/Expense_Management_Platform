using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentQueries;
using Expense_Management_Schema.Payments.Requests;
using Expense_Management_Schema.Payments.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<PaymentResponse>>> GetPayments()
        {
            var operation = new GetAllPaymentsQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<PaymentResponse>> GetPayment(int id)
        {
            var operation = new GetPaymentByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<PaymentResponse>>> GetPaymentByParameters([FromQuery] int status)
        {
            var operation = new GetPaymentsByParametersQuery(status);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] PaymentRequest payment)
        {
            var operation = new CreatePaymentCommand(payment);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdatePayment(int id, [FromBody] PaymentRequest payment)
        {
            var operation = new UpdatePaymentCommand(id, payment);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> RemovePayment(int id)
        {
            var operation = new DeletePaymentCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}