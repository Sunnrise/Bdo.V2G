using System.Threading.Tasks;
using Bdo.V2G.DTOs;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Controllers;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charging-session")]
[ApiController]
public class SessionController : V2GController
{
    private readonly IChargingSessionService _chargingSessionService;
    private readonly ISessionManagerService _sessionManagerService;

    public SessionController(IChargingSessionService chargingSessionService,
        ISessionManagerService sessionManagerService
    )
    {
        _chargingSessionService = chargingSessionService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost("setup")]
    [Consumes("application/xml", "application/json")]
    public async Task<SessionSetupResDto> SetupAsync([FromBody] SessionSetupReqDto input)
    {
        // Gelen SessionID üzerinden FSM alınıyor veya yaratılıyor
        var fsm = _sessionManagerService.GetOrCreateSession(input.SessionID);

        // Eğer FSM şu anda SessionSetup event'ini kabul etmiyorsa hata veriyoruz
        if (!fsm.CanFire(SessionEventEnum.SessionSetup))
        {
            throw new UserFriendlyException("Sequence Error: Session Setup not allowed in the current state.");
        }

        // FSM'de geçiş yapıyoruz
        await fsm.FireAsync(SessionEventEnum.SessionSetup);

        return await _chargingSessionService.SetupSessionAsync(input);
    }
}