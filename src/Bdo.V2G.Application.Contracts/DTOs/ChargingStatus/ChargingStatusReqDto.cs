namespace Bdo.V2G.DTOs.ChargingStatus;

public class ChargingStatusReqDto
{
    public int EVRemainingTime { get; set; } // Kalan şarj süresi (dakika cinsinden)
    public double EVTargetCurrent { get; set; } // Amper cinsinden hedef akım
}