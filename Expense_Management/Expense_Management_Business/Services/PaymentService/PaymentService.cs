using Expense_Management_Base.Response;
using Expense_Management_Business.Services.ReportService;
using Expense_Management_Data.Context;
using Expense_Management_Data.Entities;
using Hangfire;

namespace Expense_Management_Business.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly ExpenseManagementDbContext _dbContext;
    private readonly IReportService _reportService;

    public PaymentService(ExpenseManagementDbContext dbContext, IReportService reportService)
    {
        _dbContext = dbContext;
        _reportService = reportService;
    }

    public async Task<ApiResponse> PerformEftAsync(int receiverUser, decimal amount, int senderUser,int categoryId, int status,DateTime date)
    {
        var user = await _dbContext.Set<User>().FindAsync(receiverUser);
        if (user is null)
            return new ApiResponse("User not found");
        var payment = new Payment
        {
            Amount = amount,
            Date = DateTime.Now,
            UserID = receiverUser,
            Status = status,
            Description = $"{user.UserFirstName} {user.UserLastName} Eft operation"
        };
        await _dbContext.Set<Payment>().AddAsync(payment);
        await _dbContext.SaveChangesAsync();


        var eftDetail = new EFTDetail
        {
            PaymentID = payment.PaymentID,
            BankName = "Akbank",
            IBAN = user.IBAN,
            TransactionReference = new Random().Next(1000000, 9999999).ToString(),
            TransactionDate = DateTime.UtcNow
        };

        await _dbContext.Set<EFTDetail>().AddAsync(eftDetail);
        user.Balance += amount;
        await _dbContext.SaveChangesAsync();

        BackgroundJob.Enqueue(
            () => _reportService.CreateReportAsync(senderUser, receiverUser, categoryId, amount, status, date));
    
        return new ApiResponse("Eft operation is successful");
    }
}