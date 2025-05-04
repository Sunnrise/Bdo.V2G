using Bdo.V2G.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdo.V2G.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class V2GController : AbpControllerBase
{
    protected V2GController()
    {
        LocalizationResource = typeof(V2GResource);
    }
}
