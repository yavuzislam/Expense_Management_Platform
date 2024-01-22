using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentQueries;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Payments.Responses;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentQueryHandlers;

public class
    GetPaymentsByParametersQueryHandler : IRequestHandler<GetPaymentsByParametersQuery,
    ApiResponse<IEnumerable<PaymentResponse>>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentsByParametersQueryHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<PaymentResponse>>> Handle(GetPaymentsByParametersQuery request,
        CancellationToken cancellationToken)
    {

        var list = await _dbContext.Set<Payment>()
            .Include(x => x.User)
            .Where(x => x.Status == request.Status)
            .ToListAsync(cancellationToken);

        var mappedList = _mapper.Map<IEnumerable<PaymentResponse>>(list);
        return new ApiResponse<IEnumerable<PaymentResponse>>(mappedList);
    }
}