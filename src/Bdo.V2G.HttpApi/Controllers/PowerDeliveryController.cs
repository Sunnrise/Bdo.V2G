using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.PowerDelivery;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/power-delivery")]
[ApiController]
public class PowerDeliveryController : ControllerBase
{
    private readonly IPowerDeliveryService _powerDeliveryService;
    private readonly ISessionManagerService _sessionManagerService;

    public PowerDeliveryController(IPowerDeliveryService powerDeliveryService,
        ISessionManagerService sessionManagerService
    )
    {
        _powerDeliveryService = powerDeliveryService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<PowerDeliveryResType> DeliverAsync([FromBody] PowerDeliveryReqType input)
    {
        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);

        if (!fsm.CanFire(SessionEventEnum.PowerDeliveryStart))
        {
            return new PowerDeliveryResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        await fsm.FireAsync(SessionEventEnum.PowerDeliveryStart);

        return new PowerDeliveryResType
        {
            ResponseCode = ResponseCodeType.Ok
        };
    }
}