using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Payments.Responses;
using MediatR;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentCommandHandlers;

public class CreatePaymentCommandHandler:IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePaymentCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Payment>(request.Model);
        var result = await _dbContext.Set<Payment>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var mappedResult = _mapper.Map<PaymentResponse>(result.Entity);
        return new ApiResponse<PaymentResponse>(mappedResult);
    }
}