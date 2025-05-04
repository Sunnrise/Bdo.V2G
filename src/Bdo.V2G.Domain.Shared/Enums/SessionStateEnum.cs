namespace Bdo.V2G.Enums;

public enum SessionStateEnum
{
    Start = 0,
    SessionSetupCompleted = 1,
    ServiceDiscoveryCompleted = 2,
    PaymentDetailsCompleted = 3,
    AuthorizationCompleted = 4,
    ChargeParameterDiscoveryCompleted = 5,
    ChargingStarted = 6,
    ChargingCompleted = 7,
    SessionStopped = 8
}