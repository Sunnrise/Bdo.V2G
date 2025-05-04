using Volo.Abp.Modularity;

namespace Bdo.V2G;

[DependsOn(
    typeof(V2GDomainModule),
    typeof(V2GTestBaseModule)
)]
public class V2GDomainTestModule : AbpModule
{

}
