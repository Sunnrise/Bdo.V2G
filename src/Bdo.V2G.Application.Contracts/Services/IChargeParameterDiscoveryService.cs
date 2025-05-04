using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargeParameter;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IChargeParameterDiscoveryService : IApplicationService
{
    Task<ChargeParameterDiscoveryResDto> DiscoverChargeParametersAsync(
        ChargeParameterDiscoveryReqDto input
    );
}