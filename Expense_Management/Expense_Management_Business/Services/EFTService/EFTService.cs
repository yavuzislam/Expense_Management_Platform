using Expense_Management_Base.Response;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;

namespace Expense_Management_Business.Services.EFTService;

public class EFTService : IEFTService
{
    private readonly ExpenseManagementDbContext _dbContext;

    public EFTService(ExpenseManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse> PerformEftAsync(decimal amount ,string bankName, string iban, int paymentId)
    {
        var eftDetail = new EFTDetail
        {
            PaymentID = paymentId,
            BankName = bankName,
            IBAN = iban,
            TransactionReference = new Random().Next(1000000, 9999999).ToString(),
            TransactionDate = DateTime.UtcNow
        };

        await _dbContext.Set<EFTDetail>().AddAsync(eftDetail);
        await _dbContext.SaveChangesAsync();

        return new ApiResponse("Eft operation is successful");
    }

}