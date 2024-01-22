using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.TokenCqrs.TokenCommands;
using Expense_Management_Schema.Token.Requests;
using Expense_Management_Schema.Token.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
        {
            var operation = new CreateTokenCommand(request);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
