using System.Threading.Tasks;
using Bdo.V2G.DTOs;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class ChargingSessionService : ApplicationService, IChargingSessionService
{
    public Task<SessionSetupResDto> SetupSessionAsync(
        SessionSetupReqDto input
    )
    {
        if (string.IsNullOrWhiteSpace(input.EVCCID))
            throw new AbpException("EVCCID boş olamaz!");

        return Task.FromResult(new SessionSetupResDto
        {
            ResponseCode = "OK",
            SessionID = input.SessionID,
            EVSEID = input.EVSEID,
        });
    }
}