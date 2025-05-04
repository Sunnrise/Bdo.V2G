using System.Threading.Tasks;
using Bdo.V2G.DTOs.ServiceDiscovery;
using Volo.Abp.Application.Services;

namespace Bdo.V2G.Services;

public interface IServiceDiscoveryService :  IApplicationService
{
    Task<ServiceDiscoveryResDto> DiscoverServicesAsync(ServiceDiscoveryReqDto input);
}