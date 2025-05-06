using Bdo.V2G.Services.SessionManagement;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Bdo.V2G;

[DependsOn(
    typeof(V2GDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(V2GApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
typeof(AbpAspNetCoreSignalRModule)
    
    )]
public class V2GApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<V2GApplicationModule>();
        });
        context.Services.AddMemoryCache();
        context.Services.AddSingleton<ISessionManagerService,SessionManagerService>();

    }
}
