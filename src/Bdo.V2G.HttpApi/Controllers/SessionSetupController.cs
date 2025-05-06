using System;
using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charging-session")]
[ApiController]
public class SessionSetupController : V2GController
{
    private readonly IChargingSessionService _chargingSessionService;
    private readonly ISessionManagerService _sessionManagerService;

    public SessionSetupController(
        IChargingSessionService chargingSessionService,
        ISessionManagerService sessionManagerService
    )
    {
        _chargingSessionService = chargingSessionService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost("setup")]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<SessionSetupResType> SetupAsync(
        [FromBody] SessionSetupReqType input
    )
    {
        // // Gelen SessionID üzerinden FSM alınıyor veya yaratılıyor
        // var fsm = _sessionManagerService.GetOrCreateSession(input.SessionID);
        //
        // // Eğer FSM şu anda SessionSetup event'ini kabul etmiyorsa hata veriyoruz
        // if (!fsm.CanFire(SessionEventEnum.SessionSetup))
        // {
        //     throw new UserFriendlyException("Sequence Error: Session Setup not allowed in the current state.");
        // }
        //
        // // FSM'de geçiş yapıyoruz
        // await fsm.FireAsync(SessionEventEnum.SessionSetup);
        //
        // return await _chargingSessionService.SetupSessionAsync(input);

        // EXI mesajdan gelen EVCCID base64 yazmak istersen örnek:

        // var fsm = _sessionManagerService.GetOrCreateSession(BitConverter.ToString(input.Evccid));
        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);

        if (!fsm.CanFire(SessionEventEnum.SessionSetup))
        {
            return new SessionSetupResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        await fsm.FireAsync(SessionEventEnum.SessionSetup);

        return new SessionSetupResType
        {
            ResponseCode = ResponseCodeType.Ok,
            Evseid = "TR*XYZ*EVSE001",
            EvseTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            EvseTimeStampSpecified = true
        };
    }
}