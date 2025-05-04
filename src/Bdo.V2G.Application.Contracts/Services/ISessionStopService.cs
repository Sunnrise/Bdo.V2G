using System.Threading.Tasks;
using Bdo.V2G.DTOs.SessionStop;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface ISessionStopService : IApplicationService
{
    Task<SessionStopResDto> StopSessionAsync(SessionStopReqDto input);
}