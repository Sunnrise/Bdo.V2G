using System.Threading.Tasks;
using Bdo.V2G.DTOs.PaymentDetails;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class PaymentDetailsService : ApplicationService, IPaymentDetailsService
{
    public Task<PaymentDetailsResDto> SubmitPaymentDetailsAsync(PaymentDetailsReqDto input)
    {
        var paymentAccepted = input.PaymentOption == "Contract"; // Basit örnek
        return Task.FromResult(new PaymentDetailsResDto
        {
            ResponseCode = paymentAccepted ? "OK" : "FAILED",
            PaymentAccepted = paymentAccepted
        });
    }
}
