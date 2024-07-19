namespace API.Entities;

public class Payment
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
    public string PurchaseType { get; set; }
    public int PurchaseId { get; set; }
}