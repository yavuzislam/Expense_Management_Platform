using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationCommands;
using Expense_Management_Business.Cqrs.NotificationCqrs.NotificationQueries;
using Expense_Management_Schema.Notifications.Requests;
using Expense_Management_Schema.Notifications.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotifications()
        {
            var operation = new GetAllNotificationQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetNotification")]
        public async Task<ApiResponse<NotificationResponse>> GetNotification(int id)
        {
            var operation = new GetNotificationByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<NotificationResponse>> CreateNotification(
            [FromBody] NotificationRequest notification)
        {
            var operation = new CreateNotificationCommand(notification);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateNotification(int id, [FromBody] NotificationRequest notification)
        {
            var operation = new UpdateNotificationCommand(id, notification);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> RemoveNotification(int id)
        {
            var operation = new DeleteNotificationCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
