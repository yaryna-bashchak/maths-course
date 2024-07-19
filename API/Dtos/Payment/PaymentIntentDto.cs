namespace API.Dtos.Payment;

public class PaymentIntentDto
{
    public string PaymentIntentId { get; set; }
    public string PurchaseType { get; set; }
    public int PurchaseId { get; set; }
}