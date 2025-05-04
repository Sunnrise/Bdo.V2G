using Bdo.V2G.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Bdo.V2G.Blazor.WebApp.Tiered.Client;

public abstract class V2GComponentBase : AbpComponentBase
{
    protected V2GComponentBase()
    {
        LocalizationResource = typeof(V2GResource);
    }
}
