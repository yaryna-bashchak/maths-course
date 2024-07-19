using API.Data;
using API.Dtos.Payment;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace API.Services;

public class PaymentService
{
    public readonly IConfiguration _config;
    public readonly CourseContext _context;
    public PaymentService(IConfiguration config, CourseContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(GetPaymentDto paymentIntent)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        var service = new PaymentIntentService();

        var intent = new PaymentIntent();
        var total = 0;

        if (paymentIntent.PurchaseType == "Course")
        {
            var course = await _context.Courses.FindAsync(paymentIntent.PurchaseId);

            if (course == null)
                throw new KeyNotFoundException($"Course with ID {paymentIntent.PurchaseId} not found.");

            total = course.PriceFull * 100;

        }
        else if (paymentIntent.PurchaseType == "Section")
        {
            var course = await _context.Courses
                .Where(c => c.Sections.Any(s => s.Id == paymentIntent.PurchaseId))
                .FirstOrDefaultAsync();

            if (course == null)
                throw new KeyNotFoundException($"Course with SectionId {paymentIntent.PurchaseId} not found.");

            total = course.PriceMonthly * 100;
        }

        if (string.IsNullOrEmpty(paymentIntent.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = total,
                Currency = "uah",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = total,
            };
            await service.UpdateAsync(paymentIntent.PaymentIntentId, options);
        }

        return intent;
    }
}