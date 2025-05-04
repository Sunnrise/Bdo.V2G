using Bdo.V2G.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Bdo.V2G.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(V2GEntityFrameworkCoreModule),
    typeof(V2GApplicationContractsModule)
    )]
public class V2GDbMigratorModule : AbpModule
{
}
