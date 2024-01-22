using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportCommands;
using Expense_Management_Business.Cqrs.ReportCqrs.ReportQueries;
using Expense_Management_Schema.Reports.Requests;
using Expense_Management_Schema.Reports.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<ReportResponse>>> GetReports()
        {
            var operation = new GetAllReportsQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}", Name = "GetReport")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ReportResponse>> GetReport(int id)
        {
            var operation = new GetReportByIdQuery(id);
            var result = await _mediator.Send(operation);
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Personel")]
        public async Task<ApiResponse<ReportResponse>> CreateReport([FromBody] ReportRequest report)
        {
            var id = User.FindFirst("id")?.Value;
            var operation = new CreateReportCommand(report, int.Parse(id));
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateReport(int id, [FromBody] ReportRequest report)
        {
            var operation = new UpdateReportCommand(id, report);
            var result = await _mediator.Send(operation);
            return result;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> RemoveReport(int id)
        {
            var operation = new DeleteReportCommand(id);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("ExpenseReports")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<ExpenseReportResponse>>> GetExpenseReports(string period)
        {
            var operation = new GetExpenseReportQuery(period);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("ExpenseReportsByDateRange")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<ExpenseReportResponse>>> GetExpenseReportsByDateRange(
            DateTime startDate, DateTime endDate)
        {
            var operation = new GetExpenseReportByDateRangeQuery(startDate, endDate);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("PersonalExpenseReports")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<PersonalExpenseReportResponse>>> GetPersonalExpenseReports(
            string period)
        {
            var operation = new GetPersonalExpenseReportQuery(period);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("PersonalExpenseReportsByDateRange")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<PersonalExpenseReportResponse>>> GetPersonalExpenseReportsByDateRange(
            DateTime startDate, DateTime endDate)
        {
            var operation = new GetPersonalExpenseReportByDateRangeQuery(startDate, endDate);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("PaymentReports")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<IEnumerable<PaymentReportResponse>>> GetPaymentReports(string period)
        {
            var operation = new GetPaymentReportQuery(period);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("PaymentReportsByDateRange")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<PaymentReportResponse>> GetPaymentReportsByDateRange(DateTime startDate,
            DateTime endDate)
        {
            var operation = new GetCustomPaymentReportQuery(startDate, endDate);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}