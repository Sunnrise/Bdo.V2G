namespace Bdo.V2G.DTOs.PowerDelivery;

public class PowerDeliveryReqDto
{
    public string ChargeProgress { get; set; } // örn: Start, Stop, Renegotiate
    public double EVTargetVoltage { get; set; } // Volt cinsinden
    public double EVTargetCurrent { get; set; } // Amper cinsinden
}