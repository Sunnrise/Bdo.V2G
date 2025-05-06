using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.ServiceDiscovery;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Controllers;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/service-discovery")]
[ApiController]
public class ServiceDiscoveryController : V2GController
{
    private readonly IServiceDiscoveryService _serviceDiscoveryService;
    private readonly ISessionManagerService _sessionManagerService;

    public ServiceDiscoveryController(IServiceDiscoveryService serviceDiscoveryService,
        ISessionManagerService sessionManagerService
    )
    {
        _serviceDiscoveryService = serviceDiscoveryService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<ServiceDiscoveryResType> DiscoverAsync([FromBody] ServiceDiscoveryReqType input)
    {
        var sessionId = SessionConstants.SessionId;
        
        var fsm = _sessionManagerService.GetOrCreateSession(sessionId);

        if (!fsm.CanFire(SessionEventEnum.ServiceDiscovery))
        {
            return new ServiceDiscoveryResType
            {
                ResponseCode = ResponseCodeType.FailedSequenceError
            };
        }

        await fsm.FireAsync(SessionEventEnum.ServiceDiscovery);

        return new ServiceDiscoveryResType
        {
            ResponseCode = ResponseCodeType.Ok,
            PaymentOptionList = { PaymentOptionType.Contract },
            ServiceList = { new ServiceType()
            {
                ServiceId = 1,
                ServiceCategory = ServiceCategoryType.EvCharging
            }},
        };
    }
}