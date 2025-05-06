using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.PaymentDetails;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/payment-details")]
[ApiController]
public class PaymentDetailsController : V2GController
{
    private readonly IPaymentDetailsService _paymentDetailsService;
    private readonly ISessionManagerService _sessionManagerService;

    public PaymentDetailsController(
        IPaymentDetailsService paymentDetailsService,
        ISessionManagerService sessionManagerService
    )
    {
        _paymentDetailsService = paymentDetailsService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<PaymentDetailsResType> SubmitAsync(
        [FromBody] PaymentDetailsReqType input
    )
    {
        if (!ModelState.IsValid || input.ContractSignatureCertChain?.Certificate == null)
        {
            return new PaymentDetailsResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);

        if (!fsm.CanFire(SessionEventEnum.PaymentDetails))
        {
            return new PaymentDetailsResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        await fsm.FireAsync(SessionEventEnum.PaymentDetails);

        return new PaymentDetailsResType
        {
            ResponseCode = ResponseCodeType.Ok,
            GenChallenge = RandomNumberGenerator.GetBytes(16),
            EvseTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
    }
}