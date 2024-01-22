using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailCommands;
using Expense_Management_Business.Cqrs.EFTDetailCqrs.EFTDetailQueries;
using Expense_Management_Schema.EFTDetails.Requests;
using Expense_Management_Schema.EFTDetails.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFTDetailsController : ControllerBase
    {
        private readonly  IMediator _mediator;

        public EFTDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<EFTDetailResponse>>> GetEFTDetails()
        {
            var operation = new GetAllEFTDetailQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetEFTDetail")]
        public async Task<ApiResponse<EFTDetailResponse>> GetEFTDetail(int id)
        {
            var operation = new GetEFTDetailByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<EFTDetailResponse>> CreateEFTDetail([FromBody] EFTDetailRequest eftDetail)
        {
            var operation = new CreateEFTDetailCommand(eftDetail);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateEFTDetail(int id, [FromBody] EFTDetailRequest eftDetail)
        {
            var operation = new UpdateEFTDetailCommand(id, eftDetail);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> RemoveEFTDetail(int id)
        {
            var operation = new DeleteEFTDetailCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
