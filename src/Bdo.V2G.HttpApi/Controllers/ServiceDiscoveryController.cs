using System.Threading.Tasks;
using Bdo.V2G.DTOs.ServiceDiscovery;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Controllers;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/service-discovery")]
[ApiController]
public class ServiceDiscoveryController : V2GController
{
    private readonly IServiceDiscoveryService _serviceDiscoveryService;

    public ServiceDiscoveryController(IServiceDiscoveryService serviceDiscoveryService)
    {
        _serviceDiscoveryService = serviceDiscoveryService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<ServiceDiscoveryResDto> DiscoverAsync([FromBody] ServiceDiscoveryReqDto input)
    {
        return await _serviceDiscoveryService.DiscoverServicesAsync(input);
    }
}