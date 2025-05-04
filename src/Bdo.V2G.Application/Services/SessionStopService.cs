using System.Threading.Tasks;
using Bdo.V2G.DTOs.SessionStop;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class SessionStopService : ApplicationService, ISessionStopService
{
public Task<SessionStopResDto> StopSessionAsync(SessionStopReqDto input)
{
    return Task.FromResult(new SessionStopResDto
    {
        ResponseCode = "OK",
        SessionEnded = true
    });
}
}
