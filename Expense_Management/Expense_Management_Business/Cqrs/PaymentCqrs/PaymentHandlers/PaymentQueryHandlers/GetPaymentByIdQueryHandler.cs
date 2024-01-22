using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Payments.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentQueryHandlers;

public class GetPaymentByIdQueryHandler:IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentByIdQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var value = await _dbContext.Set<Payment>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.PaymentID == request.PaymentId, cancellationToken);

        if (value is null)
            return new ApiResponse<PaymentResponse>("Payment not found");

        var mappedValue = _mapper.Map<PaymentResponse>(value);
        return new ApiResponse<PaymentResponse>(mappedValue);
    }
}