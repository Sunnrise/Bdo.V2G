using Volo.Abp.Settings;

namespace Bdo.V2G.Settings;

public class V2GSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(V2GSettings.MySetting1));
    }
}
