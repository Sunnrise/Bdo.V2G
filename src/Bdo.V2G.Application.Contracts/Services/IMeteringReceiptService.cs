using System.Threading.Tasks;
using Bdo.V2G.DTOs.MeteringReceipt;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IMeteringReceiptService: IApplicationService
{
    Task<MeteringReceiptResDto> SubmitMeteringReceiptAsync(MeteringReceiptReqDto input);
}
