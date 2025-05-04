using Bdo.V2G.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bdo.V2G.Permissions;

public class V2GPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(V2GPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(V2GPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<V2GResource>(name);
    }
}
