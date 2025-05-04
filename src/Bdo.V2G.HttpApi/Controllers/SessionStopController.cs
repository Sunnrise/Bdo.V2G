using System.Threading.Tasks;
using Bdo.V2G.DTOs.SessionStop;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/session-stop")]
[ApiController]
public class SessionStopController : V2GController
{
    private readonly ISessionStopService _sessionStopService;

    public SessionStopController(ISessionStopService sessionStopService)
    {
        _sessionStopService = sessionStopService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<SessionStopResDto> StopAsync([FromBody] SessionStopReqDto input)
    {
        return await _sessionStopService.StopSessionAsync(input);
    }
}