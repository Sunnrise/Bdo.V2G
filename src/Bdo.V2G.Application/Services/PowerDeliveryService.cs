using System.Threading.Tasks;
using Bdo.V2G.DTOs.PowerDelivery;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class PowerDeliveryService : ApplicationService, IPowerDeliveryService
{
    public Task<PowerDeliveryResDto> DeliverPowerAsync(PowerDeliveryReqDto input)
    {
        var chargingStarted = input.ChargeProgress == "Start";

        return Task.FromResult(new PowerDeliveryResDto
        {
            ResponseCode = chargingStarted ? "OK" : "STOPPED",
            ChargingStarted = chargingStarted
        });
    }
}
