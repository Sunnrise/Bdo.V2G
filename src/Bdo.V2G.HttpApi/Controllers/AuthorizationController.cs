using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.Authorization;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Controllers;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/authorization-service")]
[ApiController]
public class AuthorizationController : V2GController
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ISessionManagerService _sessionManagerService;

    public AuthorizationController(IAuthorizationService authorizationService,
        ISessionManagerService sessionManagerService
    )
    {
        _authorizationService = authorizationService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<AuthorizationResType> AuthorizeAsync([FromBody] AuthorizationReqType  input)
    {
        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);

        // State kontrolü: Authorization Event’i uygun state’te mi?
        if (!fsm.CanFire(SessionEventEnum.Authorization))
        {
            return new AuthorizationResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError,
                EvseProcessing = EvseProcessingType.Finished
            };
        }

        await fsm.FireAsync(SessionEventEnum.Authorization);

        // Başarılı yanıt
        return new AuthorizationResType
        {
            ResponseCode = ResponseCodeType.Ok,
            EvseProcessing = EvseProcessingType.Finished
        };
    }
}