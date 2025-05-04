using System.Threading.Tasks;
using Bdo.V2G.DTOs.Authorization;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Controllers;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/authorization-service")]
[ApiController]
public class AuthorizationController : V2GController
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<AuthorizationResDto> AuthorizeAsync([FromBody] AuthorizationReqDto input)
    {
        return await _authorizationService.AuthorizeAsync(input);
    }
}