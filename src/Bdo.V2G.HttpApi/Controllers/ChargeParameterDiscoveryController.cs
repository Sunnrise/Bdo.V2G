using System.Threading.Tasks;
using Bdo.V2G.Constants;
using Bdo.V2G.DTOs.ChargeParameter;
using Bdo.V2G.Enums;
using Bdo.V2G.Services;
using Bdo.V2G.Services.SessionManagement;
using Iso15118.V2G.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/charge-parameter/discovery")]
[ApiController]
public class ChargeParameterDiscoveryController : V2GController
{
    private readonly IChargeParameterDiscoveryService _chargeParameterDiscoveryService;
    private readonly ISessionManagerService _sessionManagerService;

    public ChargeParameterDiscoveryController(
        IChargeParameterDiscoveryService chargeParameterDiscoveryService,
        ISessionManagerService sessionManagerService
    )
    {
        _chargeParameterDiscoveryService = chargeParameterDiscoveryService;
        _sessionManagerService = sessionManagerService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    [Produces("application/xml")]
    public async Task<ChargeParameterDiscoveryResType> DiscoverAsync(
        [FromBody] ChargeParameterDiscoveryReqType input
    )
    {
        var fsm = _sessionManagerService.GetOrCreateSession(SessionConstants.SessionId);
        var response = new ChargeParameterDiscoveryResType();
        if (!fsm.CanFire(SessionEventEnum.ChargeParameterDiscovery))
        {
            response.ResponseCode = ResponseCodeType.FailedSequenceError;
            response.EvseProcessing = EvseProcessingType.Finished;
            return response;
        }
        
        if (input.RequestedEnergyTransferMode == EnergyTransferModeType.AcSinglePhaseCore)
        {
            response.EvseChargeParameter = new AcEvseChargeParameterType
            {
                AcEvseStatus = new AcEvseStatusType(),
                EvseNominalVoltage = new PhysicalValueType
                {
                    Value = 230,
                    Multiplier = 0,
                    Unit = UnitSymbolType.V
                },
                EvseMaxCurrent = new PhysicalValueType
                {
                    Value = 32,
                    Multiplier = 0,
                    Unit = UnitSymbolType.A
                }
            };
        }
        else if (input.RequestedEnergyTransferMode == EnergyTransferModeType.DcCore)
        {
            response.EvseChargeParameter = new DcEvseChargeParameterType
            {
                DcEvseStatus = new DcEvseStatusType(),
                EvseMaximumCurrentLimit = new PhysicalValueType
                {
                    Value = 100,
                    Multiplier = 0,
                    Unit = UnitSymbolType.A
                },
                EvseMaximumVoltageLimit = new PhysicalValueType
                {
                    Value = 400,
                    Multiplier = 0,
                    Unit = UnitSymbolType.V
                }
            };
        }
        
        await fsm.FireAsync(SessionEventEnum.ChargeParameterDiscovery);
        return await Task.FromResult(response);
    }
}