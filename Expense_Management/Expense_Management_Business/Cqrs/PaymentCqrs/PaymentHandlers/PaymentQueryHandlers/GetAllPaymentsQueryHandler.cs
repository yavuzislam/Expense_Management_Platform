using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Payments.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentQueryHandlers;

public class GetAllPaymentsQueryHandler: IRequestHandler<GetAllPaymentsQuery, ApiResponse<IEnumerable<PaymentResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllPaymentsQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<PaymentResponse>>> Handle(GetAllPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var values = await _dbContext.Set<Payment>()
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        var mappedValues = _mapper.Map<IEnumerable<PaymentResponse>>(values);
        return new ApiResponse<IEnumerable<PaymentResponse>>(mappedValues);
    }
}