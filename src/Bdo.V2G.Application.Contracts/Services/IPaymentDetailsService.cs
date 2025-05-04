using System.Threading.Tasks;
using Bdo.V2G.DTOs.PaymentDetails;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IPaymentDetailsService : IApplicationService
{
    Task<PaymentDetailsResDto> SubmitPaymentDetailsAsync(PaymentDetailsReqDto input);
}