using API.Data;
using API.Dtos.Payment;
using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace API.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly PaymentService _paymentService;
    private readonly CourseContext _context;
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IConfiguration _config;
    private readonly ISectionsRepository _sectionsRepository;

    public PaymentsController(PaymentService paymentService, CourseContext context, IPaymentsRepository paymentsRepository, IConfiguration config, ISectionsRepository sectionsRepository)
    {
        _context = context;
        _paymentService = paymentService;
        _paymentsRepository = paymentsRepository;
        _config = config;
        _sectionsRepository = sectionsRepository;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GetPaymentDto>> CreateOrUpdatePaymentIntent(CreatePaymentIntentDto paymentIntent)
    {
        var username = User.Identity.Name;
        var existingResult = await _paymentsRepository.GetPaymentAsync(paymentIntent, username);
        GetPaymentDto payment;

        if (existingResult.IsSuccess)
        {
            payment = existingResult.Data;
            if (payment.PaymentStatus == PaymentStatus.PaymentReceived) return BadRequest(new ProblemDetails { Title = "This payment is already received" });
        }
        else
        {
            var addingResult = await _paymentsRepository.AddPaymentAsync(paymentIntent, username);

            if (!addingResult.IsSuccess)
            {
                return BadRequest(addingResult.ErrorMessage);
            }

            payment = addingResult.Data;
        }

        var intent = await _paymentService.CreateOrUpdatePaymentIntent(payment);

        if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

        payment.Amount = payment.Amount != 0 ? payment.Amount : intent.Amount / 100;
        payment.PaymentIntentId ??= intent.Id;
        payment.ClientSecret ??= intent.ClientSecret;

        var result = await _paymentsRepository.UpdatePaymentAsync(payment);

        if (!result.IsSuccess)
        {
            return BadRequest(new ProblemDetails { Title = "Problem updating payment with intent" });
        }

        return result.Data;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<GetPaymentDto>>> GetUserPayments()
    {
        var username = User.Identity.Name;
        var result = await _paymentsRepository.GetUserPayments(username);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return result.Data;
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
            _config["StripeSettings:WhSecret"]);

        var charge = (Charge)stripeEvent.Data.Object;

        var payment = await _context.Payments.FirstOrDefaultAsync(x =>
            x.PaymentIntentId == charge.PaymentIntentId);

        if (charge.Status == "succeeded")
        {
            payment.PaymentStatus = PaymentStatus.PaymentReceived;

            if (payment.PurchaseType == "Course")
            {
                await _sectionsRepository.MakeCourseAvailable(payment.PurchaseId, payment.UserId);
            }
            else if (payment.PurchaseType == "Section")
            {
                await _sectionsRepository.MakeSectionAvailable(payment.PurchaseId, payment.UserId);
            }
        }

        await _context.SaveChangesAsync();

        return new EmptyResult();
    }
}