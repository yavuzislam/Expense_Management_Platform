using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserCommands;
using Expense_Management_Business.Cqrs.UserCqrs.UserQueries;
using Expense_Management_Schema.Users.Requests;
using Expense_Management_Schema.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        private readonly IDistributedCache _distributedCache;

        // GET: api/Users
        public UsersController(IMediator mediator, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet("MyProfile")]
        [Authorize(Roles = "Personel,Admin")]
        public async Task<ApiResponse<UserResponse>> GetMyProfile()
        {
            string id = User.FindFirst("id")?.Value;
            var operation = new GetUserByIdQuery(int.Parse(id));
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<UserResponse>>> GetUsers()
        {
            var operation = new GetAllUsersQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> GetUser(int id)
        {
            var operation = new GetUserByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }


        [HttpDelete("DeleteCashe/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> DeleteCache(int id)
        {
            _memoryCache.Remove(id);
            await _distributedCache.RemoveAsync(id.ToString());
            return new ApiResponse();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<UserResponse>>> GetUserByParameters([FromQuery] string? name,
            [FromQuery] string? surname, [FromQuery] string? username, [FromQuery] int? role)
        {
            var operation = new GetUserByParametersQuery(name, surname, username, role);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> CreateUser([FromBody] UserRequest user)
        {
            var operation = new CreateUserCommand(user);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateUser(int id, [FromBody] UserRequest user)
        {
            var operation = new UpdateUserCommand(id, user);
            var result = await _mediator.Send(operation);
            return result;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> RemoveUser(int id)
        {
            var operation = new DeleteUserCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}