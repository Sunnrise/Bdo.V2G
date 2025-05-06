using System.Threading.Tasks;
using Bdo.V2G.DTOs;
using Bdo.V2G.DTOs.SessionSetup;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IChargingSessionService : IApplicationService
{
    Task<SessionSetupResDto> SetupSessionAsync(
        SessionSetupReqDto input
    );
}