using System.Threading.Tasks;
using Bdo.V2G.DTOs.MeteringReceipt;
using Bdo.V2G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

[Route("api/metering-receipt")]
[ApiController]
public class MeteringReceiptController : V2GController
{
    private readonly IMeteringReceiptService _meteringReceiptService;

    public MeteringReceiptController(IMeteringReceiptService meteringReceiptService)
    {
        _meteringReceiptService = meteringReceiptService;
    }

    [HttpPost]
    [Consumes("application/xml", "application/json")]
    public async Task<MeteringReceiptResDto> SubmitAsync([FromBody] MeteringReceiptReqDto input)
    {
        return await _meteringReceiptService.SubmitMeteringReceiptAsync(input);
    }
}