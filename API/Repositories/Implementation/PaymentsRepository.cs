using API.Data;
using API.Dtos.Payment;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private CourseContext _context;
        private readonly UserManager<User> _userManager;
        public PaymentsRepository(
            CourseContext context,
            UserManager<User> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<List<GetPaymentDto>>> GetUserPayments(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var payments = await _context.Payments
                .Where(p => p.UserId == user.Id)
                .Select(p => MapToPaymentDto(p))
                .ToListAsync();

            return new Result<List<GetPaymentDto>> { IsSuccess = true, Data = payments };
        }

        public async Task<Result<GetPaymentDto>> AddPaymentAsync(CreatePaymentIntentDto paymentIntent, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var payment = MapToPayment(paymentIntent, user.Id);
            payment.Id = _context.Payments.Any() ? _context.Payments.Max(p => p.Id) + 1 : 1;
            await _context.Payments.AddAsync(payment);

            return await SaveChangesAndReturnResult(payment.Id);
        }

        public async Task<Result<GetPaymentDto>> GetPaymentAsync(CreatePaymentIntentDto paymentIntent, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var payment = await _context.Payments
                .Where(p => p.UserId == user.Id
                    && p.PurchaseType == paymentIntent.PurchaseType
                    && p.PurchaseId == paymentIntent.PurchaseId)
                .FirstOrDefaultAsync();

            if (payment == null) return new Result<GetPaymentDto> { IsSuccess = false };

            return new Result<GetPaymentDto> { IsSuccess = true, Data = MapToPaymentDto(payment) };
        }

        private Payment MapToPayment(CreatePaymentIntentDto paymentIntentDto, string userId)
        {
            return new Payment
            {
                UserId = userId,
                PurchaseType = paymentIntentDto.PurchaseType,
                PurchaseId = paymentIntentDto.PurchaseId,
            };
        }

        private async Task<Result<GetPaymentDto>> SaveChangesAndReturnResult(int id)
        {
            try
            {
                await _context.SaveChangesAsync();

                var dbPayment = await _context.Payments
                    .FirstOrDefaultAsync(p => p.Id == id);

                return new Result<GetPaymentDto> { IsSuccess = true, Data = MapToPaymentDto(dbPayment) };
            }
            catch (System.Exception ex)
            {
                return new Result<GetPaymentDto> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        private static GetPaymentDto MapToPaymentDto(Payment payment)
        {
            return new GetPaymentDto
            {
                Id = payment.Id,
                UserId = payment.UserId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus,
                PaymentIntentId = payment.PaymentIntentId,
                ClientSecret = payment.ClientSecret,
                PurchaseType = payment.PurchaseType,
                PurchaseId = payment.PurchaseId
            };
        }
    }
}