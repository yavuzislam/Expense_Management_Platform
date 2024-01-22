using AutoMapper;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.PaymentCqrs.PaymentCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.PaymentCqrs.PaymentHandlers.PaymentCommandHandlers;

public class DeletePaymentCommandHandler:IRequestHandler<DeletePaymentCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeletePaymentCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<Payment>()
            .FirstOrDefaultAsync(x => x.PaymentID == request.PaymentId, cancellationToken);
        if (entity is null)
            return new ApiResponse("Payment not found");
        if (!entity.IsActive)
            return new ApiResponse("Payment already deleted");
        entity.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Payment deleted successfully");
    }
}