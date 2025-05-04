using Volo.Abp.Modularity;

namespace Bdo.V2G;

[DependsOn(
    typeof(V2GApplicationModule),
    typeof(V2GDomainTestModule)
)]
public class V2GApplicationTestModule : AbpModule
{

}
