using System.Threading.Tasks;
using Bdo.V2G.DTOs.MeteringReceipt;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class MeteringReceiptService : ApplicationService, IMeteringReceiptService
{
    public Task<MeteringReceiptResDto> SubmitMeteringReceiptAsync(MeteringReceiptReqDto input)
    {
        return Task.FromResult(new MeteringReceiptResDto
        {
            ResponseCode = "OK",
            ReceiptConfirmed = true
        });
    }
}