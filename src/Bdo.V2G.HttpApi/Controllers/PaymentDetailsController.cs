using System.Threading.Tasks;
using Bdo.V2G.DTOs.PaymentDetails;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/payment-details")]
[ApiController]
public class PaymentDetailsController : V2GController
{
    private readonly IPaymentDetailsService _paymentDetailsService;

    public PaymentDetailsController(IPaymentDetailsService paymentDetailsService)
    {
        _paymentDetailsService = paymentDetailsService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<PaymentDetailsResDto> SubmitAsync([FromBody] PaymentDetailsReqDto input)
    {
        return await _paymentDetailsService.SubmitPaymentDetailsAsync(input);
    }
}