using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.ChargingStatus;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charging-status")]
[ApiController]
public class ChargingStatusController : V2GController
{
    private readonly IChargingStatusService _chargingStatusService;
    private readonly ISessionManagerService _sessionManagerService;

    public ChargingStatusController(
        IChargingStatusService chargingStatusService,
        ISessionManagerService sessionManagerService)
    {
        _chargingStatusService = chargingStatusService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<ChargingStatusResType> ReportAsync([FromBody] ChargingStatusReqType input)
    {
        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);

        if (!fsm.CanFire(SessionEventEnum.ChargingStatus))
        {
            return new ChargingStatusResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        await fsm.FireAsync(SessionEventEnum.ChargingStatus);

        // Call your business logic here if needed
        // var result = await _chargingStatusService.ReportChargingStatusAsync(input);

        var response = new ChargingStatusResType
        {
            ResponseCode = ResponseCodeType.Ok,

            // Zorunlu: 7 ile 37 karakter arasında olmalı
            Evseid = "DE*BDO*E12345678",

            // Zorunlu: 1-255 arası byte
            SaScheduleTupleId = 1,

            // Opsiyonel ancak kullanılabilir
            EvseMaxCurrent = new PhysicalValueType
            {
                Value = 32,
                Multiplier = 0,
                Unit = UnitSymbolType.A
            },

            // Zorunlu
            AcEvseStatus = new AcEvseStatusType
            {
                Rcd = true,
                EvseNotification = EvseNotificationType.None,
                NotificationMaxDelay = 0
            }
        };

        return response;
    }
}