using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargeParameter;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charge-parameter/discovery")]
[ApiController]
public class ChargeParameterDiscoveryController : V2GController
{
    private readonly IChargeParameterDiscoveryService _chargeParameterDiscoveryService;

    public ChargeParameterDiscoveryController(IChargeParameterDiscoveryService chargeParameterDiscoveryService)
    {
        _chargeParameterDiscoveryService = chargeParameterDiscoveryService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<ChargeParameterDiscoveryResDto> DiscoverAsync([FromBody] ChargeParameterDiscoveryReqDto input)
    {
        return await _chargeParameterDiscoveryService.DiscoverChargeParametersAsync(input);
    }
}