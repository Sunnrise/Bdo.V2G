using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargingStatus;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IChargingStatusService : IApplicationService
{
    Task<ChargingStatusResDto> ReportChargingStatusAsync(ChargingStatusReqDto input);
}
