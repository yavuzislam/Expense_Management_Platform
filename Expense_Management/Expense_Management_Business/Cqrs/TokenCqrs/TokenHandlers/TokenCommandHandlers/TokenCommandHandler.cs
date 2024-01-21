using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Expense_Management_Base.Encryption;
using Expense_Management_Base.Enums;
using Expense_Management_Base.Response;
using Expense_Management_Base.Token;
using Expense_Management_Business.Cqrs.TokenCqrs.TokenCommands;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Token.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Expense_Management_Business.Cqrs.TokenCqrs.TokenHandlers.TokenCommandHandlers;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly JwtConfig _jwtConfig;

    public TokenCommandHandler(ExpenseManagementDbContext dbContext, JwtConfig jwtConfig)
    {
        _dbContext = dbContext;
        _jwtConfig = jwtConfig;
    }

    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Username == request.Model.UserName, cancellationToken);
        if (user == null)
            return new ApiResponse<TokenResponse>("Invalid user information");

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        if (hash != user.Password)
        {
            user.LastActivityDate = DateTime.UtcNow;
            user.PasswordRetryCount++;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        if (user.Status != 1)
            return new ApiResponse<TokenResponse>("Invalid user status");

        if (user.PasswordRetryCount > 3)
            return new ApiResponse<TokenResponse>("Invalid user status");

        user.LastActivityDate = DateTime.UtcNow;
        user.PasswordRetryCount = 0;
        await _dbContext.SaveChangesAsync(cancellationToken);

        string token = Token(user);

        return new ApiResponse<TokenResponse>(new TokenResponse()
        {
            Email = user.Email,
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration)
        });
    }

    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.UserNumber.ToString()),
            new Claim("Email", user.Email),
            new Claim("UserName", user.Username),
            new Claim(ClaimTypes.Role, (RoleTypes)user.Role == RoleTypes.Admin ? "Admin" : "Personel")
        };
        return claims;
    }
}