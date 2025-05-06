using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Bdo.V2G.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bdo.V2G.Middleware;

public class XmlValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _baseSchemaPath;
    private readonly ILogger<XmlValidationMiddleware> _logger;

    public XmlValidationMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<XmlValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _baseSchemaPath = Path.Combine(env.ContentRootPath, "schemas");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post &&
            context.Request.ContentType?.Contains("application/xml") == true)
        {
            var requestPath = context.Request.Path.Value?.ToLowerInvariant();

            // Ana XSD'yi belirle (örnek olarak MsgDef kullanıyoruz)
            var mainXsdPath = Path.Combine(_baseSchemaPath, "V2G_CI_MsgDef.xsd");

            try
            {
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                XmlValidator.ValidateXml(body, mainXsdPath);
            }
            catch (XmlSchemaValidationException ex)
            {
                _logger.LogError(ex, "XML validation failed");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"{{\"error\": \"XML validation failed: {ex.Message}\"}}");
                return;
            }
        }

        await _next(context);
    }
}