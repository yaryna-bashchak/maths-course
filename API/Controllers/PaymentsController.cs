using API.Data;
using API.Dtos.Payment;
using API.Entities;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly PaymentService _paymentService;
    private readonly CourseContext _context;
    private readonly IPaymentsRepository _paymentsRepository;

    public PaymentsController(PaymentService paymentService, CourseContext context, IPaymentsRepository paymentsRepository)
    {
        _context = context;
        _paymentService = paymentService;
        _paymentsRepository = paymentsRepository;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GetPaymentDto>> CreateOrUpdatePaymentIntent(PaymentIntentDto paymentIntent)
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

        await _context.SaveChangesAsync();
        return payment;
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
}