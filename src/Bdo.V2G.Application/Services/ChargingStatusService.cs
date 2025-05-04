using System.Threading.Tasks;
using Bdo.V2G.DTOs.ChargingStatus;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class ChargingStatusService : ApplicationService, IChargingStatusService
{
    public Task<ChargingStatusResDto> ReportChargingStatusAsync(ChargingStatusReqDto input)
    {
        // Örnek mantık: Voltajı sabit 400V döndürüyoruz
        return Task.FromResult(new ChargingStatusResDto
        {
            ResponseCode = "OK",
            EVSEPresentVoltage = 400.0
        });
    }
}