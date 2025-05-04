using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargingStatus;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charging-status")]
[ApiController]
public class ChargingStatusController : V2GController
{
    private readonly IChargingStatusService _chargingStatusService;

    public ChargingStatusController(IChargingStatusService chargingStatusService)
    {
        _chargingStatusService = chargingStatusService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<ChargingStatusResDto> ReportAsync([FromBody] ChargingStatusReqDto input)
    {
        return await _chargingStatusService.ReportChargingStatusAsync(input);
    }
}