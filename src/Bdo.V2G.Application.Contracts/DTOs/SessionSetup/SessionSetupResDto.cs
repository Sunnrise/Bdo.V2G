namespace Bdo.V2G.DTOs;

public class SessionSetupResDto
{
    public string ResponseCode { get; set; }   // "OK" / "FAILED"
    public string EVSEID { get; set; }          // Şarj istasyonu ID'si
    public string SessionID { get; set; }
}