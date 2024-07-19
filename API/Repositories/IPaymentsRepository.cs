using API.Dtos.Payment;
using API.Entities;

namespace API.Repositories
{
    public interface IPaymentsRepository
    {
        Task<Result<GetPaymentDto>> AddPaymentAsync(CreatePaymentIntentDto paymentIntent, string username);
        Task<Result<GetPaymentDto>> UpdatePaymentAsync(GetPaymentDto paymentDto);
        Task<Result<GetPaymentDto>> GetPaymentAsync(CreatePaymentIntentDto paymentIntent, string username);
        Task<Result<List<GetPaymentDto>>> GetUserPayments(string username);
    }
}