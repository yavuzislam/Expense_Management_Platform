using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentCommandHandlers;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdatePaymentCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Payment>().FindAsync(request.PaymentId, cancellationToken);
        if (entity is null)
            return new ApiResponse("Payment not found");

        entity.Amount = request.Model.Amount;
        entity.Status = request.Model.Status;
        entity.Description = request.Model.Description;
        entity.Date = request.Model.Date;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}