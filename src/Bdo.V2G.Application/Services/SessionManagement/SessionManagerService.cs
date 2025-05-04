using System.Collections.Concurrent;
using Bdo.V2G.Enums;
using Microsoft.Extensions.Caching.Memory;
using Stateless;

namespace Bdo.V2G.Services.SessionManagement;

public class SessionManagerService : ISessionManagerService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ConcurrentDictionary<string, StateMachine<SessionStateEnum, SessionEventEnum>> _fsmSessions;

    public SessionManagerService(
        IMemoryCache memoryCache
    )
    {
        _memoryCache = memoryCache;
        _fsmSessions = new ConcurrentDictionary<string, StateMachine<SessionStateEnum, SessionEventEnum>>();
    }
    
    public StateMachine<SessionStateEnum, SessionEventEnum> GetOrCreateSession(string sessionId)
    {
        return _fsmSessions.GetOrAdd(sessionId, _ =>
        {
            var sm = new StateMachine<SessionStateEnum, SessionEventEnum>(SessionStateEnum.Start);

            sm.Configure(SessionStateEnum.Start)
                .Permit(SessionEventEnum.SessionSetup, SessionStateEnum.SessionSetupCompleted);

            sm.Configure(SessionStateEnum.SessionSetupCompleted)
                .Permit(SessionEventEnum.ServiceDiscovery, SessionStateEnum.ServiceDiscoveryCompleted);

            sm.Configure(SessionStateEnum.ServiceDiscoveryCompleted)
                .Permit(SessionEventEnum.PaymentDetails, SessionStateEnum.PaymentDetailsCompleted);

            sm.Configure(SessionStateEnum.PaymentDetailsCompleted)
                .Permit(SessionEventEnum.Authorization, SessionStateEnum.AuthorizationCompleted);

            sm.Configure(SessionStateEnum.AuthorizationCompleted)
                .Permit(SessionEventEnum.ChargeParameterDiscovery, SessionStateEnum.ChargeParameterDiscoveryCompleted);

            sm.Configure(SessionStateEnum.ChargeParameterDiscoveryCompleted)
                .Permit(SessionEventEnum.PowerDeliveryStart, SessionStateEnum.ChargingStarted);

            sm.Configure(SessionStateEnum.ChargingStarted)
                .PermitReentry(SessionEventEnum.ChargingStatus) // <<< Doğrusu bu!
                .Permit(SessionEventEnum.MeteringReceipt, SessionStateEnum.ChargingCompleted);

            sm.Configure(SessionStateEnum.ChargingCompleted)
                .Permit(SessionEventEnum.PowerDeliveryStop, SessionStateEnum.SessionStopped);

            sm.Configure(SessionStateEnum.SessionStopped)
                .PermitReentry(SessionEventEnum.SessionStop);

            return sm;
        });
    }

    public void RemoveSession(string sessionId)
    {
        _fsmSessions.TryRemove(sessionId, out _);
    }
}