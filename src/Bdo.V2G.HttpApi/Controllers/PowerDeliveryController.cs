using System.Threading.Tasks;
using Bdo.V2G.DTOs.PowerDelivery;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/power-delivery")]
[ApiController]
public class PowerDeliveryController : ControllerBase
{
    private readonly IPowerDeliveryService _powerDeliveryService;

    public PowerDeliveryController(IPowerDeliveryService powerDeliveryService)
    {
        _powerDeliveryService = powerDeliveryService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<PowerDeliveryResDto> DeliverAsync([FromBody] PowerDeliveryReqDto input)
    {
        return await _powerDeliveryService.DeliverPowerAsync(input);
    }
}