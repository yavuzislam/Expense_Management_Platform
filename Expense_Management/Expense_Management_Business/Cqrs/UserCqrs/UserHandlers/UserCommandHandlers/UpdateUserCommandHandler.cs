using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserCommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse>
{
    private readonly ExpenseManagementDbContext _dbContext;

    public UpdateUserCommandHandler(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.UserNumber == request.UserId, cancellationToken);
        if (entity is null)
            return new ApiResponse("User not found");

        if (await _dbContext.Set<User>()
                .AnyAsync(x => x.Username == request.Model.Username && x.UserNumber != request.UserId,
                    cancellationToken))
            return new ApiResponse("Username already exists");
        if (await _dbContext.Set<User>().AnyAsync(x => x.Email == request.Model.Email && x.UserNumber != request.UserId,
                cancellationToken))
            return new ApiResponse("Email already exists");

        entity.Username = request.Model.Username;
        entity.Password = request.Model.Password;
        entity.Email = request.Model.Email;
        entity.Role = request.Model.Role;
        entity.Email = request.Model.Email;
        entity.UserFirstName = request.Model.UserFirstName;
        entity.UserLastName = request.Model.UserLastName;
        entity.IBAN = request.Model.IBAN == null ? entity.IBAN : request.Model.IBAN;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}