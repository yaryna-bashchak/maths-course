using API.Dtos.Payment;
using API.Entities;

namespace API.Repositories
{
    public interface IPaymentsRepository
    {
        Task<Result<GetPaymentDto>> AddPaymentAsync(PaymentIntentDto paymentIntent, string username);
        Task<Result<GetPaymentDto>> GetPaymentAsync(PaymentIntentDto paymentIntent, string username);
        Task<Result<List<GetPaymentDto>>> GetUserPayments(string username);
    }
}