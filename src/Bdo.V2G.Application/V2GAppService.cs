using System;
using System.Collections.Generic;
using System.Text;
using Bdo.V2G.Localization;
using Volo.Abp.Application.Services;

namespace Bdo.V2G;

/* Inherit your application services from this class.
 */
public abstract class V2GAppService : ApplicationService
{
    protected V2GAppService()
    {
        LocalizationResource = typeof(V2GResource);
    }
}
