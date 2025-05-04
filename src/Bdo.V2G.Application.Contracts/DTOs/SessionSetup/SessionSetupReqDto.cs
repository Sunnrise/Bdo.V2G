namespace Bdo.V2G.DTOs;

public class SessionSetupReqDto
{
    public string SessionID { get; set; }   // Oturum kimliği (zorunlu)

    public string EVCCID { get; set; }       // Araç iletişim kimliği (Vehicle Communication Controller ID)

    public string EVSEID { get; set; }
}