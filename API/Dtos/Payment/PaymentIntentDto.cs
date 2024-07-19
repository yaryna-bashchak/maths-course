namespace API.Dtos.Payment;

public class CreatePaymentIntentDto
{
    public string PurchaseType { get; set; }
    public int PurchaseId { get; set; }
}