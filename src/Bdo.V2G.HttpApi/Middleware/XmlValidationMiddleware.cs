using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bdo.V2G.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Bdo.V2G.Middleware;

public class XmlValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDictionary<string, string> _pathToXsdMapping;
    private readonly string _baseSchemaPath;

    public XmlValidationMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env
    )
    {
        _next = next;
        _baseSchemaPath = Path.Combine(env.ContentRootPath, "Schemas");
        _pathToXsdMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "api/charging-session/setup", "V2G_CI_SessionSetupReq.xsd" },
            { "/api/service-discovery", "V2G_CI_ServiceDiscoveryReq.xsd" },
            { "/api/payment-details", "V2G_CI_PaymentDetailsReq.xsd" },
            { "/api/authorization", "V2G_CI_AuthorizationReq.xsd" },
            { "/api/charge-parameter/discovery", "V2G_CI_ChargeParameterDiscoveryReq.xsd" },
            { "/api/charging-status", "V2G_CI_ChargingStatusReq.xsd" },
            { "/api/metering-receipt", "V2G_CI_MeteringReceiptReq.xsd" },
            { "/api/power/delivery", "V2G_CI_PowerDeliveryReq.xsd" },
            { "/api/session-stop", "V2G_CI_SessionStopReq.xsd" }
            
        };
    }

    public async Task InvokeAsync(
        HttpContext context
    )
    {
        if (context.Request.Method == HttpMethods.Post &&
            context.Request.ContentType != null &&
            context.Request.ContentType.Contains("application/xml", StringComparison.OrdinalIgnoreCase))
        {
            if (_pathToXsdMapping.TryGetValue(context.Request.Path.Value ?? "", out var xsdFile))
            {
                var fullXsdPath = Path.Combine(_baseSchemaPath, xsdFile);

                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                try
                {
                    Utilities.XmlValidator.ValidateXml(body, fullXsdPath);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync($"{{\"error\":\"XML validation failed: {ex.Message}\"}}");
                    return;
                }
            }
        }

        await _next(context);
    }
}