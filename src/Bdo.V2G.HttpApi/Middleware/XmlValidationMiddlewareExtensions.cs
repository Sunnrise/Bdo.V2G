using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Bdo.V2G.Middleware;

public static class XmlValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseXmlValidation(this IApplicationBuilder builder)
    {
        // var env = builder.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        return builder.UseMiddleware<XmlValidationMiddleware>();
    }
}