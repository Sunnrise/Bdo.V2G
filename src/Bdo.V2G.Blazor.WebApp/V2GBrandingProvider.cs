using Microsoft.Extensions.Localization;
using Bdo.V2G.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Bdo.V2G.Blazor.WebApp;

[Dependency(ReplaceServices = true)]
public class V2GBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<V2GResource> _localizer;

    public V2GBrandingProvider(IStringLocalizer<V2GResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
