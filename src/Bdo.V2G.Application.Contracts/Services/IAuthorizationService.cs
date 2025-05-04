using System.Threading.Tasks;
using Bdo.V2G.DTOs.Authorization;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IAuthorizationService : IApplicationService
{
    Task<AuthorizationResDto> AuthorizeAsync(AuthorizationReqDto input);
}