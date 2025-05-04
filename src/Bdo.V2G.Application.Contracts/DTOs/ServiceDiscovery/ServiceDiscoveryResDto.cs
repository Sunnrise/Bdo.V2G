using System.Collections.Generic;

namespace Bdo.V2G.DTOs.ServiceDiscovery;

public class ServiceDiscoveryResDto
{
    public string ResponseCode { get; set; }
    public List<string> SupportedServices { get; set; }
}