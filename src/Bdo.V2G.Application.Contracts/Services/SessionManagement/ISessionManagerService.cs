using Bdo.V2G.Enums;
using Stateless;

namespace Bdo.V2G.Services.SessionManagement;

public interface ISessionManagerService
{
    StateMachine<SessionStateEnum, SessionEventEnum> GetOrCreateSession(string sessionId);
    void RemoveSession(string sessionId);
}