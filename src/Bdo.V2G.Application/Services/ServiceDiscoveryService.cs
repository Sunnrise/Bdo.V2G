using System.Collections.Generic;
using System.Threading.Tasks;
using Bdo.V2G.DTOs.ServiceDiscovery;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public class ServiceDiscoveryService : ApplicationService, IServiceDiscoveryService
{
    public Task<ServiceDiscoveryResDto> DiscoverServicesAsync(
        ServiceDiscoveryReqDto input
    )
    {
        return Task.FromResult(new ServiceDiscoveryResDto
        {
            ResponseCode = "OK",
            SupportedServices = new List<string> { "AC Charging", "DC Charging", "Plug&Charge" }
        });
    }
}