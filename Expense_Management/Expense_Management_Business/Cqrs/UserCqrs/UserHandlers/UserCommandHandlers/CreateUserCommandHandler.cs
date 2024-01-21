using AutoMapper;
using Expense_Management_Base.Encryption;
using Expense_Management_Base.Response;
using Expense_Management_Business.Cqrs.UserCqrs.UserCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Users.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense_Management_Business.Cqrs.UserCqrs.UserHandlers.UserCommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(ExpenseManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUserByUsername = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Username == request.Model.Username, cancellationToken);
        if (existingUserByUsername != null)
            return new ApiResponse<UserResponse>($"Username {request.Model.Username} already exists.");

        var existingUserByIban = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.IBAN == request.Model.IBAN, cancellationToken);
        if (existingUserByIban != null)
            return new ApiResponse<UserResponse>($"IBAN {request.Model.IBAN} is already in use.");

        var existingUserByEmail = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == request.Model.Email, cancellationToken);
        if (existingUserByEmail != null)
            return new ApiResponse<UserResponse>($"Email {request.Model.Email} is already in use.");

        var entity = _mapper.Map<User>(request.Model);
        entity.UserNumber = new Random().Next(500, 1000);

        var hash = Md5Extension.GetHash(request.Model.Password.Trim());
        entity.Password = hash;

        var result = await _dbContext.Set<User>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var mappedResult = _mapper.Map<UserResponse>(result.Entity);
        return new ApiResponse<UserResponse>(mappedResult);
    }
}