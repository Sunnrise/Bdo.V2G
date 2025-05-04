using System.Threading.Tasks;
using Bdo.V2G.DTOs.Authorization;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class AuthorizationService : ApplicationService, IAuthorizationService
{
    public Task<AuthorizationResDto> AuthorizeAsync(AuthorizationReqDto input)
    {
        // Burada örnek bir yetkilendirme kontrolü yapıyoruz.
        var isAuthorized = !string.IsNullOrEmpty(input.IdToken) && input.IdToken.StartsWith("ABC");

        return Task.FromResult(new AuthorizationResDto
        {
            ResponseCode = isAuthorized ? "OK" : "FAILED",
            IsAuthorized = isAuthorized
        });
    }
}