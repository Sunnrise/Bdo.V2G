using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Bdo.V2G.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Stateless;
using Volo.Abp.AspNetCore.SignalR;

namespace Bdo.V2G.Services.SessionManagement;

public class SessionManagerService : ISessionManagerService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHubContext<V2GSessionHub> _hubContext;
    private readonly ConcurrentDictionary<string, StateMachine<SessionStateEnum, SessionEventEnum>> _fsmSessions;

    public SessionManagerService(
        IMemoryCache memoryCache,
        IHubContext<V2GSessionHub> hubContext
    )
    {
        _memoryCache = memoryCache;
        _hubContext = hubContext;
        _fsmSessions = new ConcurrentDictionary<string, StateMachine<SessionStateEnum, SessionEventEnum>>();
    }
    
    public StateMachine<SessionStateEnum, SessionEventEnum> GetOrCreateSession(string sessionId)
    {
        return _fsmSessions.GetOrAdd(sessionId, _ =>
        {
            var sm = new StateMachine<SessionStateEnum, SessionEventEnum>(SessionStateEnum.Start);
            
            sm.OnTransitionCompleted(async transition =>
            {
                await _hubContext.Clients.Group(sessionId).SendAsync("StateChanged", new
                {
                    SessionId = sessionId,
                    From = transition.Source.ToString(),
                    To = transition.Destination.ToString(),
                    Trigger = transition.Trigger.ToString(),
                    Time = DateTime.UtcNow
                });
            });
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
                .PermitReentry(SessionEventEnum.ChargingStatus) 
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

[HubRoute("/v2g-hub")]
public class V2GSessionHub : Hub
{
    public async Task JoinSession(string sessionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
    }

    // Bağlantı kesildiğinde otomatik olarak gruptan çıkarılır
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Opsiyonel: İlgili tüm session'lardan çıkarma işlemi yapılabilir (grup yönetimi eklenirse)
        await base.OnDisconnectedAsync(exception);
    }
}