using System.Threading.Tasks;
using Bdo.V2G.DTOs.PowerDelivery;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IPowerDeliveryService : IApplicationService
{
    Task<PowerDeliveryResDto> DeliverPowerAsync(PowerDeliveryReqDto input);
}