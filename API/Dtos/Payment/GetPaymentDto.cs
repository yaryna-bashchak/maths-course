using API.Entities;

namespace API.Dtos.Payment;

public class GetPaymentDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
    public string PurchaseType { get; set; }
    public int PurchaseId { get; set; }
}