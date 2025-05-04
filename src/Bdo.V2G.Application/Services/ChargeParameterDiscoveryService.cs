using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargeParameter;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class ChargeParameterDiscoveryService : ApplicationService, IChargeParameterDiscoveryService
{
    public Task<ChargeParameterDiscoveryResDto> DiscoverChargeParametersAsync(ChargeParameterDiscoveryReqDto input)
    {
        string chargeOptions = input.EVRequestedEnergyTransferType switch
        {
            "AC_single_phase" => "Single phase AC charging available",
            "AC_three_phase" => "Three phase AC charging available",
            "DC" => "DC fast charging available",
            _ => "Unknown charging option"
        };

        return Task.FromResult(new ChargeParameterDiscoveryResDto
        {
            ResponseCode = "OK",
            ChargeOptions = chargeOptions
        });
    }
}
