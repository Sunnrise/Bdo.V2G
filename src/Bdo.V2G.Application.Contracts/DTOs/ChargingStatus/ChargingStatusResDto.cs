namespace Bdo.V2G.DTOs.ChargingStatus;

public class ChargingStatusResDto
{
    public string ResponseCode { get; set; }
    public double EVSEPresentVoltage { get; set; } // Şu anki şarj istasyonu voltajı (V)
}